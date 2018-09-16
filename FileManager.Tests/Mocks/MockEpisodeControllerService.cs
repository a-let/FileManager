using FileManager.Models;
using FileManager.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.Tests.Mocks
{
    public class MockEpisodeControllerService : IEpisodeControllerService
    {
        public Episode GetEpisodeById(int id)
        {
            return id != 1 ? null : new Episode
            {
                EpisodeId = 1
            };
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

        public IQueryable<Episode> GetEpisodesBySeasonId(int seasonId)
        {
            return new List<Episode>().AsQueryable();
        }

        public bool SaveEpisode(Episode episode)
        {
            return episode != null;
        }
    }
}