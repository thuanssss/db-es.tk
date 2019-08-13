using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Services.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace DatabaseEnsoulSharp.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMyCache _cache;

        public CacheService(IMyCache myCache)
        {
            _cache = myCache;
        }

        public void ClearCacheAll()
        {
            _cache.Clear();
        }

        public void ClearCacheByKey(string key)
        {
            _cache.Remove(key);
        }

        public T GetCache<T>(string key)
        {
            return !_cache.TryGetValue(key, out T data) ? default(T) : data;
        }

        public void SetCache<T>(string key, T value)
        {
            _cache.Set(key, value, TimeSpan.FromDays(1));
        }
    }
}