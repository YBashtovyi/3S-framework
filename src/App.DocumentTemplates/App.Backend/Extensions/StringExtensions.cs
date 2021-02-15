using System;
using System.Collections.Generic;
using System.Text;

namespace App.DocumentTemplates.Extensions
{
    public static class StringExtensions
    {
        public static T? GetValueOrNull<T>(this string valueAsString) where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
            {
                return null;
            }

            return (T)Convert.ChangeType(valueAsString, typeof(T));
        }
    }
}
