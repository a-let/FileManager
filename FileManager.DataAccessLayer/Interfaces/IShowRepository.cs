using FileManager.Models;
using System.Collections.Generic;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IShowRepository
    {
        Show GetShowById(int id);
        IEnumerable<Show> GetShows();
        Show GetShowByName(int id);
        bool SaveShow(Show show);
    }
}