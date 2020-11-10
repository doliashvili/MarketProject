using System;
using Core.Commands;
using Core.Domain;
using Newtonsoft.Json;

namespace Core.Events.DomainEvents
{
    /// <summary>
    /// Abstract DomainEvent class
    /// </summary>
    /// <typeparam name="TA">Aggregate</typeparam>
    /// <typeparam name="TId">Aggregate Id</typeparam>
    public abstract class DomainEvent<TA, TId> : IDomainEvent<TId> where TA: IAggregateRoot<TId> 
    where TId: IComparable, IEquatable<TId>
    {
        public Guid EventId { get; private set; }
        public string EventName { get; private set; }
        public string EventType { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public TId AggregateId { get; private set; }
        public long Version { get; private set; }
        public CommandMeta CommandMeta { get; private set; }
        
        [JsonIgnore]
        public string EventString => JsonConvert.SerializeObject(this);
        public void SetVersion(long version) => Version = version;
        protected DomainEvent() { }
        protected DomainEvent(TA aggregateRoot, ICommand command)
        {
            EventId = Guid.NewGuid();
            EventName = GetType().Name;
            EventType = GetType().AssemblyQualifiedName;
            TimeStamp = DateTime.UtcNow;
            CommandMeta = command?.CommandMeta;
            if (AggregateId.Equals(default))
                AggregateId = aggregateRoot==null ? default : aggregateRoot.Id;
        }
    }
}