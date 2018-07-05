using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.Models;

namespace FileManager.Services.Interfaces
{
    public interface ISeriesService
    {
        Task<Series> GetSeriesByIdAsync(int id);
        Task<Series> GetSeriesByName(string name);
        Task<IEnumerable<Series>> GetSeriesAsync();
        Task<bool> SaveSeriesAsync(Series series);
    }
}