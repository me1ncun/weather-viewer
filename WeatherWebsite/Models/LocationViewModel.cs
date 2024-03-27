namespace WeatherFrontend.Models;

public class LocationViewModel
{
    public int LocationId { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public float Latitude { get; set; }
    public float Longtitude { get; set; }
}