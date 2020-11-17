using System;
using System.Collections.Generic;
using Core.Common;
using Core.Domain.Exceptions;
using Core.Events.DomainEvents;

namespace Core.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
        where TId: IComparable, IEquatable<TId>
    {
        public override TId Id { get; protected set; }
        public long Version { get; private set; }
        
        private Queue<IDomainEvent<TId>> _events = new Queue<IDomainEvent<TId>>();
        
        protected AggregateRoot() { }

        protected AggregateRoot(TId id)
        {
            Id = id;
        }
        public void ApplyChange(IDomainEvent<TId> domainEvent, bool isFromHistory = false)
        {
            lock (_events)
            {
                InnerApply(domainEvent);
                if (!isFromHistory)
                {
                    _events.Enqueue(domainEvent);
                    domainEvent.SetVersion((Version + 1));
                }
                if(Version + 1 != domainEvent.Version)
                    throw new AggregateVersioningException(
                        $"DomainEvent({domainEvent.GetType().Name}) version {domainEvent.Version} is not valid, next version should be {Version + 1}");
                Version++;
            }
        }

        public IDomainEvent<TId>[] GetUncommittedEvents()
        {
            lock (_events)
            {
                return _events.ToArray();
            }
        }

        public IDomainEvent<TId>[] FlushUncommittedEvents()
        {
            lock (_events)
            {
                var events = _events.ToArray();
                _events.Clear();
                return events;
            }
        }

        public void LoadFromHistory(IEnumerable<IDomainEvent<TId>> eventStream)
        {
            lock (_events)
            {
                foreach (var @event in eventStream)
                    ApplyChange(@event, true);
            }
        }

        private void InnerApply(IDomainEvent<TId> domainEvent)
        {
            this.Invoke("Apply", domainEvent);
        }

    }
}