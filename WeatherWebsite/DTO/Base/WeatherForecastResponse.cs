namespace WeatherBackend.DTO.Base;

public class WeatherForecastResponse
{
    public string Cod { get; set; }
    public int Message { get; set; }
    public int Cnt { get; set; }
    public List<ForecastHourlyDTO> List { get; set; }
}