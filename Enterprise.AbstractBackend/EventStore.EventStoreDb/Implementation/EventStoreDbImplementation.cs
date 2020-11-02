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
using EventStore.ClientAPI;
using EventStore.EventStoreDb.Connection;
using EventStore.EventStoreDb.Helpers;
using Newtonsoft.Json;

// ReSharper disable PossibleMultipleEnumeration

namespace EventStore.EventStoreDb.Implementation
{
    public class EventStoreDbImplementation<TA, TId> : IEventStore<TA, TId>
        where TA: IAggregateRoot<TId> 
        where TId: IComparable, IEquatable<TId>
    {
        private readonly string _streamPrefix;
        private readonly IEventStoreConnectionWrapper _connectionWrapper;

        public EventStoreDbImplementation(IEventStoreConnectionWrapper connectionWrapper)
        {
            _connectionWrapper = connectionWrapper;
            _streamPrefix = typeof(TA).Name;
        }

        private string GetStreamName(TId id) => $"{_streamPrefix}-{id}";


        public async Task AppendAsync(IEnumerable<IDomainEvent<TId>> domainEvents, CancellationToken cancellationToken = default)
        {
            if (!domainEvents.Any())
                return;

            var connection = _connectionWrapper.Connection;
            var first = domainEvents.First();
            var streamName = GetStreamName(first.AggregateId);
            var version = first.Version - 1;

            using var transaction = await connection.StartTransactionAsync(streamName, version);

            try
            {
                foreach (var @event in domainEvents)
                {
                    var bytes = Encoding.UTF8.GetBytes(@event.EventString);
                    var eventData = new EventData(@event.EventId, @event.EventName, true, bytes, null);
                    await transaction.WriteAsync(eventData);
                }

                await transaction.CommitAsync();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        public async Task<TA> RestoreAsync(TId id, CancellationToken cancellationToken = default)
        {
            var streamName = GetStreamName(id);
            var events = new List<IDomainEvent<TId>>();
            var batchSize = _connectionWrapper.Configuration.BatchSize <= 0 ? 100 : _connectionWrapper.Configuration.BatchSize;
            var agr = Activator.CreateInstance<TA>();
            StreamEventsSlice currentSlice;
            long nextSliceStart = StreamPosition.Start;
            do
            {
                currentSlice = await _connectionWrapper.Connection
                    .ReadStreamEventsForwardAsync(streamName, nextSliceStart, batchSize, false);
                nextSliceStart = currentSlice.NextEventNumber;
                events.AddRange(currentSlice.Events.Select(MapToDomainEvent));
            } while (!currentSlice.IsEndOfStream);

            return agr;
        }


        private IDomainEvent<TId> MapToDomainEvent(ResolvedEvent e)
        {
            var eventString = Encoding.UTF8.GetString(e.Event.Data);
            var @event = JsonConvert.DeserializeObject<IDomainEvent<TId>>(eventString);
            var eventType = EventTypeInMemoryStore.GetOrAdd(@event.EventType);
            var desObj = JsonConvert.DeserializeObject(@event.EventString, eventType, new JsonSerializerSettings()
            {
                ContractResolver = new PrivateSetterContractResolver()
            });
            return (IDomainEvent<TId>)desObj;
        }
    }
}
