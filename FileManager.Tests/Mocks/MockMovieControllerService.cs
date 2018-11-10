using FileManager.Models;
using FileManager.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.Tests.Mocks
{
    public class MockMovieControllerService : IMovieControllerService
    {
        public Movie GetMovieById(int id)
        {
            return id != 1 ? null : new Movie
            {
                MovieId = 1
            };
        }

        public Movie GetMovieByName(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : new Movie
            {
                Name = "Test Movie"
            };
        }

        public IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>();
        }

        public IQueryable<Movie> GetMoviesBySeriesId(int seriesId)
        {
            return new List<Movie>().AsQueryable();
        }

        public int SaveMovie(Movie movie)
        {
            return movie != null ? 1 : 0;
        }
    }
}