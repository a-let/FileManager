using System.Collections.Generic;
using FileManager.Models;
using FileManager.Web.Services.Interfaces;

namespace FileManager.Tests.Mocks
{
    public class MockShowControllerService : IShowControllerService
    {
        public Show GetShowById(int id)
        {
            return id != 1 ? null : new Show
            {
                ShowId = 1
            };
        }

        public Show GetShowByName(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : new Show
            {
                Name = "Test Show"
            };
        }

        public IEnumerable<Show> GetShows()
        {
            return new List<Show>();
        }

        public bool SaveShow(Show show)
        {
            return show != null;
        }
    }
}