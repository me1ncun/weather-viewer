using Newtonsoft.Json;

namespace WeatherBackend.DTO.Inserted
{
    public class Coord
    {
        [JsonProperty("lon")]
        public float Longtitude { get; set; }
        [JsonProperty("lat")]
        public float Latitude { get; set; }
    }
}
