﻿using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Web.Services.Interfaces
{
    public interface IShowControllerService
    {
        Show GetShowById(int id);
        Show GetShowByName(string name);
        bool SaveShow(Show show);
        IEnumerable<Show> GetShows();
    }
}