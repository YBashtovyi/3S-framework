using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace App.Data.Dto.Common.NotMapped
{
    // TODO: Нужно переделать в нормальный вид (изучить Google API)
    public class MapCoordinate
    {
        [JsonProperty("mapLat")]
        [JsonPropertyName("mapLat")]
        public double? MapLat { get; set; }

        [JsonProperty("mapLng")]
        [JsonPropertyName("mapLng")]
        public double? MapLng { get; set; }

        [JsonProperty("lines")]
        [JsonPropertyName("lines")]
        public IList<MapLine> Lines { get; set; } = new List<MapLine>();

        [JsonProperty("polygons")]
        [JsonPropertyName("polygons")]
        public IList<MapPolygon> Polygons { get; set; } = new List<MapPolygon>();

        [JsonProperty("markers")]
        [JsonPropertyName("markers")]
        public IList<MapMarker> Markers { get; set; } = new List<MapMarker>();

        [JsonProperty("zoom")]
        [JsonPropertyName("zoom")]
        public int Zoom { get; set; }
    }

    public class MapLine
    {
        [JsonProperty("path")]
        [JsonPropertyName("path")]
        public IList<MapLatLng> Path { get; set; }

        [JsonProperty("color")]
        [JsonPropertyName("color")]
        public string Color { get; set; }
    }

    public class MapPolygon
    {
        [JsonProperty("path")]
        [JsonPropertyName("path")]
        public IList<MapLatLng> Path { get; set; }

        [JsonProperty("color")]
        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonProperty("fillColor")]
        [JsonPropertyName("fillColor")]
        public string FillColor { get; set; }
    }

    public class MapMarker
    {
        [JsonProperty("position")]
        [JsonPropertyName("position")]
        public MapLatLng Position { get; set; }

        [JsonProperty("infoWindow")]
        [JsonPropertyName("infoWindow")]
        public string InfoWindow { get; set; }
    }

    public class MapLatLng
    {
        [JsonProperty("lat"), Display(Name = "Широта")]
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng"), Display(Name = "Довгота")]
        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }
}
