using System;
using System.Linq;
using Core.Common;
using Core.Repository;
using EventStore.MSSQL.Connection;
using EventStore.MSSQL.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.MSSQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEntityDbEventStore(this IServiceCollection services,
            string connectionString )
        {
            services.AddScoped<ISqlConnectionFactory>(x=> new SqlConnectionFactory(connectionString))
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
                var concreteEventStore = typeof(EntityDbImplementation<,>).MakeGenericType(kv.Key, kv.Value);
                services.AddTransient(eventStoreInterface, concreteEventStore);
            }

            return services;
        }
    }
}
