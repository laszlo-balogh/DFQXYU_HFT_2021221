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
                     //join movie in movieRepo.ReadAll()
                     //on x.MovieID equals movie.MovieID
                     where x.Movie.Year < 2000
                     select new /*RentalsWithBefore2000Class()*/
                     {
                         RentalID = x.RentalID,
                         Name = x.Movie.MovieTitle,
                         Year = x.Movie.Year
                     };
            return v1.ToList();
        }
        public IEnumerable<object> RentalsWithNotJamesCameronAndCustomerBornDateIs2000()
        {
            var v1 = from x in movieRentalRepo.ReadAll()
          //         from movie in movieRepo.ReadAll()  
          //         join rental in movieRentalRepo.ReadAll()
          //         on movie.MovieID equals rental.MovieID
          //         join customer in customerRepo.ReadAll()
          //         on rental.CustomerID equals customer.CustomerID
                     where !x.Movie.Producer.Contains("James Cameron") && x.Customer.BornDate.Year == 2000
                     select new
                     {
                         CustomerName = x.Customer.Name,
                         RentalID = x.RentalID
                     };
            return v1.ToList();
        }
        public IEnumerable<object> RentalsByCustomerNames()
        {
            var v1 = from x in movieRentalRepo.ReadAll()
                         // from customer in customerRepo.ReadAll()
                         //join rental in movieRentalRepo.ReadAll()
                         //on customer.CustomerID equals rental.CustomerID
                         //join movie in movieRepo.ReadAll()
                         //on rental.MovieID equals movie.MovieID
                     select new
                     {
                         //    Name = customer.Name,
                         //    RentalID = rental.RentalID,
                         //    Movie = movie.MovieTitle

                         Name = x.Customer.Name,
                         RentalID = x.RentalID,
                         Movie = x.Movie.MovieTitle
                     };
            var v2 = v1.AsEnumerable().GroupBy(x => x.Name);
            return v2.ToList();
        }
        public IEnumerable<object> RentalsWithIsRegularCustomer()
        {
            var v1 = from x in movieRentalRepo.ReadAll()
                         //join customer in customerRepo.ReadAll()
                         //on x.CustomerID equals customer.CustomerID
                     where x.Customer.RegularCustomer == true
                     select new
                     {
                         RentalID = x.RentalID,
                         CustomerName = x.Customer.Name,
                         RegularCustomer = x.Customer.RegularCustomer,
                         BornDate = x.Customer.BornDate
                     };
            var v2 = v1.AsEnumerable().GroupBy(x => x.CustomerName);
            return v2.ToList();
        }

        public IEnumerable<object> RentalsWithJamesCameronMovies()
        {
            var v1 = from x in movieRentalRepo.ReadAll()
                         //join movie in movieRepo.ReadAll()
                         //on x.MovieID equals movie.MovieID
                     where x.Movie.Producer.Contains("James Cameron")
                     select new
                     {
                         MovieID = x.Movie.MovieID,
                         MovieTitle = x.Movie.MovieTitle,
                         Year = x.Movie.Year,
                         Price = x.Movie.Price,
                         RentalID = x.RentalID
                     };
            return v1.ToList();
        }

        public void Create(MovieRental rental)
        {
            if (rental == null)
            {
                throw new ArgumentNullException("rental");
            }
            else if (rental.Movie == null)
            {
                throw new ArgumentNullException("Rental's movie cannot be null");
            }
            else if (rental.Customer == null)
            {
                throw new ArgumentNullException("Rental's customer cannot be null");
            }
            else if (rental.MovieID != rental.Movie.MovieID || rental.CustomerID != rental.Customer.CustomerID)
            {
                throw new ArgumentException("IDs must match");
            }
            this.movieRentalRepo.Create(rental);
        }

        public void Delete(int id)
        {
            if (id < 1)
            {
                throw new IndexOutOfRangeException("Minimum id value is 1");
            }
            this.movieRentalRepo.Delete(id);
        }

        public MovieRental Read(int id)
        {
            if (id < 1)
            {
                throw new IndexOutOfRangeException("Minimum id value is 1");
            }
            return this.movieRentalRepo.Read(id);
        }

        public IEnumerable<MovieRental> ReadAll()
        {
            return this.movieRentalRepo.ReadAll();
        }

        public void Update(MovieRental rental)
        {
            if (rental == null)
            {
                throw new ArgumentNullException("rental");
            }
            else if (rental.MovieID != rental.Movie.MovieID || rental.CustomerID != rental.Customer.CustomerID)
            {
                throw new ArgumentException("IDs must match");
            }
            this.movieRentalRepo.Update(rental);
        }

        public IEnumerable<object> RentalsByLaci()
        {
            var v1 = from x in movieRentalRepo.ReadAll()
                     where x.Customer.Name.ToUpper().Contains("LACI")
                     select new
                     {
                         RentalID = x.RentalID,
                         Name = x.Customer.Name,
                         Movie = x.Movie.MovieTitle
                     };
            return v1.ToList();
        }

        public IEnumerable<object> RentalsCustomerBefore2000()
        {
            var v1 = from x in movieRentalRepo.ReadAll()
                           where x.Customer.BornDate.Year < 2000
                           select new
                           {
                               RentalID = x.RentalID,
                               Name = x.Customer.Name,
                               BornDate=x.Customer.BornDate,
                               Movie = x.Movie.MovieTitle
                           };
            return v1.ToList();
        }
    }
    
}
