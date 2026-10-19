using WeatherTracker.DTO;
using WeatherTracker.Models;

namespace WeatherTracker.Service
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetWeatherModelAsync(double lat, double lon);
        Task<WeatherResponseDTO> GetWeatherByCoordinatesAsync(double lat, double lon);
        Task<List<WeatherResponseDTO>> GetWeatherByCityAsync(string city);
        Task<SummaryResponseDTO> GetWeatherSummaryAsync(double lat, double lon);
        //Task<string> GetFavouriteCityAsync(string city);
       
    }
}
