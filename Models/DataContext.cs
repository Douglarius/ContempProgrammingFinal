using Microsoft.EntityFrameworkCore;
using System.Data;
using ContempProgrammingFinal;

namespace ContempProgrammingFinal
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<FavoriteFood> FavoriteFoods { get; set; }
        public DbSet<FavoriteMovie> FavoriteMovies { get; set; }
        public DbSet<FavoriteGame> FavoriteGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FullName = "Chandler Mathis",
                    BirthDate = new System.DateTime(1999, 10, 8),
                    CollegeProgram = "Information Technology",
                    YearInProgram = "Senior"
                }
            );
            modelBuilder.Entity<FavoriteFood>().HasData(
                new FavoriteFood
                {
                    Id = 1,
                    Name = "Beef Wellington",
                    Cuisine = "English",
                    Vegetarian = false,
                    EasyToCook = false,
                }
            );
            modelBuilder.Entity<FavoriteMovie>().HasData(
                new FavoriteMovie
                {
                    Id = 1,
                    Name = "Nacho Libre",
                    Genre = "Comedy",
                    LeadActor = "Jack Black",
                    OnNetflix = true
                }
            );
            modelBuilder.Entity<FavoriteGame>().HasData(
                new FavoriteGame
                {
                    Id = 1,
                    Name = "Sekiro",
                    Developer = "FromSoftware",
                    Publisher = "Bandai Namco",
                    Genre = "Action RPG"
                }
            );
        }
    }
}

        