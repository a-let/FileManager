using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.DataAccessLayer.Repositories
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly FileManagerContext _context;

        public SeriesRepository(FileManagerContext context)
        {
            _context = context;
        }

        public Series GetSeriesById(int id) => _context.Series.Find(id);

        public IEnumerable<Series> GetSeries() => _context.Series;

        public Series GetSeriesByName(string name) => _context.Series.Single(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public bool SaveSeries(Series series)
        {
            try
            {
                if (series.SeriesId == 0)
                    _context.Series.Add(series);
                else
                {
                    var s = _context.Series.Find(series.SeriesId);
                    _context.Entry(s).CurrentValues.SetValues(series);
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