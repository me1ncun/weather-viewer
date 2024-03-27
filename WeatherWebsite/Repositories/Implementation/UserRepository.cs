using Dapper;
using Microsoft.AspNetCore.Identity;
using WeatherBackend.DTO.Base;
using WeatherFrontend.DAL.Interfaces;
using WeatherFrontend.DAL.Models;
using WeatherFrontend.Models;

namespace WeatherFrontend.DAL.Implementation
{
    public class UserRepository: IUserRepository
    {
        public IEnumerable<User> FindByLoginAndPass(string login, string password)
        {
            using (var connection = AppDbContext.CreateConnection())
            {
                return connection.Query<User>("SELECT * FROM [Users] WHERE Login = @l AND Password = @p", new { l = login, p = password });
            }
        }

        public User FindById(int id)
        {
            using (var connection = AppDbContext.CreateConnection())
            {
                return connection.Query<User>("SELECT * FROM [Users] WHERE UserId = @i", new { i = id }).FirstOrDefault();
            }
        }

        public void Register(string login, string password) 
        {
            using (var connection = AppDbContext.CreateConnection())
            {
                connection.Query<User>("INSERT INTO [Users] (Login, Password) VALUES (@l, @p);", new { l = login, p = password });
            }
        }

        public int FindUserId(string login, string password)
        {
            using (var connection = AppDbContext.CreateConnection())
            {
                return connection.Query<int>("SELECT FROM [Users] WHERE Login = @l AND Password = @p", new { l = login, p = password }).FirstOrDefault();
            }
        }

        public void AddLocation(CityViewModel request, int userId)
        {
            using (var connection = AppDbContext.CreateConnection())
            {
                connection.Query("INSERT INTO [Locations] (Name, UserId, Latitude, Longtitude) VALUES (@name, @userid, @lat, @lon);", new { name = request.Name, userid = userId, lat = request.Latitude, lon = request.Longtitude});
            }
        }

        public IEnumerable<Location> GetLocations(int? userId)
        {
            using (var connection = AppDbContext.CreateConnection())
            {
                return connection.Query<Location>("SELECT * FROM [Locations] WHERE UserId = @i", new { i = userId }).AsEnumerable();
            }
        }
    }
}
