using WeatherBackend.DTO.Inserted;
using WeatherFrontend.Models;

namespace WeatherBackend.DTO.Base;

public class CityDTO
{
    public Sys Sys { get; set; }
    public List<LocationDTO> LocationDto { get; set; }
}