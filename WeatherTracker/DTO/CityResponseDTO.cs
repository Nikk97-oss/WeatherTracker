using System.Text.Json.Serialization;

namespace WeatherTracker.DTO
{
    public class CityResponseDTO
    {
        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("lat")]
        public double latitude {  get; set; }

        [JsonPropertyName("lon")]
        public double longitude { get; set; }

        [JsonPropertyName("country")]
        public string country { get; set; }

        
    }
}
