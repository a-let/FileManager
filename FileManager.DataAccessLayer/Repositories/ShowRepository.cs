using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly FileManagerContext _context;

        public ShowRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<Show> GetShowByIdAsync(int id) => await _context.Show.FindAsync(id);

        public Show GetShowByName(string name) => _context.Show.FirstOrDefault(s => s.Name == name);

        public IEnumerable<Show> GetShows() => _context.Show;

        public async Task<int> SaveShowAsync(Show show)
        {
            if (show.ShowId == 0)
                await _context.Show.AddAsync(show);
            else
            {
                var s = await _context.Show.FindAsync(show.ShowId);
                _context.Entry(s).CurrentValues.SetValues(show);
            }

            await _context.SaveChangesAsync();

            return show.ShowId;
        }
    }
}