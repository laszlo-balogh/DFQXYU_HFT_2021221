using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DFQXYU_HFT_2021221.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DFQXYU_HFT_2021221.Data
{
    [Table("Movies")]
    public class MovieDbContext : DbContext
    {
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Costumer> Costumers { get; set; }
        public virtual DbSet<MovieRental> MovieRentals { get; set; }
        public MovieDbContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DFQXYU_HFT_2021221_Database.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieRental>(entity =>
            {
                entity.HasOne(r => r.Movie)
                .WithMany(m => m.Rentals)
                .HasForeignKey(m => m.MovieID)
                .HasForeignKey(m => m.CostumerID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            Movie m1 = new Movie() { MovieID = 1, MovieTitle = "Jurassic Park", Year = 1993, Producer = "Steven Spielberg", Location= "USA", Price = 2000 };
            Movie m2 = new Movie() { MovieID = 2, MovieTitle = "Avangers", Year = 2012, Producer = "Joss Whedon", Location = "USA", Price = 1950 };
            Movie m3 = new Movie() { MovieID = 3, MovieTitle = "Titanic", Year = 1997, Producer = "James Cameron", Location = "USA", Price = 1000 };
            Movie m4 = new Movie() { MovieID = 4, MovieTitle = "Avatar", Year = 2009, Producer = "James Cameron", Location = "USA", Price = 2500 };

            Costumer c1 = new Costumer() { CostumerID=1, Name="Kiss Béla",BornDate = DateTime.Now, Email ="kissb@gmail.com", PhoneNumber =0620333444};
            Costumer c2 = new Costumer() { CostumerID = 2, Name = "Kiss Kati", BornDate = DateTime.Now, Email = "kissk@gmail.com", PhoneNumber = 0620333445 };
            Costumer c3 = new Costumer() { CostumerID = 3, Name = "Kiss Laci", BornDate = DateTime.Now, Email = "kissl@gmail.com", PhoneNumber = 0620333446 };

            MovieRental r1 = new MovieRental() { RentalID = 1, StartDate = DateTime.Now, EndDate = DateTime.Now, Promotions = false, Movie = m1, MovieID = 1, Costumer = c1, CostumerID = 1 };
            MovieRental r2 = new MovieRental() { RentalID = 2, StartDate = DateTime.Now, EndDate = DateTime.Now, Promotions = true, Movie = m2, MovieID = 2, Costumer = c2, CostumerID = 2 };
            MovieRental r3 = new MovieRental() { RentalID = 3, StartDate = DateTime.Now, EndDate = DateTime.Now, Promotions = false, Movie = m3, MovieID = 3, Costumer = c3, CostumerID = 3 };

            modelBuilder.Entity<Movie>().HasData(m1, m2, m3, m4);
            modelBuilder.Entity<Costumer>().HasData(c1, c2, c3);
            modelBuilder.Entity<MovieRental>().HasData(r1, r2, r3);

        }
    }
}
