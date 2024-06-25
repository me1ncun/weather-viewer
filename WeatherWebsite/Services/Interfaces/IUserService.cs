using WeatherFrontend.DAL.Models;
using WeatherFrontend.Models;

namespace WeatherFrontend.BL.Interfaces
{
    public interface IUserService
    {
        public int? Authenticate(string login, string password);
        public User GetUserById(int id);
        public void Register(string login, string password);
        public User GetUser(string login);
        public int GetUsersId(string login, string password);
        public void AddLocation(CityViewModel request, int userId);
        public IEnumerable<Location> GetLocations(int userId);
    }
}
