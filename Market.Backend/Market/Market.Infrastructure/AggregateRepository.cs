using Core.InternalEventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Exceptions;
using Core.Events.DomainEvents;
using Core.Repository;

namespace Market.Infrastructure
{
    public class AggregateRepository<TA, TId> : IAggregateRepository<TA, TId>
        where TId : IComparable, IEquatable<TId>
        where TA : IAggregateRoot<TId>
    {
        private readonly IEventStore<TA, TId> _eventStore;
        private readonly IInternalEventPublisher _eventPublisher;

        public AggregateRepository(IEventStore<TA, TId> eventStore,
            IInternalEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
        }


        public async Task SaveAsync(TA aggregateRoot, CancellationToken cancellationToken = default)
        {
            var events = aggregateRoot.FlushUncommittedEvents();
            if (!events.Any())
                return;

            await _eventStore.AppendAsync(events, cancellationToken);

            try
            {
                    await _eventPublisher.PublishAsync(events, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<TA> GetAsync(TId id, long? expectedVersion = null, CancellationToken cancellationToken = default)
        {
            var agr = await _eventStore.RestoreAsync(id, cancellationToken);
            if (agr.Equals(default))
                return agr;

            if (null != expectedVersion)
                CheckExpectedVersion(expectedVersion.Value, agr.Version);
            return agr;
        }

        private void CheckExpectedVersion(long expectedVersion, long aggregateVersion)
        {
            if (aggregateVersion != expectedVersion)
                throw new AggregateConcurrencyException(typeof(TA));
        }
    }
}
