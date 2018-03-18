using System.IO;
using LoadUserActivity.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserActivity.Domain.Entities;

namespace UserActivity.Data.Context
{
    public class AppDbContext: DbContext
    {
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("application.json")
                .Build();

            optionsBuilder.UseSqlServer(
                config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActivityMap());
        }

        public DbSet<Activity> Activities { get; set; }
    }
}
