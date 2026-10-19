using System.Text.Json.Serialization;

namespace WeatherTracker.Models
{
    public class Sys
    {
        [JsonPropertyName("type")]
        public int type {  get; set; }

        [JsonPropertyName("id")]
        public long id  { get; set; }

        [JsonPropertyName("country")]
        public string country { get; set; }

        [JsonPropertyName("sunrise")]
        public long sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public long sunset { get; set; }

    }
}
