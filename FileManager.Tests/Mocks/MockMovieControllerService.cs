using FileManager.Models;
using FileManager.Web.Services.Interfaces;
using System.Collections.Generic;

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

        public IEnumerable<Movie> GetMoviesBySeriesId(int seriesId)
        {
            return new List<Movie>();
        }

        public bool SaveMovie(Movie movie)
        {
            return movie != null;
        }
    }
}