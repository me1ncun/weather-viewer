using Dapper;
using WeatherFrontend.BL.Interfaces;
using WeatherFrontend.DAL.Interfaces;
using WeatherFrontend.DAL.Models;
using WeatherFrontend.Models;

namespace WeatherFrontend.BL.Implementation
{
    public class UserService: IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public int? Authenticate(string login, string password)
        {
            string encpass = Encrypt(password);
            foreach (User user in userRepository.FindByLoginAndPass(login, password))
            {
                if (user.Password == encpass)
                {
                    return user.UserId;
                }
            }
            return null;
        }

        public void Register(string login, string password)
        {
            userRepository.Register(login, password);
        }

        public User GetUserById(int id)
        {
            return userRepository.FindById(id);
        }

        public User GetUser(string login, string password)
        {
            return userRepository.FindByLoginAndPass(login, password).FirstOrDefault();
        }

        public string Encrypt(string password)
        {
            return password;
        }

        public int GetUsersId(string login, string password)
        {
            return userRepository.FindUserId(login, password);
        }

        public void AddLocation(CityViewModel request, int userId)
        {
            userRepository.AddLocation(request, userId);
        }

        public IEnumerable<Location> GetLocations(int userId)
        {
            return userRepository.GetLocations(userId).AsList();
        }
    }
}
