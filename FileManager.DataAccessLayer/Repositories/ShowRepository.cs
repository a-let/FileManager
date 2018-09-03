using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.DataAccessLayer.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly FileManagerContext _context;

        public ShowRepository(FileManagerContext context)
        {
            _context = context;
        }

        public Show GetShowById(int id) => _context.Shows.Find(id);

        public Show GetShowByName(string name) => _context.Shows.Single(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Show> GetShows() => _context.Shows;

        public bool SaveShow(Show show)
        {
            try
            {
                if (show.ShowId == 0)
                    _context.Shows.Add(show);
                else
                {
                    var s = _context.Shows.Find(show.ShowId);
                    _context.Entry(s).CurrentValues.SetValues(show);
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