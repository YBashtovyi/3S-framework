using Core.Business.Services;
using Core.Common.Extensions;
using Core.Security;
using Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Administration;
using Core.Base.Administration;

namespace Core.Data.Helpers
{
    public class PostgresQueryTextService: QueryTextService
    {
        private readonly IQueryConditionsBuilder _queryConditionsHelper;
        private readonly ILogger<PostgresQueryTextService> _logger;
        private static readonly ConcurrentDictionary<Type, string> _texts = new ConcurrentDictionary<Type, string>();
        private static readonly string _queryAlias = "qry";
        private static readonly string _conditionsPlaceholder = "where (true)";
        private static readonly string _startOfQuery = "select * from";
        private static readonly bool _useSnakeCase = true;

        public PostgresQueryTextService(IQueryConditionsBuilder queryConditionsHelper, ILogger<PostgresQueryTextService> logger)
        {
            _queryConditionsHelper = queryConditionsHelper;
            _logger = logger;
        }

        /// <summary>
        /// Adds to query texts conditions recived, converting it to FormattableString 
        /// Do not pass to the function string with {0} placeholder
        /// </summary>
        /// <param name="type"></param>
        /// <param name="queryText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override FormattableString AddConditionsToQueryText(Type type, string queryText, IDictionary<string, string> parameters)
        {
            FormattableString result;
            var conditions = _queryConditionsHelper.GetQueryConditionsString(type, parameters, _queryAlias);
            if (string.IsNullOrEmpty(conditions.Format))
            {
                result = FormattableStringFactory.Create(queryText, new object[0]);
            }
            else
            {
                var conditionsOccurence = queryText.CountStringOccurrences(_conditionsPlaceholder);
                if (conditionsOccurence == 0)
                {
                    result = FormattableStringFactory.Create(queryText, new object[0]);
                }
                else
                {
                    var tempString = queryText.Replace(_conditionsPlaceholder, "where " + conditions.Format);
                    result = FormattableStringFactory.Create(tempString, conditions.GetArguments());
                }
            }

            return result;
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
            var rightsQueryString = "";
            var rls = rights.GetTypeFieldsRlsRights(type);
            foreach (var right in rls)
            {
                switch (right.AccessLevel)
                {
                    case CrudOperation.None:
                        // if access by this field is denied then any other conditions do not make sense
                        return "false";
                    default:
                    {
                        if ((right.AccessLevel & CrudOperation.A) != 0 || (right.AccessLevel & CrudOperation.R) != 0 || (right.AccessLevel & CrudOperation.B) != 0)
                        {
                            // Contain Role - GLOBAL_ADMIN
                            if ((right.AccessLevel & CrudOperation.A) != 0 && !right.Entities.Any())
                            {
                                return "";
                            }

                            var idsString = "";
                            foreach (var id in right.Entities)
                            {
                                if (string.IsNullOrEmpty(idsString))
                                {
                                    idsString += "(";
                                }
                                else
                                {
                                    idsString += ", ";
                                }
                                idsString += "'" + id.ToString() + "'";
                            }

                            if (!string.IsNullOrEmpty(idsString))
                            {
                                idsString += ")";
                            }

                            if ((right.AccessLevel & CrudOperation.R) != 0 || (right.AccessLevel & CrudOperation.A) != 0)
                            {
                                idsString = "in " + idsString;
                            }
                            else // ban
                            {
                                idsString = "not in " + idsString;
                            }

                            var fieldName = right.Name;
                            if (_useSnakeCase)
                            {
                                fieldName = fieldName.ToSnakeCase();
                            }
                            var startOfString = string.IsNullOrEmpty(_queryAlias) ? "" : _queryAlias + "." + fieldName;
                            idsString = startOfString + " " + idsString;
                            if (string.IsNullOrEmpty(rightsQueryString))
                            {
                                rightsQueryString += idsString;
                            }
                            else
                            {
                                rightsQueryString += " and " + idsString;
                            }

                        }

                        break;
                    }
                }
            }
            return rightsQueryString;
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
            var fileName = neededType.Namespace + ".pgsql." + neededType.Name + ".sql";

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
