using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.Models;

namespace FileManager.Services.Interfaces
{
    public interface ISeriesService
    {
        Task<IEnumerable<Series>> GetSeriesAsync();
        Task<bool> SaveSeriesAsync(Series series);
    }
}