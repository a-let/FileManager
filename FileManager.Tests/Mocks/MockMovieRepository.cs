using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.Tests.Mocks
{
    public class MockMovieRepository : IMovieRepository
    {
        public Movie GetMovieById(int id)
        {
            return new Movie();
        }

        public Movie GetMovieByName(string name)
        {
            return new Movie();
        }

        public IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>();
        }

        public IQueryable<Movie> GetMoviesBySeriesId(int seriesId)
        {
            return new List<Movie>().AsQueryable();
        }

        public bool SaveMovie(Movie movie)
        {
            return movie != null;
        }

        public void Dispose()
        {
            
        }
    }
}