using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Core.Common.Extensions;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Services
{
    public class PostgresQueryConditionsBuilder: IQueryConditionsBuilder
    {
        private static readonly Dictionary<Type, string> _supportedCasts = new Dictionary<Type, string>();
        private readonly Dictionary<Type, Dictionary<PropertyInfo, CaseFilterAttribute>> _typeProperties = 
                    new Dictionary<Type, Dictionary<PropertyInfo, CaseFilterAttribute>>();

        static PostgresQueryConditionsBuilder()
        {
            _supportedCasts[typeof(string)] = "";
            _supportedCasts[typeof(DateTime)] = "timestamp";
            _supportedCasts[typeof(DateTime?)] = "timestamp";
            _supportedCasts[typeof(Guid)] = "uuid";
            _supportedCasts[typeof(Guid?)] = "uuid";
            _supportedCasts[typeof(bool)] = "boolean";
            _supportedCasts[typeof(short)] = "smallint";
            _supportedCasts[typeof(int)] = "integer";
            _supportedCasts[typeof(long)] = "bigint";
            _supportedCasts[typeof(float)] = "real";
            _supportedCasts[typeof(double)] = "double precision";
            _supportedCasts[typeof(decimal)] = "numeric";
            _supportedCasts[typeof(string)] = "text";
        }

        public FormattableString GetQueryConditionsString(Type type, IDictionary<string, string> parameters, string queryAlias)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return $"";
            }

            if (!_typeProperties.TryGetValue(type, out var properties))
            {
                properties = type.GetProperties()
                   .Where(p => p.IsDefined(typeof(CaseFilterAttribute), true))
                   .ToDictionary(p => p,
                                 p => (CaseFilterAttribute)p.GetCustomAttributes(typeof(CaseFilterAttribute), true).First());
                _typeProperties.Add(type, properties);
            }

            var cases = FillSearchCaseList(properties, parameters);
            var conditions = GetStringConditions(cases, queryAlias);

            return conditions;
        }

        

        private class SearchCase
        {
            public string PropName { get; set; }
            public string InputName { get; set; }
            public string Value { get; set; }
            public PropertyInfo PropertyInfo { get; set; }
            public CaseFilterAttribute PredicateCase { get; set; }
        }

        private List<SearchCase> FillSearchCaseList(Dictionary<PropertyInfo, CaseFilterAttribute> properties,
                IDictionary<string, string> parameters)
        {
            var cases = new List<SearchCase>();
            foreach (var item in parameters) {

                if (string.IsNullOrEmpty(item.Key) || item.Key == "X-Requested-With") {
                    continue;
                }

                var sc = new SearchCase
                {
                    PropName = item.Key.ToLower(),
                    InputName = item.Key.ToLower(),
                    Value = item.Value
                };

                if (sc.PropName.EndsWith("_from")) {
                    sc.PropName = sc.PropName.Remove(sc.PropName.LastIndexOf("_from"));
                } else if (sc.PropName.EndsWith("_to")) {
                    sc.PropName = sc.PropName.Remove(sc.PropName.LastIndexOf("_to"));
                }

                sc.PropertyInfo = properties.Keys.FirstOrDefault(x => x.Name.ToLower() == sc.PropName.ToLower());
                if (sc.PropertyInfo != null && properties.TryGetValue(sc.PropertyInfo, out var pcase)) {
                    sc.PredicateCase = pcase;
                    sc.PropName = sc.PropertyInfo.Name;
                    cases.Add(sc);
                }
            }

            return cases;
        }

        private FormattableString GetStringConditions(List<SearchCase> cases, string queryAlias)
        {
            var currentConditions = new FormatStringWithParameters { Format = "", Parameters = new ArrayList(cases.Count * 2) };
            AddEqualsStringConditions(currentConditions, cases, queryAlias);
            AddContainsStringConditions(currentConditions, cases, queryAlias);
            AddValueRangeStringConditions(currentConditions, cases, queryAlias);
            AddInputRangeStringConditions(currentConditions, cases, queryAlias);
            AddConditionStringConditions(currentConditions, cases, queryAlias);
            AddOverlapsStringConditions(currentConditions, cases, queryAlias);
            AddFullTextSearchStringConditions(currentConditions, cases, queryAlias);

            return FormattableStringFactory.Create(currentConditions.Format, currentConditions.Parameters.ToArray());
        }

        private void AddEqualsStringConditions(FormatStringWithParameters conditions, List<SearchCase> cases, string queryAlias)
        {
            foreach (var item in cases.Where(x => x.PredicateCase.Operation == CaseFilterOperation.Equals)) {
                var field = GetFieldNameWithAlias(item.PropName, queryAlias);
                // if string has format "[value1,value2]", then we use 'in' statement, splitting the string to separate values
                if (item.Value != null && item.Value.StartsWith("[") && item.Value.EndsWith("]"))
                {
                    var valueToSplit = item.Value.Remove(0, 1);
                    valueToSplit = valueToSplit.Remove(valueToSplit.Length - 1, 1);
                    var values = valueToSplit.Split(',');
                    var placeHoldersList = new List<string>(values.Length);
                    // transform values
                    for (var i = 0; i < values.Length; i ++)
                    {
                        if (!string.IsNullOrEmpty(values[i]))
                        {
                            var valuePlaceholder = CreateValuePlaceholderWithCast(item.PropertyInfo.PropertyType, conditions.Parameters.Count);
                            placeHoldersList.Add(valuePlaceholder);
                            conditions.Parameters.Add(values[i]);
                        }
                        
                    }
                    var placeHolders = string.Join(",", placeHoldersList);
                    var conditionString = $"({field} in ({placeHolders}))";
                    conditions.Format = ConcatStringsWithAnd(conditions.Format, conditionString);
                }
                else
                {
                    if (item.Value == null)
                    {
                        var conditionString = $"({field} is null)";
                        conditions.Format = ConcatStringsWithAnd(conditions.Format, conditionString);
                    }
                    else
                    {
                        var valuePlaceholder = CreateValuePlaceholderWithCast(item.PropertyInfo.PropertyType, conditions.Parameters.Count);
                        var conditionString = $"({field} = {valuePlaceholder})";
                        conditions.Parameters.Add(item.Value);
                        conditions.Format = ConcatStringsWithAnd(conditions.Format, conditionString);
                    }
                }
            }
        }

        private void AddContainsStringConditions(FormatStringWithParameters conditions, List<SearchCase> cases, string queryAlias)
        {
            foreach (var item in cases.Where(x => x.PredicateCase.Operation == CaseFilterOperation.Contains && x.Value != null)) {
                var field = GetFieldNameWithAlias(item.PropName, queryAlias);
                var valuePlaceholder = CreateValuePlaceholderWithCast(item.PropertyInfo.PropertyType, conditions.Parameters.Count);
                var conditionString = $"(POSITION({valuePlaceholder} in LOWER({field})) > 0)";

                conditions.Parameters.Add(item.Value.ToLower());
                conditions.Format = ConcatStringsWithAnd(conditions.Format, conditionString);
            }
        }

        private void AddValueRangeStringConditions(FormatStringWithParameters conditions, List<SearchCase> cases, string queryAlias)
        {
            foreach (var item in cases.Where(x => x.PredicateCase.Operation == CaseFilterOperation.ValueRange && x.Value != null)) {
                string[] dates;

                if (item.Value.Contains('&')) {
                    dates = item.Value.Split('&');
                    dates[0] = DateTime.Parse(dates[0], null, System.Globalization.DateTimeStyles.RoundtripKind).ToString();
                    dates[1] = DateTime.Parse(dates[1], null, System.Globalization.DateTimeStyles.RoundtripKind).ToString();
                }
                else {
                    dates = item.Value.Split('-');
                }

                var field = GetFieldNameWithAlias(item.PropName, queryAlias);
                var firstValuePlaceholder = CreateValuePlaceholderWithCast(item.PropertyInfo.PropertyType, conditions.Parameters.Count);
                conditions.Parameters.Add(dates[0]);
                var secondValuePlaceholder = CreateValuePlaceholderWithCast(item.PropertyInfo.PropertyType, conditions.Parameters.Count);
                conditions.Parameters.Add(dates[1]);

                conditions.Format = ConcatStringsWithAnd(conditions.Format, $"({field} >= {firstValuePlaceholder})");
                conditions.Format = ConcatStringsWithAnd(conditions.Format, $"({field} <= {secondValuePlaceholder})");
            }
        }

        private void AddInputRangeStringConditions(FormatStringWithParameters conditions, List<SearchCase> cases, string queryAlias)
        {
            foreach (var item in cases.Where(x => x.PredicateCase.Operation == CaseFilterOperation.InputRange && x.Value != null)) {
                string oper;

                if (item.InputName.EndsWith("_from")) {
                    oper = ">=";
                } else if (item.InputName.EndsWith("_to")) {
                    oper = "<=";
                } else {
                    oper = "=";
                }

                if (oper != "=" && item.Value == null)
                {
                    continue;
                }
                var field = GetFieldNameWithAlias(item.PropName, queryAlias);
                if (DateTime.TryParse(item.Value, out DateTime date))
                {
                    //TODO: try to find a way to replace 'europe/kiev' with correct time zone (perhaps should use offset?)
                    field = $"({field}) at time zone 'utc' at time zone 'europe/kiev'";
                }
                var valuePlaceholder = CreateValuePlaceholderWithCast(item.PropertyInfo.PropertyType, conditions.Parameters.Count);
                conditions.Parameters.Add(item.Value);
                conditions.Format = ConcatStringsWithAnd(conditions.Format, $"({field} {oper} {valuePlaceholder})");
            }
        }

        private void AddConditionStringConditions(FormatStringWithParameters conditions, List<SearchCase> cases, string queryAlias)
        {
            foreach (var item in cases.Where(x => x.PredicateCase.Operation == CaseFilterOperation.Condition && x.Value != null)) {
                if (!string.IsNullOrEmpty(item.PredicateCase.Condition)) {
                    var valuePlaceholder = CreateValuePlaceholderWithCast(item.PropertyInfo.PropertyType, conditions.Parameters.Count);
                    var conditionString = item.PredicateCase.Condition.Replace("#item", queryAlias).Replace("#value", valuePlaceholder);
                    conditions.Parameters.Add(item.Value);
                    conditions.Format = ConcatStringsWithAnd(conditions.Format, conditionString);
                }
            }
        }

        private void AddOverlapsStringConditions(FormatStringWithParameters conditions, List<SearchCase> cases, string queryAlias)
        {
            var op_overlaps = cases.Where(x => x.PredicateCase.Operation == CaseFilterOperation.Overlaps && x.Value != null).ToArray();
            if (op_overlaps.Length > 0) {
                var groups = op_overlaps.GroupBy(x => x.PredicateCase.Group)
                    .Where(g => g.Count() == 2);
                foreach (var g in groups) {
                    var group = g.ToArray();
                    var field1 = GetFieldNameWithAlias(group[0].PropName, queryAlias);
                    var field2 = GetFieldNameWithAlias(group[1].PropName, queryAlias);

                    var firstValuePlaceholder = CreateValuePlaceholderWithCast(group[0].PropertyInfo.PropertyType, conditions.Parameters.Count);
                    conditions.Parameters.Add(group[0].Value);
                    var secondValuePlaceholder = CreateValuePlaceholderWithCast(group[1].PropertyInfo.PropertyType, conditions.Parameters.Count);
                    conditions.Parameters.Add(group[1].Value);

                    //TODO: try to find a way to replace 'europe/kiev' with correct time zone (perhaps should use offset?)
                    if (DateTime.TryParse(group[0].Value, out DateTime date))
                    {
                        firstValuePlaceholder = $"({firstValuePlaceholder}) at time zone 'europe/kiev'";
                    }

                    if (DateTime.TryParse(group[1].Value, out DateTime date2))
                    {
                        secondValuePlaceholder = $"({secondValuePlaceholder}) at time zone 'europe/kiev'";
                    }
                    var conditionString = $"(({field1}, {field2}) overlaps ({firstValuePlaceholder}, {secondValuePlaceholder}))";
                    conditions.Format = ConcatStringsWithAnd(conditions.Format, conditionString);
                }
            }
        }

        private void AddFullTextSearchStringConditions(FormatStringWithParameters conditions, List<SearchCase> cases, string queryAlias)
        {
            var fullTextSearchs = cases.Where(x => x.PredicateCase.Operation == CaseFilterOperation.FullTextSearch);

            foreach (var item in fullTextSearchs)
            {
                var field = GetFieldNameWithAlias(item.PropName, queryAlias);
                var fieldVecor = $"{field}_vector";

                var values = item.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                item.Value = $"{string.Join('&', values)}:*";

                var valuePlaceholder = CreateValuePlaceholderWithCast(item.PropertyInfo.PropertyType, conditions.Parameters.Count);
                var conditionString = $"{fieldVecor} @@ to_tsquery({valuePlaceholder})";

                conditions.Parameters.Add(item.Value.ToLower());
                conditions.Format = ConcatStringsWithAnd(conditions.Format, conditionString);
            }
        }

        private string ConcatStringsWithAnd(string first, string second)
        {
            string result;
            if (string.IsNullOrEmpty(first))
            {
                result = second;
            }
            else
            {
                result = $"{first} and {second}";
            }

            return result;
        }

        private string CreateValuePlaceholderWithCast(Type type, int parameterIndex)
        {
            var typeToCast = type;
            if (type.IsEnum)
            {
                typeToCast = type.GetEnumUnderlyingType();
            }

            if (!_supportedCasts.TryGetValue(typeToCast, out var castTo) || string.IsNullOrEmpty(castTo))
            {
                return "{" + parameterIndex + "}";
            }
            else {
                return "cast({" + parameterIndex + "} as " + castTo + ")";
            }
        }

        private string GetFieldNameWithAlias(string fieldName, string queryAlias)
        {
            return string.IsNullOrWhiteSpace(queryAlias) ? fieldName.ToSnakeCase() : queryAlias + "." + fieldName.ToSnakeCase();
        }

        private class FormatStringWithParameters
        {
            public string Format { get; set; }
            public ArrayList Parameters { get; set; }
        }
    }
}
