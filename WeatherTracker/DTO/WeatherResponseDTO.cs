namespace WeatherTracker.DTO
{
    public class WeatherResponseDTO
    {
        public string city { get; set; }
        public string country { get; set; }
        public double temperatura { get; set; }
        public double tMin { get; set; }
        public double tMax { get; set; }
        public double feelslike { get; set; }
        public int humidity { get; set; }
        public List<string> description { get; set; } = new List<string>();
    }

}