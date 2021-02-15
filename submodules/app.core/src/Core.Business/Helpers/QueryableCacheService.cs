using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.Data.Extensions;
using Core.Common.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Core.Base.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Helpers
{
    public class QueryableCacheService: IQueryableCacheService
    {
        private readonly IMemoryCache _cache;
        public QueryableCacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        public IEnumerable<T> GetData<T>(string key = null, Expression expression = null) where T : class
        {
            var cacheKey = GetCacheKey<T>(expression, key);
            _cache.TryGetValue<T[]>(cacheKey, out var data);

            return data ?? new T[0];
        }

        public IEnumerable<T> SetData<T>(IQueryable<T> data, int expirationTime, string key, int evaluatedRecordCount) where T : class
        {
            var cacheKey = GetCacheKey<T>(data.Expression, key);
            var result = data.ToArray();
            if (evaluatedRecordCount !=0)
            {
                if (typeof(IPagingCounted).IsAssignableFrom(typeof(T)))
                {
                    foreach (var item in (IEnumerable<IPagingCounted>)result)
                    {
                        item.TotalRecordCount = evaluatedRecordCount;
                    }
                }
            }
          
            _cache.Set(cacheKey, result, TimeSpan.FromSeconds(expirationTime));
            return result;
        }

        public async Task<IEnumerable<T>> SetDataAsync<T>(IQueryable<T> data, int expirationTime, string key, int evaluatedRecordCount) where T : class
        {
            var cacheKey = GetCacheKey<T>(data.Expression, key);
            var result = await data.ToArrayAsync();
            if (evaluatedRecordCount != 0)
            {
                if (typeof(IPagingCounted).IsAssignableFrom(typeof(T)))
                {
                    foreach (var item in (IEnumerable<IPagingCounted>)result)
                    {
                        item.TotalRecordCount = evaluatedRecordCount;
                    }
                }
            }

            _cache.Set(cacheKey, result, TimeSpan.FromSeconds(expirationTime));
            return result;
        }

        private string GetCacheKey<T>(Expression expression, string key) where T : class
        {
            var cacheKey = "";
            if (expression != null)
            {
                cacheKey += LocalCollectionExpander.Rewrite(Evaluator.PartialEval(expression, QueryResultCache.CanBeEvaluatedLocally)).ToString();
            }

            if (!string.IsNullOrEmpty(key)) {
                cacheKey += key;
            }

            if (cacheKey.Length > 50)
            {
                cacheKey = cacheKey.ToMd5Fingerprint();
            }
            cacheKey += typeof(T).Name + "_iqueryable_";

            return cacheKey;
        }

      
    }
}
