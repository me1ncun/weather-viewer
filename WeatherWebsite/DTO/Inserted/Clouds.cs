using Newtonsoft.Json;

namespace WeatherBackend.DTO.Inserted
{
    public class Clouds
    {
        [JsonProperty("all")]
        public string All { get; set; }
    }
}
