using DFQXYU_HFT_2021221.Data;
using DFQXYU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Repository
{
    class MovieRentalRepository : IMovieRentalRepository
    {
        MovieDbContext db;

        public MovieRentalRepository(MovieDbContext db)
        {
            this.db = db;
        }
        public void Create(MovieRental rental)
        {
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
            oldRental.Movie = rental.Movie;
            oldRental.MovieID = rental.MovieID;
            oldRental.Promotions = rental.Promotions;
            oldRental.StartDate = rental.StartDate;
            oldRental.EndDate = rental.EndDate;
            oldRental.CustomerID = rental.CustomerID;
            oldRental.Customer = rental.Customer;
            db.SaveChanges();
        }
    }
}
