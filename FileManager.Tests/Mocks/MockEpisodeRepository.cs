using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockEpisodeRepository : IEpisodeRepository
    {
        public IEnumerable<Episode> GetEpisodes() => new List<Episode>();

        public async Task<Episode> GetEpisodeByIdAsync(int id) => await Task.FromResult(new Episode());

        public Episode GetEpisodeByName(string name) => new Episode();

        public IEnumerable<Episode> GetEpisodesBySeasonId(int parentId) => new List<Episode>();

        public async Task<int> SaveEpisodeAsync(Episode target) => await Task.FromResult(target != null ? 1 : 0);
    }
}