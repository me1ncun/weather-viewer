using Newtonsoft.Json;

namespace WeatherBackend.DTO.Inserted
{
    public class Wind
    {
        [JsonProperty("speed")]
        public float Speed { get; set; }
        [JsonProperty("deg")]
        public int Deg { get; set; }
    }
}
