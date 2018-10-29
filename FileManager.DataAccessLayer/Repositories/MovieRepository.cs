using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.DataAccessLayer.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly FileManagerContext _context;

        public MovieRepository(FileManagerContext context)
        {
            _context = context;
        }

        public Movie GetMovieById(int id) => _context.Movie.Find(id);

        public Movie GetMovieByName(string name) => _context.Movie.Single(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Movie> GetMovies() => _context.Movie;

        public IQueryable<Movie> GetMoviesBySeriesId(int seriesId) => _context.Movie.Where(m => m.SeriesId == seriesId);

        public bool SaveMovie(Movie movie)
        {
            try
            {
                if (movie.MovieId == 0)
                    _context.Movie.Add(movie);
                else
                {
                    var m = _context.Movie.Find(movie.MovieId);
                    _context.Entry(m).CurrentValues.SetValues(movie);
                }

                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}