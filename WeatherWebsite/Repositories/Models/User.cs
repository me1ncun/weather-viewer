namespace WeatherFrontend.DAL.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
    }
}
