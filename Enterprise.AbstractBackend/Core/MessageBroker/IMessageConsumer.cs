using System.Threading;
using System.Threading.Tasks;

namespace Core.MessageBroker
{
    /// <summary>
    /// Message broker consumer interface
    /// </summary>
    public interface IMessageConsumer
    {
        /// <summary>
        /// Consume message
        /// </summary>
        /// <param name="message">Json formatted message</param>
        /// <param name="cancellationToken">cancellation token(optional)</param>
        /// <returns>Task of HandleResult</returns>
        Task<HandleResult> ConsumeAsync(string message, CancellationToken cancellationToken = default);
    }
}
