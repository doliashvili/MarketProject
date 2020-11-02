using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.Repository
{
    /// <summary>
    /// Repository for aggregates
    /// </summary>
    /// <typeparam name="TA">Aggregate</typeparam>
    /// <typeparam name="TId">Aggregate Id</typeparam>
    public interface IAggregateRepository<TA, TId> 
        where TId: IComparable, IEquatable<TId>
        where TA: IAggregateRoot<TId>
    {
        /// <summary>
        /// Save new aggregate
        /// </summary>
        /// <param name="aggregateRoot">Aggregate</param>
        /// <param name="cancellationToken">CancellationToken(optional)</param>
        /// <returns>Task</returns>
        Task SaveAsync(TA aggregateRoot, CancellationToken cancellationToken = default);


        /// <summary>
        /// Returns aggregate by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expectedVersion">Aggregate expected version(optional, if value will be null it ignores concurrency checks)</param>
        /// <param name="cancellationToken">CancellationToken(optional)</param>
        /// <returns>Task of TA</returns>
        Task<TA> GetAsync(TId id, long? expectedVersion = null, CancellationToken cancellationToken = default);
    }
}