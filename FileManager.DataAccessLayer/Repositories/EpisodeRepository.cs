using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class EpisodeRepository : IRepository<Episode>
    {
        private readonly FileManagerContext _context;

        public EpisodeRepository(FileManagerContext context)
        {
            _context = context;
        }

        public async Task<Episode> GetByIdAsync(int id) =>
            await _context.Episode.FindAsync(id);

        public IEnumerable<Episode> Get() =>
            _context.Episode;

        public Episode GetByName(string name) =>
            _context.Episode.FirstOrDefault(e => e.Name == name);

        public async Task SaveAsync(Episode target)
        {
            if (target.EpisodeId == 0)
                await _context.Episode.AddAsync(target);
            else
            {
                var e = await _context.Episode.FindAsync(target.EpisodeId);
                _context.Entry(e).CurrentValues.SetValues(target);
            }

            await _context.SaveChangesAsync();
        }
    }
}