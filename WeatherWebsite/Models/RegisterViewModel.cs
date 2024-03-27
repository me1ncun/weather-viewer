using System.ComponentModel.DataAnnotations;

namespace WeatherFrontend.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Login is required.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string RepeatedPassword { get; set; }
    }
}
