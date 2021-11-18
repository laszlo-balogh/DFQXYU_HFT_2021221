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
        public virtual DbSet<Customer> Customers { get; set; }
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
            Movie m5 = new Movie() { MovieID = 5, MovieTitle = "Alita: Battle Angel", Year = 2019, Producer = "James Cameron", Location = "USA", Price = 3000 };
            Movie m6 = new Movie() { MovieID = 6, MovieTitle = "Ready Player One", Year = 2018, Producer = "Steven Spielberg", Location = "USA", Price = 3000 };
            Movie m7 = new Movie() { MovieID = 7, MovieTitle = "Jaws", Year = 1975, Producer = "Steven Spielberg", Location = "USA", Price = 1000 };
            Movie m8 = new Movie() { MovieID = 8, MovieTitle = "Back to the Future", Year = 1985, Producer = "Robert Zemeckis", Location = "USA", Price = 2000 };
            Movie m9 = new Movie() { MovieID = 9, MovieTitle = "Forrest Gump", Year = 1994, Producer = "Robert Zemeckis", Location = "USA", Price = 2000 };

            Customer c1 = new Customer() { CustomerID = 1, Name = "Kiss Béla", BornDate = new DateTime(2000, 11, 02), Email = "kissb@gmail.com", PhoneNumber = 0620333444 };
            Customer c2 = new Customer() { CustomerID = 2, Name = "Kiss Kati", BornDate = new DateTime(2002, 01, 22), Email = "kissk@gmail.com", PhoneNumber = 0620333445 };
            Customer c3 = new Customer() { CustomerID = 3, Name = "Kiss Laci", BornDate = new DateTime(1999, 03, 14), Email = "kissl@gmail.com", PhoneNumber = 0620333446, RegularCustomer = true };
            Customer c4 = new Customer() { CustomerID = 4, Name = "Nagy Laci", BornDate = new DateTime(2000, 04, 25), Email = "nagyl@gmail.com", PhoneNumber = 0670323567, RegularCustomer = true };

            MovieRental r1 = new MovieRental() { RentalID = 1, Promotions = false, Movie = m1};
            MovieRental r2 = new MovieRental() { RentalID = 2, Promotions = false, Movie = m2};
            MovieRental r3 = new MovieRental() { RentalID = 3, Promotions = false, Movie = m3};
            MovieRental r4 = new MovieRental() { RentalID = 4, Promotions = true, Movie = m4};
            MovieRental r5 = new MovieRental() { RentalID = 5, Promotions = true, Movie = m5};
            MovieRental r6 = new MovieRental() { RentalID = 6, Promotions = false, Movie = m6};
            MovieRental r7 = new MovieRental() { RentalID = 7, Promotions = true, Movie = m7};
            MovieRental r8 = new MovieRental() { RentalID = 8, Promotions = false, Movie = m8};
            MovieRental r9 = new MovieRental() { RentalID = 9, Promotions = true, Movie = m9};

            m1.Rentals.Add(r1);
            m2.Rentals.Add(r2);
            m3.Rentals.Add(r3);
            m4.Rentals.Add(r4);
            m5.Rentals.Add(r5);
            m6.Rentals.Add(r6);
            m7.Rentals.Add(r7);
            m8.Rentals.Add(r8);
            m9.Rentals.Add(r9);

            c1.Rentals.Add(r1);
            c2.Rentals.Add(r2);
            c3.Rentals.Add(r3);
            c4.Rentals.Add(r4);
            c4.Rentals.Add(r5);
            c1.Rentals.Add(r6);
            c3.Rentals.Add(r7);
            c2.Rentals.Add(r8);
            c3.Rentals.Add(r9);

            r1.CustomerID = c1.CustomerID;
            r2.CustomerID = c2.CustomerID;
            r3.CustomerID = c3.CustomerID;
            r4.CustomerID = c4.CustomerID;
            r5.CustomerID = c4.CustomerID;
            r6.CustomerID = c1.CustomerID;
            r7.CustomerID = c3.CustomerID;
            r8.CustomerID = c2.CustomerID;
            r9.CustomerID = c3.CustomerID;                       

            r1.MovieID = m1.MovieID;
            r2.MovieID = m2.MovieID;
            r3.MovieID = m3.MovieID;
            r4.MovieID = m4.MovieID;
            r5.MovieID = m5.MovieID;
            r6.MovieID = m6.MovieID;
            r7.MovieID = m7.MovieID;
            r8.MovieID = m8.MovieID;
            r9.MovieID = m9.MovieID;


            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasMany<MovieRental>(/*m => m.Rentals*/)
                .WithOne(/*r => r.Movie*/)
                .HasForeignKey(m => m.MovieID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasMany<MovieRental>(/*c => c.Rentals*/)
                .WithOne(/*r => r.Customer*/)
                .HasForeignKey(c => c.CustomerID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MovieRental>(entity =>
            {
                entity.HasOne<Movie>(/*r => r.Movie*/)
                .WithMany(/*m => m.Rentals*/)
                .HasForeignKey(r => r.MovieID)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne<Customer>(/*r => r.Customer*/)
               .WithMany(/*c => c.Rentals*/)
               .HasForeignKey(r => r.CustomerID)
               .OnDelete(DeleteBehavior.ClientSetNull);
            });



            modelBuilder.Entity<Movie>().HasData(m1, m2, m3, m4,m5,m6,m7,m8,m9);
            modelBuilder.Entity<Customer>().HasData(c1, c2, c3, c4);
            modelBuilder.Entity<MovieRental>().HasData(r1, r2, r3, r4,r5,r6,r7,r8,r9);


        }
    }
}
