using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.MessageBroker.Helpers;
using Microsoft.Extensions.Hosting;


namespace MessageBroker.RabbitMQ.Consumer
{
    public class ConsumerHost : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ConsumerHost(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumers = ConsumerFinder.GetConsumers().ToList();
            if (consumers.Any())
            {
                consumers.ForEach(consumer =>
                {
                    var subscriber = new Subscriber(consumer, _serviceProvider);
                    subscriber.StartConsuming();
                });
            }

            return Task.CompletedTask;
        }
    }
}
