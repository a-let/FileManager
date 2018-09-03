using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Web.Services.Interfaces
{
    public interface IMovieControllerService
    {
        Movie GetMovieById(int id);
        IEnumerable<Movie> GetMovies();
        Movie GetMovieByName(string name);        
        bool SaveMovie(Movie movie);
        IEnumerable<Movie> GetMoviesBySeriesId(int seriesId);
    }
}