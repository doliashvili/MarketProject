using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Common;
using Core.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Core.InternalEventSystem
{
    /// <summary>
    /// Concrete internal event publisher
    /// </summary>
    public class InternalEventPublisher : IInternalEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public InternalEventPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        

        /// <summary>
        /// Publish events asynchronously
        /// </summary>
        /// <typeparam name="T">Event type</typeparam>
        /// <param name="events">Collection of events</param>
        /// <param name="cancellationToken">CancellationToken(optional)</param>
        /// <returns>Task</returns>
        public Task PublishAsync<T>(IEnumerable<T> events, CancellationToken cancellationToken = default) where T : IEvent
        {
            events = events.ToArray();

            if (!events.Any())
                return Task.CompletedTask;

            var tasks = new List<Task>();

            foreach (var @event in events)
            {
                var handlerType = typeof(IInternalEventHandler<>).MakeGenericType(@event.GetType());
                var handlers =
                    _serviceProvider.GetServices(handlerType).ToList();

                var taskList = handlers
                    .Select(handler => (Task)handler.Invoke(
                        "HandleAsync",
                        @event,
                        cancellationToken))
                    .ToList();

                tasks.AddRange(taskList);
            }

            return !tasks.Any() ? Task.CompletedTask : Task.WhenAll(tasks);
        }
    }
}