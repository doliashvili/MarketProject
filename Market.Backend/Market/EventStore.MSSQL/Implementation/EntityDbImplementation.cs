using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Common;
using Core.Domain;
using Core.Events.DomainEvents;
using Core.Repository;
using Dapper;
using EventStore.MSSQL.Connection;
using EventStore.MSSQL.Model;
using Newtonsoft.Json;

namespace EventStore.MSSQL.Implementation
{
    public class EntityDbImplementation<TA, TId> : IEventStore<TA, TId>
    where TA : IAggregateRoot<TId>, new()
    where TId : IEquatable<TId>, IComparable
    {
        private string EventStoreTableName = "EventStore";
        //Todo add EventType ?
        private static string EventStoreListOfColumnsInsert = "[Id], [CreatedAt], [Version], [Name], [AggregateId], [Data]";

        private static readonly string EventStoreListOfColumnsSelect = $"{EventStoreListOfColumnsInsert},[Sequence]";

        private readonly ISqlConnectionFactory _connectionFactory;

        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new PrivateSetterContractResolver()
        };

        public EntityDbImplementation(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AppendAsync(IEnumerable<IDomainEvent<TId>> domainEvents, CancellationToken cancellationToken = default)
        {
            if (!domainEvents.Any())
                return;

            var query =
                $@"INSERT INTO {EventStoreTableName} ({EventStoreListOfColumnsInsert})
                    VALUES (@Id,@CreatedAt,@Version,@Name,@AggregateId,@Data);";

            var listOfEvents = domainEvents.OrderBy(x => x.Version).Select(ev => new
            {
                CreatedAt = ev.TimeStamp,
                Data = JsonConvert.SerializeObject(ev, Formatting.Indented, _jsonSerializerSettings),
                Id = ev.EventId,
                ev.GetType().Name,
                AggregateId = ev.AggregateId.ToString(),
                ev.Version,
            });

            await using var connection = _connectionFactory.SqlConnection();
            await connection.ExecuteAsync(query, listOfEvents);
        }

        public async Task<TA> RestoreAsync(TId id, CancellationToken cancellationToken = default)
        {
            if (id == null)
                throw new AggregateRootNotProvidedException("AggregateRootId cannot be null");

            var query = new StringBuilder($@"SELECT {EventStoreListOfColumnsSelect} FROM {EventStoreTableName}");
            query.Append(" WHERE [AggregateId] = @AggregateId ");
            query.Append(" ORDER BY [Version] ASC;");
            var agr = new TA();

            await using var connection = _connectionFactory.SqlConnection();
            var events = (await connection.QueryAsync<EventStoreDao>(query.ToString(), id != null ? new { AggregateId = id.ToString() } : null)).ToList();
            var domainEvents = events.Select(TransformEvent).Where(x => x != null).ToList().AsReadOnly();

            foreach (var domainEvent in domainEvents)
            {
                agr.ApplyChange(domainEvent, true);
            }

            return agr.Version == 0 ? default : agr;
        }

        private IDomainEvent<TId> TransformEvent(EventStoreDao eventSelected)
        {
            var o = JsonConvert.DeserializeObject(eventSelected.Data, _jsonSerializerSettings);
            var evt = o as IDomainEvent<TId>;

            return evt;
        }
    }



    public class AggregateRootNotProvidedException : Exception
    {
        public override string Message { get; }

        public AggregateRootNotProvidedException(string aggregaterootidCannotBeNull)
        {
            Message = aggregaterootidCannotBeNull;
        }
    }
}
