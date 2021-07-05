using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly FileManagerContext _context;

        public MovieRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<Movie> GetMovieByIdAsync(int id) => await _context.Movie.FindAsync(id);

        public Movie GetMovieByName(string name) => _context.Movie.FirstOrDefault(m => m.Name == name);

        public IEnumerable<Movie> GetMovies() => _context.Movie;

        public IEnumerable<Movie> GetMoviesBySeriesId(int seriesId) => _context.Movie.Where(m => m.SeriesId == seriesId);

        public async Task<int> SaveMovieAsync(Movie movie)
        {
            if (movie.MovieId == 0)
                await _context.Movie.AddAsync(movie);
            else
            {
                var m = await _context.Movie.FindAsync(movie.MovieId);
                _context.Entry(m).CurrentValues.SetValues(movie);
            }

            await _context.SaveChangesAsync();

            return movie.MovieId;
        }
    }
}