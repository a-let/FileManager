using FileManager.Models;
using FileManager.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockEpisodeControllerService : IEpisodeControllerService
    {
        public async Task<Episode> GetEpisodeByIdAsync(int id)
        {
            return await Task.FromResult(id != 1 ? null : new Episode
            {
                EpisodeId = 1
            });
        }

        public Episode GetEpisodeByName(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : new Episode
            {
                Name = "Test Episode"
            };
        }

        public IEnumerable<Episode> GetEpisodes()
        {
            return new List<Episode>();
        }

        public IEnumerable<Episode> GetEpisodesBySeasonId(int seasonId)
        {
            return new List<Episode>();
        }

        public async Task<int> SaveEpisodeAsync(Episode episode)
        {
            return await Task.FromResult(episode != null ? 1 : 0);
        }
    }
}