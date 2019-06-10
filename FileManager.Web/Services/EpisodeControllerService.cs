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

        public async Task<Episode> GetEpisodeByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid EpisodeId");

            return await _episodeRepository.GetEpisodeByIdAsync(id);
        }

        public IEnumerable<Episode> GetEpisodes() => _episodeRepository.GetEpisodes();

        public Episode GetEpisodeByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            return _episodeRepository.GetEpisodeByName(name);
        }

        public async Task<int> SaveEpisodeAsync(Episode episode)
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