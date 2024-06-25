using Newtonsoft.Json;

namespace WeatherFrontend.DTO.Inserted
{
    public class Sys
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }
        [JsonProperty("sunset")]
        public string Sunset { get; set; }
    }
}
