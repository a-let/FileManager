using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Services.Interfaces
{
    public interface IShowService
    {
        Task<Show> GetShowById(int id);
        Task<Show> GetShowByName(string name);
        Task<IEnumerable<Show>> GetShows();
        Task<int> SaveShow(Show show);
    }
}