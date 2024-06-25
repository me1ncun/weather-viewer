using System.Data.SqlTypes;
using System.Runtime.InteropServices.JavaScript;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using WeatherFrontend.BL.Interfaces;
using WeatherFrontend.DAL.Implementation;
using WeatherFrontend.DAL.Models;
using WeatherFrontend.Database.Services;
using WeatherFrontend.DTO.Base;
using WeatherFrontend.DTO.Inserted;
using WeatherFrontend.Models;

namespace WeatherFrontend.Controllers
{
    public class MainController : Controller
    {
        private readonly WeatherService _weatherService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly string sqlString;

        public MainController(IConfiguration config, IUserService userService)
        {
            _userService = userService;
            _config = config;
            _weatherService = new WeatherService(_config.GetValue<string>("ApiKey"));
            sqlString = _config.GetConnectionString("Database");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("LoggedInUserId"));
            var locations = _userService.GetLocations(userId);
            List<ForecastDTO> weatherList = new List<ForecastDTO>();
            foreach (var location in locations)
            {
                weatherList.Add(
                    _weatherService.GetWeather(location.Name, location.Latitude, location.Longtitude).Result);
            }

            return View(weatherList);
        }

        [HttpPost("Main/GetLocations")]
        public async Task<IActionResult> GetLocations([FromForm] string LocationName)
        {
            CityDTO cityDto = new CityDTO();
            cityDto.Sys = new Sys();
            cityDto.Sys.Country = LocationName;
            cityDto.LocationDto = await GetAllCities(LocationName);
            return View(cityDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAllLocations()
        {
            using (var connection = new NpgsqlConnection(sqlString))
            {
                await connection.OpenAsync();
                var userId = Convert.ToInt32(HttpContext.Session.GetString("LoggedInUserId"));
                if (userId != null)
                {
                    string deleteQuery = "DELETE FROM Locations WHERE \"UserId\" = @UserId";
                    await connection.ExecuteAsync(deleteQuery, new { UserId = userId });
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    return RedirectToAction("Error", "User");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLocation([FromForm] CityViewModel request)
        {
            using (var connection = new NpgsqlConnection(sqlString))
            {
                await connection.OpenAsync();

                var userId = Convert.ToInt32(HttpContext.Session.GetString("LoggedInUserId"));
                string deleteQuery = "DELETE FROM Locations WHERE \"Name\" = @Name AND \"UserId\" = @Id;";
                await connection.ExecuteAsync(deleteQuery, new { Name = request.Name, Id = userId });
                return RedirectToAction("Index", "Main");
            }
        }

        [HttpPost]
        public IActionResult AddLocation([FromForm] CityViewModel request)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.Session.GetString("LoggedInUserId"));
                if (userId != null)
                {
                    _userService.AddLocation(request, userId);
                }
                else
                {
                    return RedirectToAction("Error", "User");
                }

                return RedirectToAction("Index", "Main");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult WeatherForecast() => View();

        [HttpPost]
        public async Task<IActionResult> WeatherForecast([FromForm] CityViewModel request)
        {
            try
            {
                var weatherData = await _weatherService.GetHourlyForecast(request.Latitude, request.Longtitude);
                if (weatherData != null)
                {
                    return View(weatherData);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<List<LocationDTO>> GetAllCities(string city)
        {
            try
            {
                var getAllCities = await _weatherService.GetAllCitiesByName(city);
                if (getAllCities != null)
                {
                    return getAllCities;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}