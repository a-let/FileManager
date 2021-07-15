using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class SeriesRepository : IRepository<Series>
    {
        private readonly FileManagerContext _context;

        public SeriesRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<Series> GetByIdAsync(int id) =>
            await _context.Series.FindAsync(id);

        public IEnumerable<Series> Get() =>
            _context.Series;

        public Series GetByName(string name) =>
            _context.Series.FirstOrDefault(s => s.Name == name);

        public async Task SaveAsync(Series series)
        {
            if (series.SeriesId == 0)
                await _context.Series.AddAsync(series);
            else
            {
                var s = await _context.Series.FindAsync(series.SeriesId);
                _context.Entry(s).CurrentValues.SetValues(series);
            }

            await _context.SaveChangesAsync();
        }

        public Task<Series> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}