using DFQXYU_HFT_2021221.Models;
using System;
using System.Linq;

namespace DFQXYU_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DFQXYU_HFT_2021221_Database.mdf;Integrated Security=True


            RestService restService = new RestService("http://localhost:47417/");

            var movies = restService.Get<Movie>("movies");
            var customers = restService.Get<Customer>("customers");
            var rentals = restService.Get<MovieRental>("rentals");

            //var movie = restService.Get<>


        }
    }
}
