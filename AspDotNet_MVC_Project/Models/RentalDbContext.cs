using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AspDotNet_MVC_Project.Models
{
    public class RentalDbContext : IdentityDbContext<User>
    {
        public RentalDbContext(DbContextOptions<RentalDbContext> options)
            : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> AspNetUsers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=rentals.db");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });
            builder.Entity<Car>().HasData(
                new Car { Id = 1, Make = "Toyota", Model = "Corolla", MfYear = 2020, Price = 100, Available = true },
                new Car { Id = 2, Make = "Honda", Model = "Civic", MfYear = 2019, Price = 120, Available = true },
                new Car { Id = 3, Make = "Ford", Model = "Focus", MfYear = 2018, Price = 70, Available = true },
                new Car { Id = 4, Make = "BMW", Model = "F30", MfYear = 2013, Price = 90, Available = true },
                new Car { Id = 5, Make = "Opel", Model = "Astra", MfYear = 2020, Price = 80, Available = true },
                new Car { Id = 6, Make = "Kia", Model = "Soul (🤮)", MfYear = 2019, Price = 85, Available = true },
                new Car { Id = 7, Make = "BMW", Model = "G80", MfYear = 2023, Price = 300, Available = true },
                new Car { Id = 8, Make = "Audi", Model = "A4", MfYear = 2022, Price = 200, Available = true },
                new Car { Id = 9, Make = "Mercedes", Model = "W206", MfYear = 2021, Price = 180, Available = true },
                new Car { Id = 10, Make = "Volkswagen", Model = "Polo", MfYear = 2019, Price = 110, Available = true }
            );
        }
    }
}
