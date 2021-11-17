using DFQXYU_HFT_2021221.Models;
using DFQXYU_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Logic
{
    class MovieLogic : IMovieLogic
    {
        IMovieRepository movieRepo;
        public MovieLogic(IMovieRepository movieRepo)
        {
            this.movieRepo = movieRepo;
        }
        public void Create(Movie movie)
        {
            if (movie.MovieTitle==null)
            {
                throw new ArgumentNullException("MovieTitle cannot be null");
            }
            else if (movie.Year.ToString().Length>4)
            {
                throw new ArgumentNullException("Wrong format");
            }
            else
            {
                this.movieRepo.Create(movie);
            }
        }

        public void Delete(int id)
        {
            this.movieRepo.Delete(id);
        }

        public Movie Read(int id)
        {
            return this.movieRepo.Read(id);
        }

        public IEnumerable<Movie> ReadAll()
        {
            return this.movieRepo.ReadAll();
        }

        public void Update(Movie movie)
        {
            this.movieRepo.Update(movie);
        }
    }
}
