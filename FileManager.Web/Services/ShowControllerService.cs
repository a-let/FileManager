using FileManager.Models;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;

namespace FileManager.Web.Services
{
    public class ShowControllerService : IShowControllerService
    {
        private readonly IShowRepository _showRepository;

        public ShowControllerService(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        public Show GetShowById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ShowId");

            return _showRepository.GetShowById(id);
        }

        public Show GetShowByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _showRepository.GetShowByName(name);
        }

        public IEnumerable<Show> GetShows()
        {
            return _showRepository.GetShows();
        }

        public bool SaveShow(Show show)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            return _showRepository.SaveShow(show);
        }
    }
}