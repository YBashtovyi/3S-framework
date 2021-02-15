using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Base.Data;

namespace Core.Data.Helpers
{
    public interface IQueryableCacheService
    {
        /// <summary>
        /// Gets data collection from cache by key
        /// </summary>
        /// <typeparam name="T">Collection data type</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="expression">If data stored to cache used predicate, pass it to the function</param>
        /// <returns></returns>
        IEnumerable<T> GetData<T>(string key = null, Expression expression = null) where T : class;

        /// <summary>
        /// Executes query and stores result into cache
        /// </summary>
        /// <typeparam name="T">Collection data type</typeparam>
        /// <param name="query">Query to get data from database</param>
        /// <param name="expirationTime">Cache duration</param>
        /// <param name="key">Cache key</param>
        /// <param name="evaluatedRecordCount">Total records count for queries, that do not calculate count themself or 0 if do calculate</param>
        /// <returns>Data collection fetched from database</returns>
        IEnumerable<T> SetData<T>(IQueryable<T> query, int expirationTime, string key, int evaluatedRecordCount) where T : class;

        /// <summary>
        /// Executes query async and stores result into cache
        /// </summary>
        /// <typeparam name="T">Collection data type</typeparam>
        /// <param name="query">Query to get data from database</param>
        /// <param name="expirationTime">Cache duration</param>
        /// <param name="key">Cache key</param>
        /// <param name="evaluatedRecordCount">Total records count for queries, that do not calculate count themself or 0 if do calculate</param>
        /// <returns>Data collection fetched from database</returns>
        Task<IEnumerable<T>> SetDataAsync<T>(IQueryable<T> query, int expirationTime, string key, int evaluatedRecordCount) where T : class;
    }
}
