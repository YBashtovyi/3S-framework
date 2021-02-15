using System;
using System.Collections.Concurrent;
using System.Linq;
using App.Data.Models;
using Core.Services.Data;
using Core.Services.DistributedCacheService;

namespace App.Business.Services.ApplicationServices
{
    public class CacheValueService
    {
        private static readonly string _cachePrefix = "cvs_";
        private readonly IDistributedCacheService _cache;
        private readonly ICommonDataService _dataService;
        /// <summary>
        /// Stores cached keys to give an ability to reset cache
        /// Key - key in cache, value - does not matter. ConcurrentDictionary is used to support unique values
        /// </summary>
        private ConcurrentDictionary<string, bool> _cacheKeys = new ConcurrentDictionary<string, bool>();

        public CacheValueService(IDistributedCacheService cache, ICommonDataService dataService)
        {
            _cache = cache;
            _dataService = dataService;
        }

        /// <summary>
        /// Cleares all values from caches, that were cached by this service
        /// </summary>
        public void ClearCache()
        {
            // check if this is copy of keys, otherwise should add ToList() because then we delete keys from dictionary
            var keys = _cacheKeys.Keys;

            foreach (var key in keys)
            {
                _cache.ClearKey(key);
                _cacheKeys.TryRemove(key, out _);
            }

        }
    }
}
