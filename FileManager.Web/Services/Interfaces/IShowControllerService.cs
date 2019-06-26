using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services.Interfaces
{
    public interface IShowControllerService
    {
        Task<Show> GetShowByIdAsync(int id);
        Show GetShowByName(string name);
        Task<int> SaveShowAsync(Show show);
        IEnumerable<Show> GetShows();
    }
}