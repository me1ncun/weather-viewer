using Newtonsoft.Json;

namespace WeatherFrontend.DTO.Inserted
{
    public class Wind
    {
        [JsonProperty("speed")]
        public float Speed { get; set; }
        [JsonProperty("deg")]
        public int Deg { get; set; }
    }
}
