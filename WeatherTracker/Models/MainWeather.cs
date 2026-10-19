using System.Text.Json.Serialization;

namespace WeatherTracker.Models
{
    public class MainWeather
    {
        [JsonPropertyName("temp")]
        public double temp {  get; set; }

        [JsonPropertyName("feels_like")]
        public double feelslike { get; set; }

        [JsonPropertyName("temp_Min")]
        public double tempMin { get; set; }

        [JsonPropertyName("temp_Max")]
        public double tempMax { get; set; }

        [JsonPropertyName("pressure")]
        public int pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int humidity { get; set; }

        [JsonPropertyName("sea_level")]
        public int seaLevel { get; set; }

        [JsonPropertyName("grnd_level")]
        public int grndLevel { get; set; }
    }
}
