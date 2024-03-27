﻿using System.Runtime.InteropServices.JavaScript;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherBackend.Database.Repositories;
using WeatherBackend.Database.Services;
using WeatherBackend.DTO.Base;
using WeatherBackend.DTO.Inserted;
using WeatherFrontend.BL.Interfaces;
using WeatherFrontend.DAL.Implementation;
using WeatherFrontend.DAL.Models;
using WeatherFrontend.Models;

namespace WeatherFrontend.Controllers
{
    public class MainController : Controller
    {
        private readonly WeatherService _weatherService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public MainController(IConfiguration config, IUserService userService)
        {
            _userService = userService;
            _config = config;
            _weatherService = new WeatherService(_config.GetValue<string>("ApiKey"));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
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
            using (var connection = AppDbContext.CreateConnection())
            {
                await connection.OpenAsync();
                var userId = Convert.ToInt32(HttpContext.Session.GetString("LoggedInUserId"));
                if (userId != null)
                {
                    string deleteQuery = "DELETE FROM Locations WHERE UserId = @UserId";
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
        public IActionResult AddLocation([FromForm] CityViewModel request)
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

        [HttpGet]
        public async Task<IActionResult> WeatherForecast(float latitude, float longtitude)
        {
            try
            {
                var weatherData = await _weatherService.GetWeather(latitude, longtitude);
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
                return new JsonResult(ex);
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

        /*[HttpGet]
        public IActionResult RenderWidgets()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("LoggedInUserId"));
            
            var locations = _userService.GetLocations(userId);

            if (locations != null)
            {
                return View(locations);
            }
            else
            {
                return RedirectToAction("Error","User");
            }
        }*/
        

        [HttpGet]
        public async Task<IActionResult> RenderWidgets()
        {
            if (int.TryParse(HttpContext.Session.GetString("LoggedInUserId"), out int userId))
            {
                var locations = _userService.GetLocations(userId);
                /*
                List<ForecastDTO> list = new List<ForecastDTO>();
                foreach (var location in locations)
                {
                    list.Add(_weatherService.GetWeather(location.Latitude, location.Longtitude).Result);
                }
                */
        
                return View(locations);
            }
            else
            {
                return View(null); // Вернуть пустую модель, если userId не удалось получить
            }
        }
    }
}