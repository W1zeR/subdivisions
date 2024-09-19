using Microsoft.EntityFrameworkCore;
using WebApi.Entity;
using Microsoft.Extensions.Configuration;

namespace WebApi.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Subdivision> Subdivisions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
