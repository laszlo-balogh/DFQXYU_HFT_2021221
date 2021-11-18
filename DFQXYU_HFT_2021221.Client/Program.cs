using DFQXYU_HFT_2021221.Data;
using DFQXYU_HFT_2021221.Logic;
using DFQXYU_HFT_2021221.Repository;
using System;
using System.Linq;

namespace DFQXYU_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
             Console.WriteLine("Hello World!");
            //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DFQXYU_HFT_2021221_Database.mdf;Integrated Security=True

            MovieDbContext db = new MovieDbContext();

             db.Movies.ToList().ForEach(x => Console.WriteLine($"\t{x.MovieTitle}"));

            IMovieReantalLogic ml = new MovieRentalLogic(new MovieRentalRepository(db), new MovieRepository(db), new CustomerRepository(db));

            var var1 = ml.RentalsWithBefore2000();
            var var2 = ml.RentalsWithIsRegularCustomer();
            var var3 = ml.RentalsWithNotJamesCameronAndCustomerBornDateIs2000();
            var var4 = ml.RentalsWithJamesCameronMovies();
            ;





        }
    }
}
