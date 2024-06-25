using Newtonsoft.Json;
using WeatherFrontend.DTO.Inserted;

namespace WeatherFrontend.DTO.Base;

public class ForecastHourlyDTO
{
    public List<Weather> weather { get; set; }
    public Main main { get; set; }
    public string dt_txt { get; set; }
}