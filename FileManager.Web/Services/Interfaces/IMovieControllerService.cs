using System.Collections.Generic;
using System.Linq;
using FileManager.Models;

namespace FileManager.Web.Services.Interfaces
{
    public interface IMovieControllerService
    {
        Movie GetMovieById(int id);
        IEnumerable<Movie> GetMovies();
        Movie GetMovieByName(string name);        
        bool SaveMovie(Movie movie);
        IQueryable<Movie> GetMoviesBySeriesId(int seriesId);
    }
}