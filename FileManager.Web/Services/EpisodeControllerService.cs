using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.Web.Services
{
    public class EpisodeControllerService : IEpisodeControllerService
    {
        private readonly IFileManagerObjectAdapter<Episode> _episodeAdapter;

        public EpisodeControllerService(IFileManagerObjectAdapter<Episode> episodeAdapter)
        {
            _episodeAdapter = episodeAdapter;
        }

        public Episode GetEpisodeById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid EpisodeId");

            return _episodeAdapter.GetById(id);
        }

        public IEnumerable<Episode> GetEpisodes()
        {
            return _episodeAdapter.Get();
        }

        public Episode GetEpisodeByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            return _episodeAdapter.GetByName(name);
        }

        public bool SaveEpisode(Episode episode)
        {
            if (episode == null)
                throw new ArgumentNullException(nameof(episode));

            return _episodeAdapter.Save(episode);
        }

        public IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId)
        {
            if (seasonId <= 0)
                throw new ArgumentException("Invalid SeasonId");

            return _episodeAdapter.GetByParentId(seasonId);
        }
    }
}