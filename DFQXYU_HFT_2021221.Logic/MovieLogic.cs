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
            if (movie == null)
            {
                throw new ArgumentNullException("movie");
            }
            else if (movie.MovieTitle == null)
            {
                throw new ArgumentNullException("MovieTitle cannot be null");
            }
            else if (movie.Year.ToString().Length > 4 || movie.Year < 0)
            {
                throw new ArgumentNullException("Wrong format");
            }
            else if (movie.Year < 0)
            {
                throw new ArgumentException("Year cannot be negative");
            }
            else if (movie.Price < 0)
            {
                throw new ArgumentException("Price cannot be negative");
            }
            else
            {
                this.movieRepo.Create(movie);
            }
        }

        public void Delete(int id)
        {
            if (id < 1)
            {
                throw new IndexOutOfRangeException("Minimum id value is 1");
            }
            this.movieRepo.Delete(id);
        }

        public Movie Read(int id)
        {
            if (id < 1)
            {
                throw new IndexOutOfRangeException("Minimum id value is 1");
            }
            return this.movieRepo.Read(id);
        }

        public IEnumerable<Movie> ReadAll()
        {
            return this.movieRepo.ReadAll();
        }

        public void Update(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("movie");
            }
            else if (movie.MovieTitle == null)
            {
                throw new ArgumentNullException("MovieTitle cannot be null");
            }
            else if (movie.Year.ToString().Length > 4 || movie.Year < 0)
            {
                throw new ArgumentNullException("Wrong format");
            }
            else if (movie.Price < 0)
            {
                throw new ArgumentException("Price cannot be negative");
            }
            this.movieRepo.Update(movie);
        }
    }
}
