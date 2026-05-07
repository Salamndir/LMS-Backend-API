using StudentApi.Model;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace StudentApi.Data
{
    // AppDbContext inherits from DbContext (The core of EF Core)
    public class AppDbContext : DbContext
    {
        // Constructor that accepts options (like DB type and connection string)
        // and passes them to the base class
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets represent the tables in the database.
        // We use these properties to query and save instances of the entities.
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }

        // This method runs once when the database is created.
        // We use it here to insert the required assignment test data (Seeding).
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Seed Initial Users (Password is "123" hashed with BCrypt)
            string passwordHash = BCrypt.Net.BCrypt.HashPassword("123");

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "admin@lms.com", PasswordHash = passwordHash, Role = "Admin" },
                new User { Id = 2, Email = "trainee@lms.com", PasswordHash = passwordHash, Role = "Trainee" }
            );

            // 2. Seed Initial Courses
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Title = "Introduction to ASP.NET Core",
                    Description = "Learn the basics of building web APIs",
                    YouTubeVideoUrl = "https://www.youtube.com/watch?v=MFsYaRnrcPQ"
                },
                new Course
                {
                    Id = 2,
                    Title = "Clean Architecture principles",
                    Description = "Master software design patterns",
                    YouTubeVideoUrl = "https://www.youtube.com/watch?v=1OLSE6tX71Y"
                }
            );
        }
    }
}
