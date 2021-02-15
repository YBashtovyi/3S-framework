using System;
using System.Collections.Generic;
using System.Text;

namespace App.Business.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, Func<T, bool> predicate) 
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}
