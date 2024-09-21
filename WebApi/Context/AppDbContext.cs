using Microsoft.EntityFrameworkCore;
using WebApi.Entity;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subdivision>().HasData(
                    new Subdivision { Id = 1, Name = "Подразделение", IsActive = true },
                    new Subdivision { Id = 2, Name = "Часть подразделения", IsActive = false, MainId = 1 },
                    new Subdivision { Id = 3, Name = "Часть отдела", IsActive = true, MainId = 4 },
                    new Subdivision { Id = 4, Name = "Отдел", IsActive = false }
            );
        }
    }
}
