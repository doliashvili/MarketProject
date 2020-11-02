using Microsoft.Extensions.DependencyInjection;
using ReadModelRepository.RavenDb.abstraction;
using ReadModelRepository.RavenDb.implementation;

namespace ReadModelRepository.RavenDb
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRavenDbReadModels(this IServiceCollection services,
            RavenSettings settings)
        {
            services.AddSingleton(services)
                .AddSingleton<IRavenConnectionWrapper, RavenDbConnectionWrapper>();

            return services;
        }
    }
}
