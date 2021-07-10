using FileManager.Models;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class ShowControllerService : IControllerService<Show>
    {
        private readonly IRepository<Show> _showRepository;

        public ShowControllerService(IRepository<Show> showRepository)
        {
            _showRepository = showRepository;
        }

        public async Task<Show> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ShowId");

            return await _showRepository.GetByIdAsync(id);
        }

        public IEnumerable<Show> Get() =>
            _showRepository.Get();

        public Show GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _showRepository.GetByName(name);
        }

        public async Task SaveAsync(Show show)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            await _showRepository.SaveAsync(show);
        }
    }
}