using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockSeasonControllerService : ISeasonControllerService
    {
        public async Task<Season> GetAsync(int id) => await Task.FromResult(id != 1 ? null : new Season
        {
            SeasonId = 1
        });

        public async Task<IEnumerable<Season>> GetAsync() => await Task.FromResult(new List<Season>());

        public Task<Season> GetAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Season> GetSeasonsByShowId(int showId) => new List<Season>();

        public async Task<int> SaveAsync(Season season) => await Task.FromResult(season != null ? 1 : 0);
    }
}