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

        public Season GetSeasonById(int id) => _context.Seasons.Find(id);

        public IEnumerable<Season> GetSeasons() => _context.Seasons;

        public IQueryable<Season> GetSeasonsByShowId(int showId) => _context.Seasons.Where(s => s.ShowId == showId);

        public bool SaveSeason(Season season)
        {
            try
            {
                if (season.SeasonId == 0)
                    _context.Seasons.Add(season);
                else
                {
                    var s = _context.Seasons.Find(season.SeasonId);
                    _context.Entry(s).CurrentValues.SetValues(season);
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