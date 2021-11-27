using DFQXYU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Logic
{
    public interface IMovieReantalLogic
    {
        void Create(MovieRental rental);
        void Delete(int id);
        MovieRental Read(int id);
        IEnumerable<MovieRental> ReadAll();
        void Update(MovieRental rental);

        IEnumerable<object> RentalsWithBefore2000();
        IEnumerable<object> RentalsWithIsRegularCustomer();
        IEnumerable<object> RentalsWithNotJamesCameronAndCustomerBornDateIs2000();
        IEnumerable<object> RentalsWithJamesCameronMovies();
        IEnumerable<object> RentalsByCustomerNames();
        IEnumerable<object> RentalsByLaci();
        IEnumerable<object> RentalsCustomerBefore2000();
    }
}
