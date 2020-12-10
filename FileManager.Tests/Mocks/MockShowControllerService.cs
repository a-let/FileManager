using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockShowControllerService : IShowControllerService
    {
        public async Task<Show> GetAsync(int id) => await Task.FromResult(id != 1 ? null : new Show
        {
            ShowId = 1
        });

        public async Task<Show> GetAsync(string name) => await Task.FromResult(string.IsNullOrWhiteSpace(name) ? null : new Show
        {
            Name = "Test Show"
        });

        public async Task<IEnumerable<Show>> GetAsync() => await Task.FromResult(new List<Show>());

        public async Task<int> SaveAsync(Show show) => await Task.FromResult(show != null ? 1 : 0);
    }
}