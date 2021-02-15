using Core.Services.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Administration;

namespace Core.Business.Services
{
    public abstract class QueryTextService : IQueryTextService
    {
        public virtual FormattableString GetParameterizedQueryString(Type type,
            IDictionary<string, string> parameters = null,
            bool addCount = false,
            IUserApplicationRights rights = null,
            params object[] formatParams)
        {
            var queryString = GetFullQueryText(type, addCount, rights, formatParams);
            var withConditions = AddConditionsToQueryText(type, queryString, parameters);
            return withConditions;
        }

        public virtual async Task<FormattableString> GetParameterizedQueryStringAsync(Type type,
            IDictionary<string, string> parameters = null,
            bool addCount = false,
            IUserApplicationRights rights = null,
            params object[] formatParams)
        {
            var queryString = await GetFullQueryTextAsync(type, addCount, rights, formatParams);
            var withConditions = AddConditionsToQueryText(type, queryString, parameters);
            return withConditions;
        }

        public virtual string GetFullQueryText(Type type, bool addCount = false, IUserApplicationRights rights = null, params object[] formatParams)
        {
            var queryString = GetSqlText(type);
            if (formatParams == null)
            {
                return GetFullQueryTextInternal(queryString, type, addCount, rights);
            }
            return GetFullQueryTextInternal(string.Format(queryString, formatParams), type, addCount, rights);
        }

        public virtual async Task<string> GetFullQueryTextAsync(Type type, bool addCount = false, IUserApplicationRights rights = null, params object[] formatParams)
        {
            var queryString = await GetSqlTextAsync(type);
            if (formatParams == null)
            {
                return GetFullQueryTextInternal(queryString, type, addCount, rights);
            }
            return GetFullQueryTextInternal(string.Format(queryString, formatParams), type, addCount, rights);
        }

        protected virtual string GetFullQueryTextInternal(string queryString, Type type, bool addCount = false, IUserApplicationRights rights = null)
        {
            var normalizedQueryString = NormalizeSqlQueryText(queryString);

            var result = normalizedQueryString;
            if (addCount)
            {
                result = AddRecordCountToQueryText(normalizedQueryString);
            }

            if (rights != null)
            {
                var rightsQueryString = GetRightsQueryString(type, rights);
                if (!string.IsNullOrEmpty(rightsQueryString))
                {
                    result = $"{result} and {rightsQueryString}";
                }
            }

            return result;
        }

        /// <summary>
        /// Adds to query texts conditions recived, converting it to FormattableString 
        /// Do not pass to the function string with {0} placeholder
        /// </summary>
        /// <param name="type"></param>
        /// <param name="queryText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract FormattableString AddConditionsToQueryText(Type type, string queryText, IDictionary<string, string> parameters);

        public abstract string GetSqlText(Type type);

        public abstract Task<string> GetSqlTextAsync(Type type);

        public abstract string NormalizeSqlQueryText(string queryText);

        public abstract string AddRecordCountToQueryText(string queryString);

        public abstract string GetRightsQueryString(Type type, IUserApplicationRights rights);
       
    }
}
