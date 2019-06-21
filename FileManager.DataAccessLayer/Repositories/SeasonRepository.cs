using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly FileManagerContext _context;

        public SeasonRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<Season> GetSeasonByIdAsync(int id) => await _context.Season.FindAsync(id);

        public IEnumerable<Season> GetSeasons() => _context.Season;

        public IEnumerable<Season> GetSeasonsByShowId(int showId) => _context.Season.Where(s => s.ShowId == showId);

        public async Task<int> SaveSeasonAsync(Season season)
        {
            if (season.SeasonId == 0)
                await _context.Season.AddAsync(season);
            else
            {
                var s = await _context.Season.FindAsync(season.SeasonId);
                _context.Entry(s).CurrentValues.SetValues(season);
            }

            await _context.SaveChangesAsync();

            return season.SeasonId;
        }
    }
}