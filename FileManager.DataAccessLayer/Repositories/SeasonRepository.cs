using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.DataAccessLayer.Repositories
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly FileManagerContext _context;

        public SeasonRepository(FileManagerContext context)
        {
            _context = context;
        }

        public Season GetSeasonById(int id) => _context.Season.Find(id);

        public IEnumerable<Season> GetSeasons() => _context.Season;

        public IQueryable<Season> GetSeasonsByShowId(int showId) => _context.Season.Where(s => s.ShowId == showId);

        public int SaveSeason(Season season)
        {
            if (season.SeasonId == 0)
                _context.Season.Add(season);
            else
            {
                var s = _context.Season.Find(season.SeasonId);
                _context.Entry(s).CurrentValues.SetValues(season);
            }

            _context.SaveChanges();

            return season.SeasonId;
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