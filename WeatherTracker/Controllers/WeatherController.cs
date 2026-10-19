using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WeatherTracker.DTO;
using WeatherTracker.Models;
using WeatherTracker.Service;

namespace WeatherTracker.Controllers
{
    [ApiController]
    [Route("Weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IGeoCodingService _geoCodingSerivce;
        List<FavouriteCityDTO> favouriteCity;

        public WeatherController(IWeatherService weatherService, IGeoCodingService geoCodingService)
        {
            _weatherService = weatherService;
            _geoCodingSerivce = geoCodingService;
            favouriteCity = new List<FavouriteCityDTO>();
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherByCoordinates([FromQuery] WeatherRequestDTO request)
        {
            if (request.latitude < -90 || request.latitude > 90)
            {
                return BadRequest("La latitudine dev'essere compresa tra -90 e 90");
            }
            if (request.longitude < -180 || request.longitude > 180)
            {
                return BadRequest("La longitudine dev'essere compresa tra -180 e 180");
            }
            try
            {
                WeatherResponseDTO response = await _weatherService.GetWeatherByCoordinatesAsync(request.latitude, request.longitude);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore durante il recupero dati");
            }
        }


        //TODO: Da valutare a questo punto se necessario questa Call qua, potrebbe anche non servire
        [HttpGet("{cityRequest}")]
        public async Task<IActionResult> GetWeatherByCity(CityRequestDTO cityRequest){
            try
            {
                List<WeatherResponseDTO> responseList= await _weatherService.GetWeatherByCityAsync(cityRequest.city);
                return Ok(responseList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore durante il recupero dati");
            }
        }



        [HttpGet("Summary")]
        public async Task<IActionResult> GetWeatherSummary([FromQuery] WeatherRequestDTO request){
            if (request.latitude < -90 || request.latitude > 90)
            {
                return BadRequest("La latitudine dev'essere compresa tra -90 e 90");
            }
            if (request.longitude < -180 || request.longitude > 180)
            {
                return BadRequest("La longitudine dev'essere compresa tra -180 e 180");
            }
            try
            {
                SummaryResponseDTO response = await _weatherService.GetWeatherSummaryAsync(request.latitude,request.longitude);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Errore durante il recupero dati");
            }
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchCity([FromQuery] CityRequestDTO cityRequest)
        {
            try
            {
                List<CityResponseDTO> cityRespList= await _geoCodingSerivce.GetGeoCoordinateAsync(cityRequest.city);
                return Ok(cityRespList);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Errore durante il recupero dati");
            }
        }

        [HttpGet("Favourites")]
        public async Task<IActionResult> GetFavourites()
        {
            if (favouriteCity.Count>0)
                return Ok(favouriteCity);
            else
                return BadRequest("Nessuna città tra i preferti");
        }

        [HttpDelete("Favourites")]
        public async Task<IActionResult> DeleteFavourites([FromBody] FavouriteCityDTO city)
        {
            if (city == null)
                return BadRequest("Dati mancanti");

            var favourite = favouriteCity.FirstOrDefault(c =>
                c.Latitude == city.Latitude &&
                c.Longitude == city.Longitude);

            if (favourite == null)
                return NotFound("Città non presente tra i preferiti");

            favouriteCity.Remove(favourite);
            return Ok(favourite);
        }

        [HttpPost("Favourites")]
        public async Task<IActionResult> AddFavouriteCity([FromBody] FavouriteCityDTO request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Dati mancanti");

                if (string.IsNullOrWhiteSpace(request.Name) ||
                    string.IsNullOrWhiteSpace(request.Country))
                    return BadRequest("Nome della città e paese obbligatori");

                if (request.Latitude < -90 || request.Latitude > 90 ||
                    request.Longitude < -180 || request.Longitude > 180)
                    return BadRequest("Coordinate non valide");

                if (!favouriteCity.Any(c =>
                c.Latitude == request.Latitude &&
                c.Longitude == request.Longitude))
                    favouriteCity.Add(request);
                else
                    return BadRequest("Città già presente tra i favoriti");


                return Ok(request);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Errore durante il recupero dati");
            }
        }


    }
}
