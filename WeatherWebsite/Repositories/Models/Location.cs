using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherFrontend.DAL.Models;

[Table("locations")]
public class Location
{
    [Column("LocationId")]
    public int LocationId { get; set; }
    [Column("Name")]
    public string Name { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    [Column("Latitude")]
    public float Latitude { get; set; }
    [Column("Longtitude")]
    public float Longtitude { get; set; }
}