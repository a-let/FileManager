using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetMovies();
        Task<Movie> GetMovieById(int id);
        Task<Movie> GetMovieByName(string name);
        Task<int> SaveMovie(Movie movie);
        Task<IEnumerable<Movie>> GetMoviesBySeriesId(int seriesId);
    }
}