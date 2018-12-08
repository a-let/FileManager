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

        public Show GetShowById(int id) => _context.Show.Find(id);

        public Show GetShowByName(string name) => _context.Show.Single(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Show> GetShows() => _context.Show;

        public int SaveShow(Show show)
        {
            if (show.ShowId == 0)
                _context.Show.Add(show);
            else
            {
                var s = _context.Show.Find(show.ShowId);
                _context.Entry(s).CurrentValues.SetValues(show);
            }

            _context.SaveChanges();

            return show.ShowId;
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