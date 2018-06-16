using System.Collections.Generic;
using FileManager.Models;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.Web.Services
{
    public class ShowControllerService : IShowControllerService
    {
        private readonly IFileManagerObjectAdapter<Show> _showAdapter;

        public ShowControllerService(IFileManagerObjectAdapter<Show> showAdapter)
        {
            _showAdapter = showAdapter;
        }

        public IEnumerable<Show> GetShows()
        {
            return _showAdapter.Get();
        }

        public bool SaveShow(Show show)
        {
            return _showAdapter.Save(show);
        }
    }
}