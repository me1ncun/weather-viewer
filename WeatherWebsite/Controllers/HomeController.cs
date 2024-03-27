using Microsoft.AspNetCore.Mvc;

namespace WeatherFrontend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
