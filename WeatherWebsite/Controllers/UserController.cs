﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using WeatherFrontend.BL.Interfaces;
using WeatherFrontend.Models;
using WeatherWebsite.Models;

namespace WeatherFrontend.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Login() => View();
        
        [HttpGet]
        public IActionResult Registration() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                int? userId = _userService.Authenticate(model.Login, model.Password);
                if (userId != null)
                {
                    HttpContext.Session.SetString("LoggedInUser", model.Login);
                    HttpContext.Session.SetString("LoggedInUserId", Convert.ToString(userId));
                    
                    return View(_userService.GetUserById(userId.Value));
                }
                else
                {
                    ModelState.AddModelError("notCorrect", "Неправильный логин или пароль. Попробуйте еще раз.");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegisterViewModel model)
        {
            var userExists = _userService.GetUser(model.Login);

            if (userExists != null)
            {
                return RedirectToAction("Error");
            }

            _userService.Register(model.Login, model.Password);
            var user = _userService.GetUser(model.Login);
    
            if (user != null)
            {
                return View(user);
            }

            return View();
        }
        
        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                HttpContext.Response.Clear();
                
                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
