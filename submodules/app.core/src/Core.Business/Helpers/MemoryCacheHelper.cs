using Core.Data.Extensions;
using Core.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace Core.Data.Helpers
{
    public class MemoryCacheHelper
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<MemoryCacheHelper> _logger;

        public MemoryCacheHelper(IMemoryCache memoryCache, ILogger<MemoryCacheHelper> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public IList<T> GetCachedEntity<T>(DbContext context,
            double expirationTimeSeconds,
            string cacheKey = null,
            Expression<Func<T, bool>> expression = null) where T : class
        {
            var cacheData = new List<T>();

            if (cacheKey == null)
            {
                cacheKey = typeof(T).ToString();
            }

            string expressionKey = null;
            
            if (expression != null)
            {
                expressionKey = LocalCollectionExpander.Rewrite(Evaluator.PartialEval(expression, QueryResultCache.CanBeEvaluatedLocally)).ToString();
                if (expressionKey.Length > 50)
                {
                    expressionKey = expressionKey.ToMd5Fingerprint();
                }
            }

            try
            {
                if (!_memoryCache.TryGetValue(cacheKey + expressionKey, out cacheData))
                {
                    if (expression != null)
                    {
                        cacheData = context.Set<T>().Where(expression).ToList();
                    }
                    else
                    {
                        cacheData = context.Set<T>().ToList();
                    }
                    
                    if (expirationTimeSeconds > 0)
                    {
                        _memoryCache.Set(cacheKey + expressionKey, cacheData,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(expirationTimeSeconds)));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MemoryCaching error");
            }

            return cacheData;
        }

        public dynamic Cache(string cacheKey, out object cacheObject, double expirationTimeSeconds, Func<object> getObject)
        {
            cacheObject = null;

            try
            {
                if (!_memoryCache.TryGetValue(cacheKey, out cacheObject))
                {
                    cacheObject = getObject();
                    if (expirationTimeSeconds > 0)
                    {
                        _memoryCache.Set(cacheKey, cacheObject, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(expirationTimeSeconds)));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MemoryCaching error");
            }

            return cacheObject;
        }
    }
}
