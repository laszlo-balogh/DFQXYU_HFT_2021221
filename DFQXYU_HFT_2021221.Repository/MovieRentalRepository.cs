using DFQXYU_HFT_2021221.Data;
using DFQXYU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Repository
{
    public class MovieRentalRepository : IMovieRentalRepository
    {
        MovieDbContext db;

        public MovieRentalRepository(MovieDbContext db)
        {
            this.db = db;
        }
        public void Create(MovieRental rental)
        {
            var customer = from x in db.Customers
                           where x.CustomerID == rental.CustomerID
                           select new Customer()
                           {
                               Name = x.Name,
                               BornDate = x.BornDate,
                               CustomerID = x.CustomerID,
                               Email = x.Email,
                               PhoneNumber = x.PhoneNumber,
                               RegularCustomer = x.RegularCustomer,
                               Rentals = x.Rentals,
                           };

            var movie = from x in db.Movies
                          where x.MovieID == rental.MovieID
                          select new Movie()
                          {
                              MovieID = x.MovieID,
                              Location = x.Location,
                              MovieTitle = x.MovieTitle,
                              Price = x.Price,
                              Producer = x.Producer,
                              Year = x.Year,
                          };

            if (rental.Customer == null)
            {
                rental.Customer = customer as Customer;
            }

            if (rental.Movie == null)
            {
                rental.Movie = movie as Movie;
            }

            db.Add(rental);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public MovieRental Read(int id)
        {
            return db.MovieRentals.FirstOrDefault(t => t.RentalID == id);
        }

        public IQueryable<MovieRental> ReadAll()
        {
            return db.MovieRentals;
        }

        public void Update(MovieRental rental)
        {
            var oldRental = Read(rental.RentalID);
            oldRental.MovieID = rental.MovieID;
            oldRental.Promotions = rental.Promotions;
            oldRental.StartDate = rental.StartDate;
            oldRental.EndDate = rental.EndDate;
            oldRental.CustomerID = rental.CustomerID;
            db.SaveChanges();
        }
    }
}
