using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services.Interfaces
{
    public interface ISeriesControllerService
    {
        Task<Series> GetSeriesByIdAsync(int id);
        IEnumerable<Series> GetSeries();
        Series GetSeriesByName(string name);
        Task<int> SaveSeriesAsync(Series series);
    }
}