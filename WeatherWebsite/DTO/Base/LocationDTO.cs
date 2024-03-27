using Newtonsoft.Json;
using WeatherBackend.DTO.Inserted;

namespace WeatherBackend.DTO.Base
{
    public class LocationDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("lat")]
        public float Latitude { get; set; }
        [JsonProperty("lon")]
        public float Longtitude { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
    }
}
