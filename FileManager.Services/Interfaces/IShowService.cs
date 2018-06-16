using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.BusinessLayer;

namespace FileManager.Services.Interfaces
{
    public interface IShowService
    {
        Task<IEnumerable<Show>> GetShowsAsync();
        Task<bool> SaveShowAsync(Show show);
    }
}