using System;
using System.Linq;
using Core.Common;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadModelRepository.Concrete;

namespace ReadModelRepository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMSSQLReadModelRepository<TDB>
        (this IServiceCollection services, 
            bool excludeTests = true
            ) where TDB : AppDbContext
        {
            services.AddScoped<AppDbContext>(x => x.GetService<TDB>());

            var assemblies =
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => !x.IsDynamic).ToArray();

            if (excludeTests)
                assemblies = assemblies
                    .Where(x =>
                        // ReSharper disable once PossibleNullReferenceException
                        !x.FullName.Contains("Test", StringComparison.InvariantCultureIgnoreCase))
                    .ToArray();

            var readModelsDictionaryType = AssemblyScanner.FindReadModels(assemblies);
            foreach (var kv in readModelsDictionaryType)
            {
                var readModelInterface = typeof(IReadModelRepository<,>).MakeGenericType(kv.Key, kv.Value);
                var concreteRepository = typeof(RepositoryMSSQL<,>).MakeGenericType(kv.Key, kv.Value);
                services.AddScoped(readModelInterface, concreteRepository);
            }

            return services;
        }
    }
}
