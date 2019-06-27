using FileManager.Models;
using FileManager.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockMovieControllerService : IMovieControllerService
    {
        public async Task<Movie> GetMovieByIdAsync(int id) => await Task.FromResult(id != 1 ? null : new Movie
        {
            MovieId = 1
        });

        public Movie GetMovieByName(string name) => string.IsNullOrWhiteSpace(name) ? null : new Movie
        {
            Name = "Test Movie"
        };

        public IEnumerable<Movie> GetMovies() => new List<Movie>();

        public IEnumerable<Movie> GetMoviesBySeriesId(int seriesId) => new List<Movie>();

        public async Task<int> SaveMovieAsync(Movie movie) => await Task.FromResult(movie != null ? 1 : 0);
    }
}