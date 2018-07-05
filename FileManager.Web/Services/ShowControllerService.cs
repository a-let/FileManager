using System;
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

        public Show GetShowById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ShowId");

            return _showAdapter.GetById(id);
        }

        public Show GetShowByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _showAdapter.GetByName(name);
        }

        public IEnumerable<Show> GetShows()
        {
            return _showAdapter.Get();
        }

        public bool SaveShow(Show show)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            return _showAdapter.Save(show);
        }
    }
}