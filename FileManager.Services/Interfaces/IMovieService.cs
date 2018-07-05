using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.Models;

namespace FileManager.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task<Movie> GetMovieByNameAsync(string name);
        Task<bool> SaveMovieAsync(Movie movie);
        Task<IEnumerable<Movie>> GetMoviesBySeriesId(int seriesId);
    }
}