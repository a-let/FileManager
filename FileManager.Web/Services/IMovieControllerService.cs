using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Web.Services
{
    public interface IMovieControllerService
    {
        IEnumerable<Movie> GetMovies();
        bool SaveMovie(Movie movie);
    }
}