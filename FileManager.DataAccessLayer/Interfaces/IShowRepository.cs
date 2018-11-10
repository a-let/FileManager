using FileManager.Models;
using System;
using System.Collections.Generic;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IShowRepository : IDisposable
    {
        Show GetShowById(int id);
        IEnumerable<Show> GetShows();
        Show GetShowByName(string name);
        int SaveShow(Show show);
    }
}