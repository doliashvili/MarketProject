using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Events;

namespace Core
{
    public interface IEventPublisher
    {
        /// <summary>
        /// Publish an event to zero to many handler functions.
        /// </summary>
        /// <typeparam name="T">Event type</typeparam>
        /// <param name="events">Collection of events to be sent</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>Task representing publishing</returns>
        Task PublishAsync<T>(IEnumerable<T> events, CancellationToken cancellationToken = default) where T : IEvent;
    }
}