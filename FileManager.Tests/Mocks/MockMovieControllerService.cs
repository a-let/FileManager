using FileManager.Models;
using FileManager.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockMovieControllerService : IMovieControllerService
    {
        public async Task<Movie> GetAsync(int id) => await Task.FromResult(id != 1 ? null : new Movie
        {
            MovieId = 1
        });

        public async Task<Movie> GetAsync(string name) => await Task.FromResult(string.IsNullOrWhiteSpace(name) ? null : new Movie
        {
            Name = "Test Movie"
        });

        public async Task<IEnumerable<Movie>> GetAsync() => await Task.FromResult(new List<Movie>());

        public IEnumerable<Movie> GetMoviesBySeriesId(int seriesId) => new List<Movie>();

        public async Task<int> SaveAsync(Movie movie) => await Task.FromResult(movie != null ? 1 : 0);
    }
}