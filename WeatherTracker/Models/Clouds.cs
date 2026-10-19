using System.Text.Json.Serialization;

namespace WeatherTracker.Models
{
    public class Clouds
    {
        [JsonPropertyName("all")]
        public int all {  get; set; }
    }
}
