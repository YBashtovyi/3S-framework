using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Administration;

namespace Core.Services.Data
{
    public interface IQueryTextService
    {
        FormattableString AddConditionsToQueryText(Type type, string queryText, IDictionary<string, string> parameters);
        string AddRecordCountToQueryText(string queryText);
        string GetFullQueryText(Type type, bool addCount = false, IUserApplicationRights rights = null, params object[] formatParams);
        Task<string> GetFullQueryTextAsync(Type type, bool addCount = false, IUserApplicationRights rights = null, params object[] formatParams);
        FormattableString GetParameterizedQueryString(Type type, IDictionary<string, string> parameters = null, bool addCount = false, IUserApplicationRights rights = null, params object[] formatParams);
        Task<FormattableString> GetParameterizedQueryStringAsync(Type type, IDictionary<string, string> parameters = null, bool addCount = false, IUserApplicationRights rights = null, params object[] formatParams);
        string GetRightsQueryString(Type type, IUserApplicationRights rights);
        string GetSqlText(Type type);
        Task<string> GetSqlTextAsync(Type type);
        string NormalizeSqlQueryText(string queryText);
    }
}
