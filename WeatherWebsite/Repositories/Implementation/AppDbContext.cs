using System.Data.SqlClient;

namespace WeatherFrontend.DAL.Implementation
{
    public class AppDbContext
    {
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection("Data Source=.;Initial Catalog=Weather;Integrated Security=true;");
        }
    }
}
