using System;
using Core.Commands;

namespace Core.Events.DomainEvents
{


    /// <summary>
    /// DomainEvent interface
    /// </summary>
    /// <typeparam name="TId">Aggregate Id</typeparam>
    public interface IDomainEvent<TId> : IDomainEvent
    {
        Guid EventId { get; }
        string EventName { get; }
        string EventType { get; }
        DateTime TimeStamp { get; }
        TId AggregateId { get; }
        long Version { get; }
        CommandMeta CommandMeta { get; }
        string EventString { get; }
        void SetVersion(long version);
    }

    public interface IDomainEvent : IEvent { }

}