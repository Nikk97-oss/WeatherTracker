using System.Text.Json;
using WeatherTracker.DTO;
using WeatherTracker.Models;

namespace WeatherTracker.Service
{
    public class GeoCodingService: IGeoCodingService
    {
        private HttpClient httpClient;
        private string _url;

        public GeoCodingService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<List<CityResponseDTO>> GetGeoCoordinateAsync(string city)
        {
            _url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=5&appid=bcf984be3d67824fb19d69bd83abfc49";
            HttpResponseMessage httpResponse = await httpClient.GetAsync(_url);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(httpResponse.StatusCode.ToString());
            }

            string response = await httpClient.GetStringAsync(_url);
            List<CityResponseDTO> cityResDTO = JsonSerializer.Deserialize<List<CityResponseDTO>>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return cityResDTO;
        }
    }
}
