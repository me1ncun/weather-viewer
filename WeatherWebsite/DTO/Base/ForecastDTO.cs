using System.Diagnostics.Eventing.Reader;
using System.Net;
using Newtonsoft.Json;
using WeatherFrontend.DTO.Inserted;

namespace WeatherFrontend.DTO.Base
{
    public class ForecastDTO
    {
        public Coord coord { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public Sys sys { get; set; }
        public string name { get; set; }
        public string CorrectName { get; set; }
        public List<Weather> weather { get; set; }
    }
}
