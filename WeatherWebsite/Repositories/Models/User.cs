using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherFrontend.DAL.Models
{
    [Table("users")]
    public class User
    {
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("Login")]
        public string Login { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
    }
}
