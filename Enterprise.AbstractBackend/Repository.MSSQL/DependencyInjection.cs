using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.MSSQL.Concrete;

namespace Repository.MSSQL
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddMSSQLReadModelRepository(this IServiceCollection services,
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
                var readModelInterface = typeof(IReadModelRepository<,>).MakeGenericType(kv.Key, kv.Value);
                var concreteRepository = typeof(RepositoryMSSQL<,>).MakeGenericType(kv.Key, kv.Value);
                services.AddTransient(readModelInterface, concreteRepository);
            }

            return services;
        }
    }
}
