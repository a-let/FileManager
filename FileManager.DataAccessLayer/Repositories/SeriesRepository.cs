using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly FileManagerContext _context;

        public SeriesRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<Series> GetSeriesByIdAsync(int id) => await _context.Series.FindAsync(id);

        public IEnumerable<Series> GetSeries() => _context.Series;

        public Series GetSeriesByName(string name) => _context.Series.FirstOrDefault(s => s.Name == name);

        public async Task<int> SaveSeriesAsync(Series series)
        {
            if (series.SeriesId == 0)
                await _context.Series.AddAsync(series);
            else
            {
                var s = await _context.Series.FindAsync(series.SeriesId);
                _context.Entry(s).CurrentValues.SetValues(series);
            }

            await _context.SaveChangesAsync();

            return series.SeriesId;
        }
    }
}