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
    public class MovieDbContext : DbContext
    {
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<MovieRental> MovieRentals { get; set; }
        public MovieDbContext()
        {
            this.Database.EnsureCreated();
        }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

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
            modelBuilder.Entity<Movie>(entity =>
            {
                entity
                .HasMany(m => m.Rentals)
                .WithOne(r => r.Movie)
                .HasForeignKey(m => m.MovieID)
                .OnDelete(DeleteBehavior.Cascade);

            });


            modelBuilder.Entity<Customer>(entity =>
            {
                entity
                .HasMany/*<MovieRental>*/(c => c.Rentals)
                .WithOne(r => r.Customer)
                .HasForeignKey(c => c.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<MovieRental>(entity =>
            {
                entity
                .HasOne(r => r.Movie)
                .WithMany(m => m.Rentals)
                .HasForeignKey(r => r.MovieID)
                .OnDelete(DeleteBehavior.Cascade);

               

                entity.HasOne(r => r.Customer)
               .WithMany(c => c.Rentals)
               .HasForeignKey(r => r.CustomerID)
               .OnDelete(DeleteBehavior.Cascade);

                
            });

            Movie m1 = new Movie() { MovieID = 1, MovieTitle = "Jurassic Park", Year = 1993, Producer = "Steven Spielberg", Location = "USA", Price = 2000 };
            Movie m2 = new Movie() { MovieID = 2, MovieTitle = "Avangers", Year = 2012, Producer = "Joss Whedon", Location = "USA", Price = 1950 };
            Movie m3 = new Movie() { MovieID = 3, MovieTitle = "Titanic", Year = 1997, Producer = "James Cameron", Location = "USA", Price = 1000 };
            Movie m4 = new Movie() { MovieID = 4, MovieTitle = "Avatar", Year = 2009, Producer = "James Cameron", Location = "USA", Price = 2500 };
            Movie m5 = new Movie() { MovieID = 5, MovieTitle = "Alita: Battle Angel", Year = 2019, Producer = "James Cameron", Location = "USA", Price = 3000 };
            Movie m6 = new Movie() { MovieID = 6, MovieTitle = "Ready Player One", Year = 2018, Producer = "Steven Spielberg", Location = "USA", Price = 3000 };
            Movie m7 = new Movie() { MovieID = 7, MovieTitle = "Jaws", Year = 1975, Producer = "Steven Spielberg", Location = "USA", Price = 1000 };
            Movie m8 = new Movie() { MovieID = 8, MovieTitle = "Back to the Future", Year = 1985, Producer = "Robert Zemeckis", Location = "USA", Price = 2000 };
            Movie m9 = new Movie() { MovieID = 9, MovieTitle = "Forrest Gump", Year = 1994, Producer = "Robert Zemeckis", Location = "USA", Price = 2000 };

            Customer c1 = new Customer() { CustomerID = 1, Name = "Kiss Béla", BornDate = new DateTime(2000, 11, 02), Email = "kissb@gmail.com", PhoneNumber = 0620333444 };
            Customer c2 = new Customer() { CustomerID = 2, Name = "Kiss Kati", BornDate = new DateTime(2002, 01, 22), Email = "kissk@gmail.com", PhoneNumber = 0620333445 };
            Customer c3 = new Customer() { CustomerID = 3, Name = "Kiss Laci", BornDate = new DateTime(1999, 03, 14), Email = "kissl@gmail.com", PhoneNumber = 0620333446, RegularCustomer = true };
            Customer c4 = new Customer() { CustomerID = 4, Name = "Nagy Laci", BornDate = new DateTime(2000, 04, 25), Email = "nagyl@gmail.com", PhoneNumber = 0670323567, RegularCustomer = true };

            MovieRental r1 = new MovieRental() { RentalID = 1, Promotions = false, CustomerID = c1.CustomerID,MovieID=m1.MovieID/*, Movie = m1*/ };
            MovieRental r2 = new MovieRental() { RentalID = 2, Promotions = false, CustomerID = c2.CustomerID, MovieID = m2.MovieID/*, Movie = m2*/ };
            MovieRental r3 = new MovieRental() { RentalID = 3, Promotions = false, CustomerID = c3.CustomerID, MovieID = m3.MovieID/*, Movie = m3*/ };
            MovieRental r4 = new MovieRental() { RentalID = 4, Promotions = true, CustomerID = c4.CustomerID, MovieID = m4.MovieID/*, Movie = m4*/ };
            MovieRental r5 = new MovieRental() { RentalID = 5, Promotions = true, CustomerID = c4.CustomerID, MovieID = m5.MovieID/*, Movie = m5*/ };
            MovieRental r6 = new MovieRental() { RentalID = 6, Promotions = false, CustomerID = c1.CustomerID, MovieID = m6.MovieID/*, Movie = m6*/ };
            MovieRental r7 = new MovieRental() { RentalID = 7, Promotions = true, CustomerID = c3.CustomerID, MovieID = m7.MovieID/*, Movie = m7*/ };
            MovieRental r8 = new MovieRental() { RentalID = 8, Promotions = false, CustomerID = c2.CustomerID,MovieID = m8.MovieID/*, Movie = m8*/ };
            MovieRental r9 = new MovieRental() { RentalID = 9, Promotions = true, CustomerID = c3.CustomerID, MovieID = m9.MovieID/* Movie = m9*/ };


            modelBuilder.Entity<Movie>().HasData(m1, m2, m3, m4, m5, m6, m7, m8, m9);
            modelBuilder.Entity<Customer>().HasData(c1, c2, c3, c4);
            modelBuilder.Entity<MovieRental>().HasData(r1, r2, r3, r4, r5, r6, r7, r8, r9);            
        }
    }
}
