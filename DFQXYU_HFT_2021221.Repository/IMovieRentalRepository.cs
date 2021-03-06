using DFQXYU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Repository
{
    public interface IMovieRentalRepository
    {
        void Create(MovieRental rental);
        void Delete(int id);
        MovieRental Read(int id);
        IQueryable<MovieRental> ReadAll();
        void Update(MovieRental rental);



    }
}
