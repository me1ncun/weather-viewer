using System.Diagnostics.Eventing.Reader;
using System.Net;
using Newtonsoft.Json;
using WeatherBackend.DTO.Inserted;

namespace WeatherBackend.DTO.Base
{
    public class ForecastDTO
    {
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public Sys sys { get; set; }
        public string name { get; set; }
    }
}
