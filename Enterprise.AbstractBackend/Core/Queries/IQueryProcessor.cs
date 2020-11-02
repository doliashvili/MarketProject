using System.Threading;
using System.Threading.Tasks;

namespace Core.Queries
{
    /// <summary>
    /// Mediator object between clients and query handlers
    /// </summary>
    public interface IQueryProcessor
    {
        /// <summary>
        /// Process query
        /// </summary>
        /// <typeparam name="TResponse">Return type</typeparam>
        /// <param name="query">Query for handle</param>
        /// <param name="cancellationToken">CancellationToken(optional)</param>
        /// <returns>Task of TResponse</returns>
        Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query,
            CancellationToken cancellationToken = default);
    }
}