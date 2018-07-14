using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.Web.Services
{
    public class ShowControllerService : IShowControllerService
    {
        private readonly IFileManagerObjectRepository<Show> _showRepository;

        public ShowControllerService(IFileManagerObjectRepository<Show> showRepository)
        {
            _showRepository = showRepository;
        }

        public Show GetShowById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ShowId");

            return _showRepository.GetById(id);
        }

        public Show GetShowByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _showRepository.GetByName(name);
        }

        public IEnumerable<Show> GetShows()
        {
            return _showRepository.Get();
        }

        public bool SaveShow(Show show)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            return _showRepository.Save(show);
        }
    }
}