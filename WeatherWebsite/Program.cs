using System.Configuration;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using WeatherBackend.Database.Services;
using WeatherFrontend.BL.Interfaces;
using WeatherFrontend.DAL.Implementation;
using WeatherFrontend.DAL.Interfaces;
using WeatherFrontend.DAL.Models;
using UserService = WeatherFrontend.BL.Implementation.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<WeatherService>(provider =>
{
    // Retrieve API key from configuration
    var apiKey = "466761f29c6213b4d7dbad7a7440fffd";
        
    // Create WeatherService instance with the API key
    return new WeatherService(apiKey);
});
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "WeatherApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(1);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/User/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
