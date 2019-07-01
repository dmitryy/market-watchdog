using Microsoft.Extensions.Caching.Memory;

namespace Moex.Api.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(
            IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public T Set<T>(string key, T value)
        {
            return _memoryCache.Set<T>(key, value);
        }
    }
}
