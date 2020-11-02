using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Cache.Redis
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddRedisCache(this IServiceCollection services, RedisConfig redisConfig)
        {

            services.AddSingleton(redisConfig)
                .AddSingleton<IDatabase>(_ =>
                {
                    var conn = ConnectionMultiplexer.Connect(redisConfig.ConnectionSettings);
                    return conn.GetDatabase();
                });

            return services;
        }

    }
}
