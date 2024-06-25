using WeatherFrontend.DTO.Inserted;
using WeatherFrontend.Models;

namespace WeatherFrontend.DTO.Base;

public class CityDTO
{
    public Sys Sys { get; set; }
    public List<LocationDTO> LocationDto { get; set; }
}