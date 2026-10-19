using WeatherTracker.DTO;

namespace WeatherTracker.Service
{
    public interface IGeoCodingService
    {
        Task<List<CityResponseDTO>> GetGeoCoordinateAsync(string city); 
    }
}
