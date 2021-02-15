using System;
using System.Reflection;

namespace Core.Common.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static Type GetPropertyGenericArgumentType(this PropertyInfo info, int argumentNumber = 0)
        {
            if (argumentNumber < 0) {
                throw new ArgumentException("Argument " + nameof(argumentNumber) + " cannot be less than zero");
            };

            if (!info.PropertyType.IsGenericType) {
                throw new ArgumentException("Property " + info.Name + " is not generic");
            }

            return info.PropertyType.GetGenericArguments()[argumentNumber];
        }
    }
}
