using System.Linq;
using System.Reflection;
using Core.MessageBroker;
using Core.MessageBroker.Helpers;
using MessageBroker.RabbitMQ.Connection;
using MessageBroker.RabbitMQ.Consumer;
using MessageBroker.RabbitMQ.Producer;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBroker.RabbitMQ
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, RabbitMqConfig config, 
            params Assembly[] consumerAssemblies)
        {

            services.AddSingleton(config)
                .AddSingleton<IRabbitConnectionWrapper>(_ => new RabbitConnectionWrapper(config))
                .AddSingleton<IMessageProducer, RabbitProducer>();

            var consumers = ConsumerFinder.FindAll(consumerAssemblies);
            consumers.ToList().ForEach(consumer =>
            {
                services.AddTransient(consumer);
            });

            services.AddHostedService<ConsumerHost>();

            return services;
        }

    }
}
