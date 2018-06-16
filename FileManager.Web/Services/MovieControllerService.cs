using System.Collections.Generic;
using FileManager.Models;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.Web.Services
{
    public class MovieControllerService : IMovieControllerService
    {
        private readonly IFileManagerObjectAdapter<Movie> _movieAdapter;

        public MovieControllerService(IFileManagerObjectAdapter<Movie> movieAdapter)
        {
            _movieAdapter = movieAdapter;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _movieAdapter.Get();
        }

        public bool SaveMovie(Movie movie)
        {
            return _movieAdapter.Save(movie);
        }
    }
}