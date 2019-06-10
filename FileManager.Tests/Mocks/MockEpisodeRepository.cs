using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockEpisodeRepository : IEpisodeRepository
    {
        public IEnumerable<Episode> GetEpisodes()
        {
            return new List<Episode>();
        }

        public async Task<Episode> GetEpisodeByIdAsync(int id)
        {
            return await Task.FromResult(new Episode());
        }

        public Episode GetEpisodeByName(string name)
        {
            return new Episode();
        }

        public IEnumerable<Episode> GetEpisodesBySeasonId(int parentId)
        {
            return new List<Episode>();
        }

        public async Task<int> SaveEpisodeAsync(Episode target)
        {
            return await Task.FromResult(target != null ? 1 : 0);
        }

        public void Dispose()
        {
            
        }
    }
}