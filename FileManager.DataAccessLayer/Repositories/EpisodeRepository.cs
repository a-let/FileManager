using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.DataAccessLayer.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly FileManagerContext _context;

        public EpisodeRepository(FileManagerContext context)
        {
            _context = context;
        }

        public Episode GetEpisodeById(int id) => _context.Episodes.Find(id);

        public Episode GetEpisodeByName(string name) => _context.Episodes.Single(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Episode> GetEpisodes() => _context.Episodes;

        public IQueryable<Episode> GetEpisodesBySeasonId(int seasonId) => _context.Episodes.Where(e => e.SeasonId == seasonId);

        public bool SaveEpisode(Episode episode)
        {
            try
            {
                if (episode.EpisodeId == 0)
                    _context.Episodes.Add(episode);
                else
                {
                    var e = _context.Episodes.Find(episode.EpisodeId);
                    _context.Entry(e).CurrentValues.SetValues(episode);
                }

                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}