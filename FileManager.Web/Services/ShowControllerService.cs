using FileManager.Models;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class ShowControllerService : IShowControllerService
    {
        private readonly IShowRepository _showRepository;

        public ShowControllerService(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        public async Task<Show> GetAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ShowId");

            return await _showRepository.GetShowByIdAsync(id);
        }

        public async Task<Show> GetAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _showRepository.GetShowByName(name);
        }

        public async Task<IEnumerable<Show>> GetAsync()
        {
            return await Task.Run(() => _showRepository.GetShows());
        }

        public async Task<int> SaveAsync(Show show)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            return await _showRepository.SaveShowAsync(show);
        }
    }
}