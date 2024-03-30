using Microsoft.AspNetCore.Mvc;

namespace WeatherFrontend.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
