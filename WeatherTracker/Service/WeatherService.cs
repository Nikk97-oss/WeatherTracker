using Microsoft.AspNetCore.Components.Infrastructure;
using System.ComponentModel;
using System.Text.Json;
using WeatherTracker.DTO;
using WeatherTracker.Models;

namespace WeatherTracker.Service
{

    public class WeatherService : IWeatherService
    {
        private HttpClient httpClient;
        private string _url;
        private IGeoCodingService geoCodingService;


        public WeatherService(HttpClient _httpClient, IGeoCodingService _geoCodingService)
        {
            httpClient = _httpClient;
            geoCodingService = _geoCodingService;
        }

        public async Task<WeatherModel> GetWeatherModelAsync(double lat, double lon)
        {
            _url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=bcf984be3d67824fb19d69bd83abfc49&units=metric&lang=it";
            HttpResponseMessage httpResponse = await httpClient.GetAsync(_url);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(httpResponse.StatusCode.ToString());
            }

            string response = await httpClient.GetStringAsync(_url);
            WeatherModel model = JsonSerializer.Deserialize<WeatherModel>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return model;
        }

        public async Task<WeatherResponseDTO> GetWeatherByCoordinatesAsync(double lat, double lon)
        {
            WeatherModel model = await GetWeatherModelAsync(lat, lon);
            WeatherResponseDTO weatherDTO = new WeatherResponseDTO();
            weatherDTO.city = model.Name;
            weatherDTO.country = model.Sys.country;
            foreach (Weather w in model.Weather)
            {
                weatherDTO.description.Add(w.description);
            }
            weatherDTO.tMin = model.Main.tempMin;
            weatherDTO.tMax = model.Main.tempMax;
            weatherDTO.feelslike = model.Main.feelslike;
            weatherDTO.temperatura = model.Main.temp;
            weatherDTO.humidity = model.Main.humidity;
            return weatherDTO;
        }

        //TODO: Da valutare, come nel controller se tenere o meno sta funzione.
        public async Task<List<WeatherResponseDTO>> GetWeatherByCityAsync(string city)
        {
            List<CityResponseDTO> coordinates = await geoCodingService.GetGeoCoordinateAsync(city);

            List<WeatherResponseDTO> responseList = new List<WeatherResponseDTO>();

            foreach (CityResponseDTO resp in coordinates)
            {
                responseList.Add(await GetWeatherByCoordinatesAsync(resp.latitude, resp.longitude));
            }

            return responseList;

        }

        public async Task<SummaryResponseDTO> GetWeatherSummaryAsync(double lat, double lon)
        {
            WeatherModel model = await GetWeatherModelAsync(lat, lon);
            SummaryResponseDTO summaryResponseDTO = new SummaryResponseDTO();
            summaryResponseDTO.city = model.Name;
            summaryResponseDTO.temperature = model.Main.temp;
            foreach (Weather w in model.Weather)
            {
                summaryResponseDTO.description.Add(w.description);
                summaryResponseDTO.icon.Add(w.icon);
            }

            return summaryResponseDTO;
            
        }

        //public async Task<WeatherResponseDTO> GetFavouriteCityAsync(string city)
        //{
        //    CityResponseDTO coords= (await geoCodingService.GetGeoCoordinateAsync(city)).First();
            
        //    return await GetWeatherByCoordinatesAsync(coords.latitude, coords.latitude);
        //}
        
    }
}


