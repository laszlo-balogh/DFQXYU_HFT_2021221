using DFQXYU_HFT_2021221.Models;
using DFQXYU_HFT_2021221;
using System;
using System.Linq;
using DFQXYU_HFT_2021221.Data;
using DFQXYU_HFT_2021221.Repository;
using DFQXYU_HFT_2021221.Logic;

namespace DFQXYU_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DFQXYU_HFT_2021221_Database.mdf;Integrated Security=True            

            MovieDbContext db = new MovieDbContext();

            MovieRentalLogic log = new MovieRentalLogic(new MovieRentalRepository(db), new MovieRepository(db), new CustomerRepository(db));

            
            
            //var v2 = log.RentalsByCustomerNames().ToArray();
            //foreach (var item in v2)
            //{
            //    foreach (var sas in item)
            //    {

            //    }
            //}
            //var v4= v2[1].ToString();

            ;

            RestService restService = new RestService("http://localhost:47417/");

            var movies = restService.Get<Movie>("movies");
            var customers = restService.Get<Customer>("customers");
            var rentals = restService.Get<MovieRental>("rentals");

            var v = log.RentalsByCustomerNames();

            ;
            //var movie = restService.Get<>


        }
    }
}
