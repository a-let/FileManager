using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IShowRepository
    {
        Task<Show> GetShowByIdAsync(int id);
        IEnumerable<Show> GetShows();
        Show GetShowByName(string name);
        Task<int> SaveShowAsync(Show show);
    }
}