using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class SeasonRepository : IRepository<Season>
    {
        private readonly FileManagerContext _context;

        public SeasonRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<Season> GetByIdAsync(int id) =>
            await _context.Season.FindAsync(id);

        public IEnumerable<Season> Get() =>
            _context.Season;

        public Season GetByName(string name) =>
            throw new NotImplementedException();

        public async Task SaveAsync(Season season)
        {
            if (season.SeasonId == 0)
                await _context.Season.AddAsync(season);
            else
            {
                var s = await _context.Season.FindAsync(season.SeasonId);
                _context.Entry(s).CurrentValues.SetValues(season);
            }

            await _context.SaveChangesAsync();
        }
    }
}