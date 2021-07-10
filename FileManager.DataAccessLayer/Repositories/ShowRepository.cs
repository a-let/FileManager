using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class ShowRepository : IRepository<Show>
    {
        private readonly FileManagerContext _context;

        public ShowRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<Show> GetByIdAsync(int id) =>
            await _context.Show.FindAsync(id);

        public IEnumerable<Show> Get() => 
            _context.Show;

        public Show GetByName(string name) =>
            _context.Show.FirstOrDefault(s => s.Name == name);

        public async Task SaveAsync(Show show)
        {
            if (show.ShowId == 0)
                await _context.Show.AddAsync(show);
            else
            {
                var s = await _context.Show.FindAsync(show.ShowId);
                _context.Entry(s).CurrentValues.SetValues(show);
            }

            await _context.SaveChangesAsync();
        }
    }
}