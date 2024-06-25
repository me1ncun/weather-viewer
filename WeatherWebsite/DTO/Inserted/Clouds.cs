using Newtonsoft.Json;

namespace WeatherFrontend.DTO.Inserted
{
    public class Clouds
    {
        [JsonProperty("all")]
        public string All { get; set; }
    }
}
