using System;
using System.Threading.Tasks;
using Core.ThirdParty.Redis;
using Microsoft.Extensions.Logging;

namespace Core.Services.DistributedCacheService
{
    public class DistributedCacheService: IDistributedCacheService
    {
        private readonly IRedisdatabaseProvider _sourceProvider;
        private readonly ILogger<DistributedCacheService> _logger;
        public DistributedCacheService(IRedisdatabaseProvider sourceProvider, ILogger<DistributedCacheService> logger)
        {
            _sourceProvider = sourceProvider;
            _logger = logger;
        }

        public T GetValue<T>(string key)
        {
            var source = _sourceProvider.GetDatabase();
            try
            {
                return source.GetValueFromString<T>(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot get value from cache for the key {0}", key);
                //ClearKey(key);
                throw;
            }
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            var source = _sourceProvider.GetDatabase();
            try
            {
                return await source.GetValueFromStringAsync<T>(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot get value from cache for the key {0}", key);
                //ClearKey(key);
                throw;
            }
        }

        public void SetValue(string key, object value, TimeSpan? expiry = null)
        {
            var source = _sourceProvider.GetDatabase();
            try
            {
                source.SaveValueAsString(key, value, expiry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot get save value to cache for the key {0} and object {1}", key, value);
                throw;
            }
        }

        public async Task SetValueAsync(string key, object value, TimeSpan? expiry = null)
        {
            var source = _sourceProvider.GetDatabase();
            try
            {
                await source.SaveValueAsStringAsync(key, value, expiry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot get save value to cache for the key {0} and object {1}", key, value);
                throw;
            }
        }

        public bool ClearKey(string key)
        {
            var source = _sourceProvider.GetDatabase();
            try
            {
                return source.KeyDelete(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot delete value from cache for the key {0}", key);
                throw;
            }
        }

        public async Task Reset(bool allSources = false)
        {
            var source = _sourceProvider.GetDatabase();
            if (allSources)
            {
                await source.ExecuteAsync("FLUSHALL");
            } else
            {
                await source.ExecuteAsync("FLUSHDB");
            }
        }
    }
}
