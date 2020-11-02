using System.Threading;
using System.Threading.Tasks;

namespace Core.Queries
{
    /// <summary>
    /// IQueryHandler Interface
    /// </summary>
    /// <typeparam name="TQuery">Query for handle</typeparam>
    /// <typeparam name="TResponse">Return type</typeparam>
    public interface IQueryHandler<in TQuery, TResponse> 
        where TQuery: IQuery<TResponse>
    {
        /// <summary>
        /// Handle query
        /// </summary>
        /// <param name="query">IQuery type</param>
        /// <param name="cancellationToken">CancellationToken(optional)</param>
        /// <returns name="TResponse">Task of TResponse</returns>
        Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}