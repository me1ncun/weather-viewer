using Newtonsoft.Json;

namespace WeatherBackend.DTO.Inserted
{
    public class Main
    {
        [JsonProperty("temp")]
        public float temp { get; set; }
        [JsonProperty("pressure")]
        public int pressure { get; set; }
        [JsonProperty("humidity")]
        public int humidity { get; set; }
    }
}
