using System.Collections.Generic;
using System.Threading.Tasks;
using FileManager.Models;

namespace FileManager.Services.Interfaces
{
    public interface IShowService
    {
        Task<IEnumerable<Show>> GetShowsAsync();
        Task<bool> SaveShowAsync(Show show);
    }
}