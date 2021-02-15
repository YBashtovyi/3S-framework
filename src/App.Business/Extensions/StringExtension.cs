using System;
using System.Linq;

namespace App.Business.Extensions
{
    public static class StringExtension
    {
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length == 1)
                {
                    return str.ToUpperInvariant();
                }
                if (str.Length > 1)
                {
                    return char.ToUpperInvariant(str[0]) + str.Substring(1).ToLowerInvariant();
                }
            }
            return str;
        }

        public static string FirstCharToLower(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToLower() + input.Substring(1),
            };
        }

        public static string FirstCharToUpper(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1),
            };
        }
    }
}
