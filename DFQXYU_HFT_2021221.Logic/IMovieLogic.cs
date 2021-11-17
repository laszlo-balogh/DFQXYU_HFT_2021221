using DFQXYU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Logic
{
    public interface IMovieLogic
    {
        void Create(Movie movie);
        void Delete(int id);
        Movie Read(int id);
        IEnumerable<Movie> ReadAll();
        void Update(Movie novie);
    }
}
