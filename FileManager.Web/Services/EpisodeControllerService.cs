using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class EpisodeControllerService : IEpisodeControllerService
    {
        private readonly IEpisodeRepository _episodeRepository;

        public EpisodeControllerService(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        public async Task<Episode> GetAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid EpisodeId");

            return await _episodeRepository.GetEpisodeByIdAsync(id);
        }

        public async Task<IEnumerable<Episode>> GetAsync() => await Task.Run(() => _episodeRepository.GetEpisodes());

        public async Task<Episode> GetAsync(string name) => await Task.Run(() =>
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            return _episodeRepository.GetEpisodeByName(name);
        });

        public async Task<int> SaveAsync(Episode episode)
        {
            if (episode == null)
                throw new ArgumentNullException(nameof(episode));

            return await _episodeRepository.SaveEpisodeAsync(episode);
        }

        public IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId)
        {
            if (seasonId <= 0)
                throw new ArgumentException("Invalid SeasonId");

            return _episodeRepository.GetEpisodesBySeasonId(seasonId);
        }
    }
}