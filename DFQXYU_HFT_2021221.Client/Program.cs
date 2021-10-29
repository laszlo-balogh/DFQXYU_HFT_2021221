using DFQXYU_HFT_2021221.Data;
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

            db.Movies.ToList().ForEach(x => Console.WriteLine($"\t{x.MovieTitle}"))
            ;

        }
    }
}
