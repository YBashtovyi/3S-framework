using System;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;

namespace Core.ThirdParty.Redis
{
    public static class IDatabaseExtensions
    {
        public static T GetValueFromString<T>(this IDatabase source, string key)
        {
            var stringValue = source.StringGet(key).ToString();
            if (string.IsNullOrEmpty(stringValue))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(stringValue);
        }

        public static void SaveValueAsString(this IDatabase source, string key, object value, TimeSpan? expiry = null)
        {
            var stringValue = JsonSerializer.Serialize(value);
            source.StringSet(key, stringValue, expiry);
        }

        public static async Task<T> GetValueFromStringAsync<T>(this IDatabase source, string key)
        {
            var value = await source.StringGetAsync(key);
            var stringValue = value.ToString();
            if (string.IsNullOrEmpty(stringValue))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(stringValue);
        }

        public static async Task SaveValueAsStringAsync(this IDatabase source, string key, object value, TimeSpan? expiry = null)
        {
            var stringValue = JsonSerializer.Serialize(value);
            await source.StringSetAsync(key, stringValue, expiry);
        }

        public static bool ClearKey(this IDatabase source, string key)
        {
            return source.KeyDelete(key);
        }
    }
}
