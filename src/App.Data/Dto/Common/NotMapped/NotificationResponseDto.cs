using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using App.Data.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.Data.Dto.Common.NotMapped
{
    public class NotificationCreateResponseDto
    {
        [JsonPropertyName("id")]
        [JsonProperty(PropertyName = "id")]
        public Guid? Id { get; set; }

        [JsonPropertyName("external_id")]
        [JsonProperty(PropertyName = "external_id")]
        public Guid? ExternalId { get; set; }

        [JsonPropertyName("errors")]
        [JsonProperty(PropertyName = "errors")]
        [Newtonsoft.Json.JsonConverter(typeof(OneSignalCreateErrorJsonConverter))]
        public List<string> Errors { get; set; }
    }
}
