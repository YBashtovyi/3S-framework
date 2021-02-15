using System.Collections.Generic;

namespace App.Business.Extensions
{
    /// <summary>
    /// Extensions for System.Collections.Generic.Dictionary
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Concatenates two sequences.
        /// </summary>
        /// <typeparam name="TKey">The type of key of the sequences.</typeparam>
        /// <typeparam name="TValue">The type of value of the sequences.</typeparam>
        /// <param name="first">first sequence</param>
        /// <param name="second">second sequence</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> Concat<TKey, TValue>(this IDictionary<TKey, TValue> first, Dictionary<TKey, TValue> second)
            where TKey : class
            where TValue : class
        {
            var result = new Dictionary<TKey, TValue>();
            foreach (var item in first)
            {
                result.Add(item.Key, item.Value);
            }
            foreach (var item in second)
            {
                result.Add(item.Key, item.Value);
            }
            return result;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the System.Collections.Generic.Dictionary.
        /// </summary>
        /// <typeparam name="TKey">type of key of the sequences.</typeparam>
        /// <typeparam name="TValue">type of value of the sequences.e</typeparam>
        /// <param name="source">source sequence</param>
        /// <param name="collection">elements of the specified collection wich need to add</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source, Dictionary<TKey, TValue> collection)
            where TKey : class
            where TValue : class
        {
            foreach (var item in collection)
            {
                source.Add(item.Key, item.Value);
            }
        }
    }
}
