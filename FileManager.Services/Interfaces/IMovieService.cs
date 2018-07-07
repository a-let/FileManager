using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Services.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetMovies();
        Movie GetMovieById(int id);
        Movie GetMovieByName(string name);
        bool SaveMovie(Movie movie);
        IEnumerable<Movie> GetMoviesBySeriesId(int seriesId);
    }
}