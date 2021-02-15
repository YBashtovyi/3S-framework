using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Administration;
using Core.Business.Services;
using Microsoft.Extensions.Logging;

namespace Core.Data.Helpers
{
    public class MssqlQueryTextService : QueryTextService
    {
        //private readonly IQueryConditionsBuilder _queryConditionsHelper;
        private readonly ILogger<MssqlQueryTextService> _logger;
        private static readonly ConcurrentDictionary<Type, string> _texts = new ConcurrentDictionary<Type, string>();
        private static readonly string _queryAlias = "qry";
        private static readonly string _conditionsPlaceholder = "where (0 = 0)";
        private static readonly string _startOfQuery = "select * from";

        //public MssqlQueryTextService(IQueryConditionsBuilder queryConditionsHelper, ILogger<MssqlQueryTextService> logger)
        //{
        //    _queryConditionsHelper = queryConditionsHelper;
        //    _logger = logger;
        //}

        public MssqlQueryTextService(ILogger<MssqlQueryTextService> logger)
        {
            _logger = logger;
        }

        public override FormattableString AddConditionsToQueryText(Type type, string queryText, IDictionary<string, string> parameters)
        {
            // not implemented yet
            return FormattableStringFactory.Create(queryText, new object[0]);
        }

        public override string AddRecordCountToQueryText(string queryString)
        {
            var queryText = queryString.ToString();
            var firstSelectOccurrence = queryText.IndexOf(_startOfQuery);
            var startOfFinalQuery = "select *, count(*) over() as total_record_count from";
            var endOfFinalQuery = queryText.Substring(firstSelectOccurrence + _startOfQuery.Length);
            var resultString = $"{startOfFinalQuery} {endOfFinalQuery}";

            return resultString;
        }

        public override string GetRightsQueryString(Type type, IUserApplicationRights rights)
        {
            // not implemented yet
            return string.Empty;
        }

        public override string GetSqlText(Type type)
        {
            if (_texts.TryGetValue(type, out var result))
            {
                return result;
            }

            using (var stream = GetResourceStream(type))
            {
                using var reader = new StreamReader(stream);
                result = reader.ReadToEnd();
            }

            _texts.TryAdd(type, result);
            return result;
        }

        public override async Task<string> GetSqlTextAsync(Type type)
        {
            if (_texts.TryGetValue(type, out var result))
            {
                return result;
            }

            using (var stream = GetResourceStream(type))
            {
                using var reader = new StreamReader(stream);
                result = await reader.ReadToEndAsync();
            }

            _texts.TryAdd(type, result);

            return result;
        }

        public override string NormalizeSqlQueryText(string queryText)
        {
            var resultString = queryText;
            var endQueryPlaceHolder = $"as {_queryAlias} {_conditionsPlaceholder}";
            var needsWrapping = !queryText.Contains(_startOfQuery) || !queryText.Contains(endQueryPlaceHolder);
            if (needsWrapping)
            {
                resultString = $"{_startOfQuery} ({resultString}) {endQueryPlaceHolder}";
            }

            return resultString;
        }

        /// <summary>
        /// Gets stream for sql file by type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="currentType"></param>
        /// <returns></returns>
        private Stream GetResourceStream(Type type, Type currentType = null)
        {
            var neededType = currentType ?? type;

            var assembly = Assembly.GetAssembly(neededType);
            var fileName = neededType.Namespace + ".mssql." + neededType.Name + ".sql";

            var filePath = assembly.GetManifestResourceNames()
                .FirstOrDefault(r => r.ToLowerInvariant() == fileName.ToLowerInvariant());

            if (!string.IsNullOrEmpty(filePath))
            {
                _logger.LogInformation("Resource file with name {0} for type {1} was found", fileName, type.Name);
                return assembly.GetManifestResourceStream(filePath);
            }
            else if (neededType.BaseType == typeof(object))
            {
                _logger.LogWarning("Cannot find resource file {0} for type {1}", fileName, neededType);
                throw new Exception($"Resource file for {type.Name} not found! If file does exist check that 'Embedded resource' property is set");
            }

            _logger.LogWarning("Cannot find resource file {0} for type {1}. Trying to get resource for base type {2}", fileName, neededType, neededType.BaseType);
            return GetResourceStream(type, neededType.BaseType);
        }
    }
}
