using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockShowControllerService : IShowControllerService
    {
        public async Task<Show> GetShowByIdAsync(int id) => await Task.FromResult(id != 1 ? null : new Show
        {
            ShowId = 1
        });

        public Show GetShowByName(string name) => string.IsNullOrWhiteSpace(name) ? null : new Show
        {
            Name = "Test Show"
        };

        public IEnumerable<Show> GetShows() => new List<Show>();

        public async Task<int> SaveShowAsync(Show show) => await Task.FromResult(show != null ? 1 : 0);
    }
}