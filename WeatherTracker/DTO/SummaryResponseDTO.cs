namespace WeatherTracker.DTO
{
    public class SummaryResponseDTO
    {
        public string city { get; set; }
        public double temperature { get; set;  }
        public List<string> description { get; set; } = new List<string>();
        public List<string> icon { get; set; } = new List<string>();

    }
}
