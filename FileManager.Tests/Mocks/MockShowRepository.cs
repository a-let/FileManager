using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockShowRepository : IShowRepository
    {
        public IEnumerable<Show> GetShows() => new List<Show>();

        public async Task<Show> GetShowByIdAsync(int id) => await Task.FromResult(new Show());

        public Show GetShowByName(string name) => new Show();

        public async Task<int> SaveShowAsync(Show show) => await Task.FromResult(show != null ? 1 : 0);
    }
}