using EventStore.EventStoreDb.Connection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Core.Common;
using Core.Repository;
using EventStore.EventStoreDb.Implementation;

namespace EventStore.EventStoreDb
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEventStore(this IServiceCollection services, EventStoreConfig config)
        {
            services.AddSingleton(config)
                .AddSingleton<IEventStoreConnectionWrapper>(_ => new EventStoreConnectionWrapper(config))
                .RegisterStores(false);

            return services;
        }


        public static IServiceCollection RegisterStores(this IServiceCollection services,
            bool excludeTests = true)
        {
            var assemblies =
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => !x.IsDynamic).ToArray();

            if (excludeTests)
                assemblies = assemblies
                    .Where(x =>
                        // ReSharper disable once PossibleNullReferenceException
                        !x.FullName.Contains("Test", StringComparison.InvariantCultureIgnoreCase))
                    .ToArray();

            var aggregates = AssemblyScanner.FindAggregates(assemblies);
            foreach (var kv in aggregates)
            {
                var eventStoreInterface = typeof(IEventStore<,>).MakeGenericType(kv.Key, kv.Value);
                var concreteEventStore = typeof(EventStoreDbImplementation<,>).MakeGenericType(kv.Key, kv.Value);
                services.AddTransient(eventStoreInterface, concreteEventStore);
            }

            return services;
        }
    }
}
