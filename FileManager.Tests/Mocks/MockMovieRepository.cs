using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockMovieRepository : IMovieRepository
    {
        public async Task<Movie> GetMovieByIdAsync(int id) => await Task.FromResult(new Movie());

        public Movie GetMovieByName(string name) => new Movie();

        public IEnumerable<Movie> GetMovies() => new List<Movie>();

        public IEnumerable<Movie> GetMoviesBySeriesId(int seriesId) => new List<Movie>().AsQueryable();

        public async Task<int> SaveMovieAsync(Movie movie) => await Task.FromResult(movie != null ? 1 : 0);
    }
}