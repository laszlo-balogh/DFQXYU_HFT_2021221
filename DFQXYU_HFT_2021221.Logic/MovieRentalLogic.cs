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
                     where x.Movie.Year < 2000
                     select new 
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
                     select new
                     {                        
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
            this.movieRentalRepo.Create(rental);
        }

        public void Delete(int id)
        {
            if (id < 1)
            {
                throw new IndexOutOfRangeException("Minimum id value is 1");
            }
            else if (this.movieRentalRepo.Read(id) == null)
            {
                throw new IndexOutOfRangeException("Rental not found");
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
            else if (Read(rental.RentalID) == null)
            {
                throw new ArgumentException("ID not found");
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
