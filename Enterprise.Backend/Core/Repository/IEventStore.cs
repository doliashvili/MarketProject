using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Events.DomainEvents;

namespace Core.Repository
{
    /// <summary>
    /// IEventStore interface
    /// </summary>
    /// <typeparam name="TA">Aggregate</typeparam>
    /// <typeparam name="TId">Aggregate id</typeparam>
    public interface IEventStore<TA, TId> where TId : IComparable, IEquatable<TId>
        where TA : IAggregateRoot<TId>
    {
        /// <summary>
        /// Append events to store
        /// </summary>
        /// <param name="domainEvents">DomainEvent</param>
        /// <param name="cancellationToken">CancellationToken(optional)</param>
        /// <returns>Task</returns>
        Task AppendAsync(IEnumerable<IDomainEvent<TId>> domainEvents,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Loads aggregate from events
        /// </summary>
        /// <param name="id">Aggregate id</param>
        /// <param name="cancellationToken">CancellationToken(optional)</param>
        /// <returns>Task of TA</returns>
        Task<TA> RestoreAsync(TId id, CancellationToken cancellationToken = default);
    }
}