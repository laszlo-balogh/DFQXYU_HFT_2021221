using DFQXYU_HFT_2021221.Data;
using DFQXYU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Repository
{
    class MovieRepository : IMovieRepository
    {
        MovieDbContext db;

        public MovieRepository(MovieDbContext db)
        {
            this.db = db;
        }

        public void Create(Movie movie)
        {
            db.Add(movie);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public Movie Read(int id)
        {
            return db.Movies.FirstOrDefault(t => t.MovieID==id);
        }

        public IQueryable<Movie> ReadAll()
        {
            return db.Movies;
        }

        public void Update(Movie movie)
        {
            var oldMovie = Read(movie.MovieID);
            oldMovie.Location = movie.Location;
            oldMovie.MovieTitle = movie.MovieTitle;
            oldMovie.Price = movie.Price;
            oldMovie.Producer = movie.Producer;
            oldMovie.Rentals = movie.Rentals;
            oldMovie.Year = movie.Year;                   
            db.SaveChanges();
        }
    }
}
