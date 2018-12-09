using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Services.Interfaces
{
    public interface ISeriesService
    {
        Task<Series> GetSeriesById(int id);
        Task<Series> GetSeriesByName(string name);
        Task<IEnumerable<Series>> GetSeries();
        Task<int> SaveSeries(Series series);
    }
}