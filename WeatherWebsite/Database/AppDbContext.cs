using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WeatherFrontend.DAL.Models;

namespace WeatherFrontend.DAL.Implementation
{
    public class AppDbContext: DbContext
    {
        private IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("Database"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
