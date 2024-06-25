using WeatherFrontend.DAL.Models;
using WeatherFrontend.Models;

namespace WeatherFrontend.DAL.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<User> FindByLoginAndPass(string login);
        public IEnumerable<User> FindByLoginAndPass(string login, string password);
        public User FindById(int id);
        public void Register(string login, string password);
        public int FindUserId(string login, string password);
        public void AddLocation(CityViewModel request, int userId);
        public IEnumerable<Location> GetLocations(int? userId);
    }
}
