using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Core.Base.Exceptions;

namespace Core.Common.Extensions
{
    public static class QueryableExtensions
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<string, string>> _names = new ConcurrentDictionary<Type, Dictionary<string, string>>();

        public static IQueryable<TDto> AddOrderBy<TDto>(this IQueryable<TDto> query, Func<IQueryable<TDto>, IOrderedQueryable<TDto>> orderBy) where TDto : class
        {
            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        public static IQueryable<TDto> AddOrderBy<TDto>(this IQueryable<TDto> query, string orderBy) where TDto : class
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                return query.OrderBy(orderBy);
            }

            return query;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortExpression) where T : class
        {
            var index = 0;
            var sortingNames = sortExpression.Split(',');
            IQueryable<T> result = null;
            foreach (var item in sortingNames)
            {
                var sortingFunction = index++ > 0 ? "ThenBy" : "OrderBy";
                if (item.StartsWith("-"))
                {
                    var fieldName = GetRealFieldName(typeof(T), item.Substring(1));
                    if (!string.IsNullOrWhiteSpace(fieldName))
                    {
                        sortingFunction += "Descending";
                        sortExpression = fieldName;
                    }
                }
                else
                {
                    sortExpression = GetRealFieldName(typeof(T), item);
                }
                var intermidateSource = result ?? source;
                var methodCall = GenerateMethodCall<T>(intermidateSource, sortingFunction, sortExpression.TrimStart());
                result = intermidateSource.Provider.CreateQuery<T>(methodCall);
            }

            return result ?? source;
        }

        private static string GetRealFieldName(Type type, string fieldName)
        {
            // if there is no config for Consultation
            if (!_names.TryGetValue(type, out var config))
            {
                config = new Dictionary<string, string>();
                var result = _names.TryAdd(type, config);
            }

            // if there is no field name for patientCardCaption (item)
            var lowerName = fieldName.ToLower();
            if (!config.TryGetValue(lowerName, out var realFieldName))
            {
                var field = type.GetProperties().Where(p => p.Name.ToLower() == lowerName).FirstOrDefault();
                if (field == null)
                {
                    throw new WrongFieldException($"Error occurred when trying get real model field with name {fieldName} in type {type.ToString()}");
                }
                
                realFieldName = field.Name; // PatientCardCaption
                config.Add(lowerName, realFieldName);
            }

            return realFieldName ?? "";
        }

        private static MethodCallExpression GenerateMethodCall<T>(IQueryable<T> source, string methodName, string fieldName) where T : class
        {
            var type = typeof(T);
            var selector = GenerateSelector<T>(fieldName, out var selectorResultType);
            var resultExp = Expression.Call(typeof(Queryable), methodName,
                                            new Type[] { type, selectorResultType },
                                            source.Expression, Expression.Quote(selector));
            return resultExp;
        }

        private static LambdaExpression GenerateSelector<T>(string propertyName, out Type resultType) where T : class
        {
            // Create a parameter to pass into the Lambda expression (x => x.OrderByField).
            var parameter = Expression.Parameter(typeof(T), "x");
            //  create the selector part, but support child properties
            PropertyInfo property;
            Expression propertyAccess;
            if (propertyName.Contains('.'))
            {
                // support to be sorted on child fields.
                var childProperties = propertyName.Split('.');
                property = typeof(T).GetProperty(childProperties[0]);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
                for (var i = 1; i < childProperties.Length; i++)
                {
                    property = property.PropertyType.GetProperty(childProperties[i]);
                    propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                }
            }
            else
            {
                property = typeof(T).GetProperty(propertyName);
                if (property == null)
                {
                    throw new ApplicationException($"Property with name {propertyName} does not exist for type {typeof(T).Name}");
                }
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }
            resultType = property.PropertyType;
            // Create the order by expression.
            return Expression.Lambda(propertyAccess, parameter);
        }
    }
}
