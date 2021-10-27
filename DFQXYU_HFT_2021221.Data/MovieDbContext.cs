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
        public virtual DbSet<Customer> Costumers { get; set; }
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

            Movie m1 = new Movie() { MovieID = 1, MovieTitle = "Jurassic Park", Year = 1993, Producer = "Steven Spielberg", Location = "USA", Price = 2000 };
            Movie m2 = new Movie() { MovieID = 2, MovieTitle = "Avangers", Year = 2012, Producer = "Joss Whedon", Location = "USA", Price = 1950 };
            Movie m3 = new Movie() { MovieID = 3, MovieTitle = "Titanic", Year = 1997, Producer = "James Cameron", Location = "USA", Price = 1000 };
            Movie m4 = new Movie() { MovieID = 4, MovieTitle = "Avatar", Year = 2009, Producer = "James Cameron", Location = "USA", Price = 2500 };

            Customer c1 = new Customer() { CustomerID = 1, Name = "Kiss Béla", BornDate = new DateTime(2000, 11, 02), Email = "kissb@gmail.com", PhoneNumber = 0620333444 };
            Customer c2 = new Customer() { CustomerID = 2, Name = "Kiss Kati", BornDate = new DateTime(2002, 01, 22), Email = "kissk@gmail.com", PhoneNumber = 0620333445 };
            Customer c3 = new Customer() { CustomerID = 3, Name = "Kiss Laci", BornDate = new DateTime(1999, 03, 14), Email = "kissl@gmail.com", PhoneNumber = 0620333446 };

            MovieRental r1 = new MovieRental() { RentalID = 1, Promotions = false, Movie = m1, /*MovieID = m1.MovieID,*/ Costumer = c1/*, CostumerID = c1.CostumerID*/ };
            MovieRental r2 = new MovieRental() { RentalID = 2, Promotions = false, Movie = m2,/* MovieID = m2.MovieID,*/ Costumer = c2/* CostumerID = c2.CostumerID*/ };
            MovieRental r3 = new MovieRental() { RentalID = 3, Promotions = false, Movie = m3, /*MovieID = m3.MovieID,*/ Costumer = c3/*, CostumerID = c3.CostumerID*/ };

            m1.Rentals.Add(r1);
            m2.Rentals.Add(r2);
            m3.Rentals.Add(r3);

            c1.Rentals.Add(r1);
            c2.Rentals.Add(r2);
            c3.Rentals.Add(r3);

            r1.CostumerID = c1.CustomerID;
            r2.CostumerID = c2.CustomerID;
            r3.CostumerID = c3.CustomerID;

            r1.MovieID = m1.MovieID;
            r2.MovieID = m2.MovieID;
            r3.MovieID = m3.MovieID;


            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasMany(m => m.Rentals)
                .WithOne(r => r.Movie)
                .HasForeignKey(m => m.MovieID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasMany(c => c.Rentals)
                .WithOne(r => r.Costumer)
                .HasForeignKey(c => c.CostumerID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MovieRental>(entity =>
            {
                entity.HasOne(r => r.Movie)
                .WithMany(m => m.Rentals)
                .HasForeignKey(r => r.MovieID)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(r => r.Costumer)
               .WithMany(m => m.Rentals)
               .HasForeignKey(r => r.CostumerID)
               .OnDelete(DeleteBehavior.ClientSetNull);
            });



            modelBuilder.Entity<Movie>().HasData(m1, m2, m3, m4);
            modelBuilder.Entity<Customer>().HasData(c1, c2, c3);
            modelBuilder.Entity<MovieRental>().HasData(r1, r2, r3);

            
        }
    }
}
