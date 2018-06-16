using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.BusinessLayer;

namespace FileManager.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetMoviesAsync();
        Task<bool> SaveMovieAsync(Movie movie);
    }
}