using System.Collections.Generic;
using FileManager.BusinessLayer;

namespace FileManager.Web.Services
{
    public interface IMovieControllerService
    {
        IEnumerable<Movie> GetMovies();
        bool SaveMovie(Movie movie);
    }
}