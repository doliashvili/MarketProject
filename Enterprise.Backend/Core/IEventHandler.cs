using System.Threading;
using System.Threading.Tasks;
using Core.Events;

namespace Core
{
    /// <summary>
    /// Defines a handler for an event.
    /// </summary>
    /// <typeparam name="T">Event type being handled</typeparam>
    public interface IEventHandler<in T> where T: IEvent
    {
        /// <summary>
        /// Handle event
        /// </summary>
        /// <param name="event">IEvent type</param>
        /// <param name="cancellationToken">CancellationToken(optional)</param>
        /// <returns>Task</returns>
        Task HandleAsync(T @event, CancellationToken cancellationToken = default);
    }
}
