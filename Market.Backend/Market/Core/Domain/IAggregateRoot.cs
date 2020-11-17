using System;
using System.Collections.Generic;
using Core.Events.DomainEvents;

namespace Core.Domain
{
    public interface IAggregateRoot<TId> where TId: IComparable, IEquatable<TId>
    {
        TId Id { get; }
        long Version { get; }
        void ApplyChange(IDomainEvent<TId> domainEvent, bool isFromHistory = false);
        IDomainEvent<TId>[] GetUncommittedEvents();
        IDomainEvent<TId>[] FlushUncommittedEvents();
        void LoadFromHistory(IEnumerable<IDomainEvent<TId>> eventStream);
    }
}