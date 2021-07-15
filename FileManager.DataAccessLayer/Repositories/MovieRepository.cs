using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly FileManagerContext _context;

        public MovieRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<Movie> GetByIdAsync(int id) =>
            await _context.Movie.FindAsync(id);

        public IEnumerable<Movie> Get() =>
            _context.Movie;

        public Movie GetByName(string name) =>
            _context.Movie.FirstOrDefault(m => m.Name == name);

        public async Task SaveAsync(Movie movie)
        {
            if (movie.MovieId == 0)
                await _context.Movie.AddAsync(movie);
            else
            {
                var m = await _context.Movie.FindAsync(movie.MovieId);
                _context.Entry(m).CurrentValues.SetValues(movie);
            }

            await _context.SaveChangesAsync();
        }

        public Task<Movie> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}