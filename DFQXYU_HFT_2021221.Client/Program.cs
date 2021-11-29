using DFQXYU_HFT_2021221.Models;
using DFQXYU_HFT_2021221;
using System;
using System.Linq;
using DFQXYU_HFT_2021221.Data;
using DFQXYU_HFT_2021221.Repository;
using DFQXYU_HFT_2021221.Logic;
using System.Collections.Generic;

namespace DFQXYU_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DFQXYU_HFT_2021221_Database.mdf;Integrated Security=True            

            //MovieDbContext db = new MovieDbContext();

            //MovieRentalLogic log = new MovieRentalLogic(new MovieRentalRepository(db), new MovieRepository(db), new CustomerRepository(db));
            //MovieLogic mlog = new MovieLogic(new MovieRepository(db));

            
            
            ////var v2 = log.RentalsByCustomerNames().ToArray();
            ////foreach (var item in v2)
            ////{
            ////    foreach (var sas in item)
            ////    {

            ////    }
            ////}
            ////var v4= v2[1].ToString();

            //;

            RestService restService = new RestService("http://localhost:47417/");

            //var movies = restService.Get<Movie>("movie");
            //var customers = restService.Get<Customer>("customer");
            //var rentals = restService.Get<MovieRental>("rental");
            //var movie1 = restService.Get<Movie>(1,"movie");

            //var v = log.RentalsByCustomerNames();
            //var v1 =log.RentalsWithIsRegularCustomer();
            //var vvv = mlog.Read(1);
            //;
            //Movie m1 = new Movie() { MovieID = 1, MovieTitle = "Raq Park", Year = 1993, Producer = "Steven dzsonnis", Location = "USA", Price = 2000 };
            Movie m2 = new Movie() { MovieTitle = "Raq Park2", Year = 1992, Producer = "Steven dzsonnis2", Location = "USA2", Price = 2002 };

            //restService.Post<Movie>(m2, "movie");
            ;
            Menu m = new Menu(restService);
            m.Start();
            //foreach (var item in movies)
            //{
            //    Console.WriteLine(item);
            //}


            ;
            //var movie = restService.Get<>


        }       
    }
}
