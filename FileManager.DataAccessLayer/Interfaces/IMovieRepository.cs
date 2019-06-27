using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie> GetMovieByIdAsync(int id);
        IEnumerable<Movie> GetMovies();
        Movie GetMovieByName(string name);
        Task<int> SaveMovieAsync(Movie movie);
        IEnumerable<Movie> GetMoviesBySeriesId(int seriesId);
    }
}