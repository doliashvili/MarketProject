using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Repository;
using StackExchange.Redis;

namespace Cache.Redis
{
    public class RedisCache : ICache
    {
        private readonly IDatabase _database;

        public RedisCache(IDatabase database)
        {
            _database = database;
        }

        public async Task<string> SetStringAsync(string key, string value, TimeSpan? expiration = null, 
            CancellationToken cancellationToken = default)
        {
            await _database.StringSetAsync(new RedisKey(key), new RedisValue(value), expiration);
            return value;
        }

        public Task<T> SetAsync<T>(string key, T value, TimeSpan? expiration = null, 
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOrSetAsync<T>(string key, Func<T> valueSelector, TimeSpan? expiration = null, 
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetOrSetStringAsync(string key, Func<string> valueSelector, TimeSpan? expiration = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetStringAsync(string key, CancellationToken cancellationToken = default)
        {
            var result = await _database.StringGetAsync(new RedisKey(key));
            return result.HasValue ? result.ToString() : null;
        }

        public Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync<T>(string key, T newValue, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateStringAsync(string key, string newValue, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
