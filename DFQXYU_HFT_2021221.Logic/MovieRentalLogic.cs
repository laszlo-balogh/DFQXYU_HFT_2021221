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
        public IEnumerable<object> Before2000()
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
        public IEnumerable<object> IsRegularCustomer()
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

        public void Create(MovieRental rental)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }



        public MovieRental Read(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovieRental> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(MovieRental rental)
        {
            throw new NotImplementedException();
        }
    }
}
