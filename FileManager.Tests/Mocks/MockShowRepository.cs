using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using System.Collections.Generic;

namespace FileManager.Tests.Mocks
{
    public class MockShowRepository : IShowRepository
    {
        public IEnumerable<Show> GetShows()
        {
            return new List<Show>();
        }

        public Show GetShowById(int id)
        {
            return new Show();
        }

        public Show GetShowByName(string name)
        {
            return new Show();
        }

        public bool SaveShow(Show show)
        {
            return show != null;
        }

        public void Dispose()
        {

        }
    }
}