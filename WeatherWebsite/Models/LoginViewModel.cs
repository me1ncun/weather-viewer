using System.ComponentModel.DataAnnotations;

namespace WeatherFrontend.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login is required.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
