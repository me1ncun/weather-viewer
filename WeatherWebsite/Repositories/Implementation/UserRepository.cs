using Dapper;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using WeatherFrontend.DAL.Interfaces;
using WeatherFrontend.DAL.Models;
using WeatherFrontend.Models;

namespace WeatherFrontend.DAL.Implementation
{
    public class UserRepository: IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string sqlString;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlString = _configuration.GetConnectionString("Database");
        }
        
        public IEnumerable<User> FindByLoginAndPass(string login)
        {
            using (var connection = new NpgsqlConnection(sqlString))
            {
                return connection.Query<User>("SELECT * FROM users WHERE \"Login\" = @l", new { l = login });
            }
        }
        
        public IEnumerable<User> FindByLoginAndPass(string login, string password)
        {
            using (var connection = new NpgsqlConnection(sqlString))
            {
                return connection.Query<User>("SELECT * FROM users WHERE \"Login\" = @l AND \"Password\" = @p;", new { l = login, p = password });
            }
        }

        public User FindById(int id)
        {
            using (var connection = new NpgsqlConnection(sqlString))
            {
                return connection.QueryFirstOrDefault<User>("SELECT * FROM users WHERE \"UserId\"= @i;", new { i = id });
            }
        }

        public void Register(string login, string password) 
        {
            using (var connection = new NpgsqlConnection(sqlString))
            {
                connection.Query<User>("INSERT INTO users (\"Login\", \"Password\") VALUES (@l, @p);", new { l = login, p = password });
            }
        }

        public int FindUserId(string login, string password)
        {
            using (var connection = new NpgsqlConnection(sqlString))
            {
                return connection.QueryFirstOrDefault<int>("SELECT FROM users WHERE \"Login\" = @l AND \"Password\" = @p;", new { l = login, p = password });
            }
        }

        public void AddLocation(CityViewModel request, int userId)
        {
            using (var connection = new NpgsqlConnection(sqlString))
            {
                connection.Query("INSERT INTO locations (\"Name\", \"UserId\", \"Latitude\", \"Longtitude\") VALUES (@name, @userid, @lat, @lon);", new { name = request.Name, userid = userId, lat = request.Latitude, lon = request.Longtitude});
            }
        }

        public IEnumerable<Location> GetLocations(int? userId)
        {
            using (var connection = new NpgsqlConnection(sqlString))
            {
                return connection.Query<Location>("SELECT * FROM locations WHERE \"UserId\" = @i;", new { i = userId }).AsEnumerable();
            }
        }
    }
}
