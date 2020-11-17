using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.MessageBroker
{
    /// <summary>
    /// Message broker producer interface
    /// </summary>
    public interface IMessageProducer
    {
        /// <summary>
        /// Produce single message
        /// </summary>
        /// <typeparam name="T">IMessage type</typeparam>
        /// <param name="message">message</param>
        /// <param name="publishOptions">publisher options(optional)</param>
        /// <param name="cancellationToken">cancellation token(optional)</param>
        /// <returns>Task</returns>
        Task PublishAsync<T>(T message, PublishOptions publishOptions = default,
            CancellationToken cancellationToken = default) where T : IMessage;

        /// <summary>
        /// Produce multiple events
        /// </summary>
        /// <typeparam name="T">IMessage type</typeparam>
        /// <param name="messages">messages</param>
        /// <param name="publishOptions">publisher options(optional)</param>
        /// <param name="cancellationToken">cancellation token(optional)</param>
        /// <returns>Task</returns>
        Task PublishAsync<T>(IEnumerable<T> messages, PublishOptions publishOptions = default, 
            CancellationToken cancellationToken = default) where T : IMessage;
    }
}
