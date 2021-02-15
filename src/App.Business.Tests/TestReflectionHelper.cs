using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace App.Business.Tests.Security
{
    public static class TestReflectionHelper
    {
        private static Type[] TypeCollection;
        private static readonly ConcurrentDictionary<string, Type[]> _typeCache = new ConcurrentDictionary<string, Type[]>();
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, MethodInfo>> _genericMethods = new ConcurrentDictionary<Type, ConcurrentDictionary<string, MethodInfo>>();

        public static IEnumerable<Type> TypeList
        {
            get
            {
                if (TypeCollection == null)
                {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                        .Where(el => el.FullName.StartsWith("App.Data")
                            || el.FullName.StartsWith("Core.Base")
                            || el.FullName.StartsWith("Core.Security")
                            || el.FullName.StartsWith("Core.Data."));
                    TypeCollection = assemblies
                        .SelectMany(t => t.GetTypes())
                        .Where(t => t.IsClass && t.Namespace != null)
                        .ToArray();
                }
                return TypeCollection;
            }
        }

        public static Type GetTypeByName(string typeName)
        {
            if (typeName == null)
            {
                throw new ArgumentNullException("Type name cannot be null");
            }

            if (!_typeCache.TryGetValue(typeName, out var types))
            {
                types = TypeList.Where(x => x.Name == typeName).ToArray();
                _typeCache.TryAdd(typeName, types);
            }
             
            if (types.Length == 1)
            {
                return types[0];
            }
            else if (types.Length == 0)
            {
                return null;
            }
            else
            {
                throw new AmbiguousMatchException($"There is more than one type that matches name {typeName}");
            }
        }

        public static IEnumerable<Type> GetPropertyGenericArguments(PropertyInfo[] infos, Type propertyType, int argumentNumber = 0)
        {
            if (argumentNumber < 0) {
                throw new ArgumentException("Argument " + nameof(argumentNumber) + " cannot be less than zero");
            };

            return infos
                .Where(prop => prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == propertyType)
                .Select(prop => prop.PropertyType.GetGenericArguments()[argumentNumber]);
        }

        public static object InvokeGenericMethod(object instance, Type genericType, string methodName, object[] parameters, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            var genericMethods = GetCachedTypeGenericMethods(genericType);

            var methodKey = methodName + ". " + bindingFlags;
            if (!genericMethods.TryGetValue(methodKey, out var method))
            {
                var nongeneric = GetNonGenericMethod(instance, methodName, bindingFlags);
                method = nongeneric?.MakeGenericMethod(genericType);
                genericMethods.TryAdd(methodKey, method);
            }

            return method.Invoke(instance, parameters);
        }

        /// <summary>
        /// Does not use cache. Use only if this is rare operation
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="genericType"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static object InvokeGenericMethod(object instance, Type[] genericTypes, string methodName, object[] parameters, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic)
        {
            var nongeneric = GetNonGenericMethod(instance, methodName, bindingFlags);
            var method = nongeneric?.MakeGenericMethod(genericTypes);

            return method.Invoke(instance, parameters);
        }

        private static MethodInfo GetNonGenericMethod(object instance, string methodName, BindingFlags bindingFlags)
        {
            var nongeneric = instance.GetType().GetMethod(methodName, bindingFlags);
            if (nongeneric == null)
            {
                var baseType = instance.GetType().BaseType;
                // check for method in all base types while do not reach object type or do not find the needed method
                while (baseType != null && nongeneric == null)
                {
                    nongeneric = baseType.GetMethod(methodName, bindingFlags);
                    baseType = baseType.BaseType;
                }
            }
            return nongeneric;
        }

        private static ConcurrentDictionary<string, MethodInfo> GetCachedTypeGenericMethods(Type type)
        {
            if (!_genericMethods.TryGetValue(type, out var genericMethods))
            {
                genericMethods = new ConcurrentDictionary<string, MethodInfo>();
                _genericMethods.TryAdd(type, genericMethods);
            }
            return genericMethods;
        }
    }
}
