using FileManager.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services.Interfaces
{
    public interface IMovieControllerService : IService<Movie>
    {
        IEnumerable<Movie> GetMoviesBySeriesId(int seriesId);
    }
}