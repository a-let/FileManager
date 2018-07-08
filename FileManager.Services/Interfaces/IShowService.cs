using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Services.Interfaces
{
    public interface IShowService
    {
        Show GetShowById(int id);
        Show GetShowByName(string name);
        IEnumerable<Show> GetShows();
        bool SaveShow(Show show);
    }
}