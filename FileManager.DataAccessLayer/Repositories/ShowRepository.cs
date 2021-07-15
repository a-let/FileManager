using FileManager.DataAccessLayer.Interfaces;
using FileManager.DataAccessLayer.Queries;
using FileManager.Models;
using FileManager.Models.Dtos;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Repositories
{
    public class ShowRepository : IRepository<Show>
    {
        private readonly FileManagerContext _context;
        private readonly IQueryByIdAsync<QueryById, ShowDto> _queryById;

        public ShowRepository(FileManagerContext context, IQueryByIdAsync<QueryById, ShowDto> queryById)
        {
            _context = context;
            _queryById = queryById;
        }

        public async Task<Show> GetByIdAsync(int id) => null;

        public IEnumerable<Show> Get() => null;

        public Show GetByName(string name) => null;

        public async Task<Show> FindAsync(int id)
        {
            var showDto = await _queryById.QueryAsync(id);

            if (showDto == null)
                return null;

            return new Show(showDto);
        }

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