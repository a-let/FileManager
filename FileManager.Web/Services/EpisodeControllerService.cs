using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class EpisodeControllerService : IControllerService<Episode>
    {
        private readonly IRepository<Episode> _episodeRepository;

        public EpisodeControllerService(IRepository<Episode> episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        public async Task<Episode> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid EpisodeId");

            return await _episodeRepository.GetByIdAsync(id);
        }

        public IEnumerable<Episode> Get() =>
            _episodeRepository.Get();

        public Episode GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            return _episodeRepository.GetByName(name);
        }

        public async Task SaveAsync(Episode episode)
        {
            if (episode == null)
                throw new ArgumentNullException(nameof(episode));

            await _episodeRepository.SaveAsync(episode);
        }
    }
}