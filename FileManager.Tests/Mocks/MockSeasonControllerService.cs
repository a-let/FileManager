using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockSeasonControllerService : ISeasonControllerService
    {
        public async Task<Season> GetSeasonByIdAsync(int id) => await Task.FromResult(id != 1 ? null : new Season
        {
            SeasonId = 1
        });

        public IEnumerable<Season> GetSeasons() => new List<Season>();

        public IEnumerable<Season> GetSeasonsByShowId(int showId) => new List<Season>();

        public async Task<int> SaveSeasonAsync(Season season) => await Task.FromResult(season != null ? 1 : 0);
    }
}