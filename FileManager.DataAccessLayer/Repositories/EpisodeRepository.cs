using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly FileManagerContext _context;

        public EpisodeRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<Episode> GetEpisodeByIdAsync(int id) => await _context.Episode.FindAsync(id);

        public Episode GetEpisodeByName(string name) => _context.Episode.Single(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Episode> GetEpisodes() => _context.Episode;

        public IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId) => _context.Episode.Where(e => e.SeasonId == seasonId);

        public async Task<int> SaveEpisodeAsync(Episode episode)
        {
            if (episode.EpisodeId == 0)
                await _context.Episode.AddAsync(episode);
            else
            {
                var e = await _context.Episode.FindAsync(episode.EpisodeId);
                _context.Entry(e).CurrentValues.SetValues(episode);
            }

            await _context.SaveChangesAsync();

            return episode.EpisodeId;
        }
    }
}