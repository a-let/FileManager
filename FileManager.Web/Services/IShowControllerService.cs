using System.Collections.Generic;
using FileManager.BusinessLayer;

namespace FileManager.Web.Services
{
    public interface IShowControllerService
    {
        bool SaveShow(Show show);
        IEnumerable<Show> GetShows();
    }
}