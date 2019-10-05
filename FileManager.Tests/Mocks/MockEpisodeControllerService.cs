using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockEpisodeControllerService : IEpisodeControllerService
    {
        public async Task<Episode> GetAsync(int id) => await Task.FromResult(id != 1 ? null : new Episode
        {
            EpisodeId = 1
        });

        public async Task<Episode> GetAsync(string name) => await Task.FromResult(string.IsNullOrWhiteSpace(name) ? null : new Episode
        {
            Name = "Test Episode"
        });

        public async Task<IEnumerable<Episode>> GetAsync() => await Task.FromResult(new List<Episode>());

        public IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId) => new List<Episode>();

        public async Task<int> SaveAsync(Episode episode) => await Task.FromResult(episode != null ? 1 : 0);
    }
}