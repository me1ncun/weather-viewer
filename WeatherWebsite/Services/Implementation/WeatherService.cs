﻿using Google.Protobuf.WellKnownTypes;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using WeatherBackend.DTO.Base;

namespace WeatherBackend.Database.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        private readonly string Weather_host = "https://api.openweathermap.org/data/2.5/weather";
        private readonly string Geo_host = "https://api.openweathermap.org/geo/1.0";
        private string Api_key;

        public WeatherService(string apiKey)
        {
            _httpClient = new HttpClient();
            Api_key = apiKey;
        }
        
        public async Task<List<LocationDTO>> GetAllCitiesByName(string city)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{Geo_host}/direct?q={city}&limit=5&appid={Api_key}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var allCities = JsonConvert.DeserializeObject<List<LocationDTO>>(data);
                    return allCities;
                }
                else
                {
                    // Handle error response (e.g., log the error, throw a custom exception, etc.)
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw ex;
            }
        }

        public async Task<ForecastDTO> GetWeather(float latitude, float longtitude)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{Weather_host}?lat={latitude}&lon={longtitude}&appid={Api_key}");

                if (response.IsSuccessStatusCode)
                {
                    var weatherData = await response.Content.ReadAsAsync<ForecastDTO>();
                    return weatherData;
                }
                else
                {
                    // Handle error response (e.g., log the error, throw a custom exception, etc.)
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw ex;
            }
        }
    }
}