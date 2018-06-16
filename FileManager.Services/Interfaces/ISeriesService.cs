using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.BusinessLayer;

namespace FileManager.Services.Interfaces
{
    public interface ISeriesService
    {
        Task<IEnumerable<Series>> GetSeriesAsync();
        Task<bool> SaveSeriesAsync(Series series);
    }
}