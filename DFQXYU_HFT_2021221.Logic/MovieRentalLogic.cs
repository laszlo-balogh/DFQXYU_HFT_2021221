using DFQXYU_HFT_2021221.Models;
using DFQXYU_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Logic
{
    public class MovieRentalLogic : IMovieReantalLogic
    {
        IMovieRentalRepository movieRentalRepo;
        IMovieRepository movieRepo;
        ICustomerRepository customerRepo;
        public MovieRentalLogic(IMovieRentalRepository movieRentalRepo,
            IMovieRepository movieRepo, ICustomerRepository customerRepo)
        {
            this.movieRentalRepo = movieRentalRepo;
            this.movieRepo = movieRepo;
            this.customerRepo = customerRepo;
        }
        public IEnumerable<object> RentalsWithBefore2000()
        {
            var v1 = from x in movieRentalRepo.ReadAll()
                     join movie in movieRepo.ReadAll()
                     on x.MovieID equals movie.MovieID
                     where movie.Year < 2000
                     select new
                     {
                         RentalID = x.RentalID,
                         Name = movie.MovieTitle,
                         Year = movie.Year
                     };
            return v1.ToList();
        }
        public IEnumerable<object> RentalsWithNotJamesCameronAndCustomerBornDateIs2000()
        {
            var v1 = from movie in movieRepo.ReadAll()
                     join rental in movieRentalRepo.ReadAll()
                     on movie.MovieID equals rental.MovieID
                     join customer in customerRepo.ReadAll()
                     on rental.CustomerID equals customer.CustomerID
                     where !movie.Producer.Contains("James Cameron") && customer.BornDate.Year == 2000
                     select new
                     {
                         CustomerNamr = customer.Name,
                         RentalID= rental.RentalID
                     };
            return v1.ToList();
        }
        public IEnumerable<object> RentalsByCustomerNames()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<object> RentalsWithIsRegularCustomer()
        {
            var v1 = from x in movieRentalRepo.ReadAll()
                     join customer in customerRepo.ReadAll()
                     on x.CustomerID equals customer.CustomerID
                     where customer.RegularCustomer == true
                     select new
                     {
                         RentalID = x.RentalID,
                         CustomerName = customer.Name,
                         RegularCustomer = customer.RegularCustomer,
                         BornDate = customer.BornDate
                     };
            return v1.ToList();
        }

        public IEnumerable<object> RentalsWithJamesCameronMovies()
        {
            var v1 = from x in movieRentalRepo.ReadAll()
                     join movie in movieRepo.ReadAll()
                     on x.MovieID equals movie.MovieID
                     where movie.Producer == "James Cameron"
                     select new
                     {
                         MovieID = movie.MovieID,
                         MovieTitle = movie.MovieTitle,
                         Year = movie.Year,
                         Price = movie.Price,
                         RentalID= x.RentalID
                     };
            return v1.ToList();
        }

        public void Create(MovieRental rental)
        {
            this.movieRentalRepo.Create(rental);
        }

        public void Delete(int id)
        {
            this.movieRentalRepo.Delete(id);
        }

        public MovieRental Read(int id)
        {
            return this.movieRentalRepo.Read(id);
        }

        public IEnumerable<MovieRental> ReadAll()
        {
            return this.movieRentalRepo.ReadAll();
        }

        public void Update(MovieRental rental)
        {
            this.movieRentalRepo.Update(rental);
        }        
    }
}
