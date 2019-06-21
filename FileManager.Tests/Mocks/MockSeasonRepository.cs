using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockSeasonRepository : ISeasonRepository
    { 
        public IEnumerable<Season> GetSeasons() => new List<Season>();

        public async Task<Season> GetSeasonByIdAsync(int id) => await Task.FromResult(new Season());

        public IEnumerable<Season> GetSeasonsByShowId(int showId) => new List<Season>();

        public async Task<int> SaveSeasonAsync(Season season) => await Task.FromResult(season != null ? 1 : 0);
    }
}