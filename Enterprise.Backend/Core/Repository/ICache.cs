using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Repository
{
    /// <summary>
    /// Cache repository
    /// </summary>
    public interface ICache
    {
        Task<string> SetStringAsync(string key, string value, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
        Task<T> SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
        Task<T> GetOrSetAsync<T>(string key, Func<T> valueSelector, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
        Task<string> GetOrSetStringAsync(string key, Func<string> valueSelector, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
        Task<string> GetStringAsync(string key, CancellationToken cancellationToken = default);
        Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;
        Task<T> UpdateAsync<T>(string key, T newValue, CancellationToken cancellation = default) where T : class;
        Task<string> UpdateStringAsync(string key, string newValue, CancellationToken cancellation = default);
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}
