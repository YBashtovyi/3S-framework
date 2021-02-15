using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Data.Converters
{
    public class OneSignalCreateErrorJsonConverter: JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(JObject) || objectType == typeof(IEnumerable<string>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var result = new List<string>();

            if (token.Type == JTokenType.Object)
            {
                var jObject = token.ToObject<JObject>();

                if (jObject["invalid_external_user_ids"] != null)
                {
                    var externalUserIds = jObject["invalid_external_user_ids"].ToObject<JArray>();
                    result.Add("Invalid external user ids");

                    foreach (var item in externalUserIds)
                    {
                        result.Add(item.ToString());
                    }
                }
                return result;
            }

            return token.ToObject<IEnumerable<string>>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
