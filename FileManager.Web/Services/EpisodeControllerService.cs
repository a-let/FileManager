using FileManager.DataAccessLayer;
using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.Web.Services
{
    public class EpisodeControllerService : IEpisodeControllerService
    {
        private readonly FileManagerContext _fileManagerContext;

        public EpisodeControllerService(FileManagerContext fileManagerContext)
        {
            _fileManagerContext = fileManagerContext;
        }

        public Episode GetEpisodeById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid EpisodeId");

            return _fileManagerContext.Episodes.Find(id);
        }

        public IEnumerable<Episode> GetEpisodes()
        {
            return _fileManagerContext.Episodes;
        }

        public Episode GetEpisodeByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            return _fileManagerContext.Episodes.Single(e => e.Name.Equals(name));
        }

        public void SaveEpisode(Episode episode)
        {
            if (episode == null)
                throw new ArgumentNullException(nameof(episode));

            if (episode.EpisodeId == 0)
                _fileManagerContext.Episodes.Add(episode);

            _fileManagerContext.SaveChanges();
        }

        public IQueryable<Episode> GetEpisodesBySeasonId(int seasonId)
        {
            if (seasonId <= 0)
                throw new ArgumentException("Invalid SeasonId");

            return _fileManagerContext.Episodes.Where(e => e.SeasonId == seasonId);
        }
    }
}