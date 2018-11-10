using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.Web.Services
{
    public class EpisodeControllerService : IEpisodeControllerService
    {
        private readonly IEpisodeRepository _episodeRepository;

        public EpisodeControllerService(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        public Episode GetEpisodeById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid EpisodeId");

            return _episodeRepository.GetEpisodeById(id);
        }

        public IEnumerable<Episode> GetEpisodes()
        {
            return _episodeRepository.GetEpisodes();
        }

        public Episode GetEpisodeByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            return _episodeRepository.GetEpisodeByName(name);
        }

        public int SaveEpisode(Episode episode)
        {
            if (episode == null)
                throw new ArgumentNullException(nameof(episode));

            return _episodeRepository.SaveEpisode(episode);
        }

        public IQueryable<Episode> GetEpisodesBySeasonId(int seasonId)
        {
            if (seasonId <= 0)
                throw new ArgumentException("Invalid SeasonId");

            return _episodeRepository.GetEpisodesBySeasonId(seasonId);
        }
    }
}