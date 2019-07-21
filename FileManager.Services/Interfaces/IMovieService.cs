using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Services.Interfaces
{
    public interface IMovieService : IService<Movie>
    {
        Task<IEnumerable<Movie>> GetMoviesBySeriesId(int seriesId);
    }
}