using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Linq;

namespace FileManager.Tests.Mocks
{
    public class MockEpisodeRepository : IEpisodeRepository
    {
        public IEnumerable<Episode> GetEpisodes()
        {
            return new List<Episode>();
        }

        public Episode GetEpisodeById(int id)
        {
            return new Episode();
        }

        public Episode GetEpisodeByName(string name)
        {
            return new Episode();
        }

        public IQueryable<Episode> GetEpisodesBySeasonId(int parentId)
        {
            return new List<Episode>().AsQueryable();
        }

        public int SaveEpisode(Episode target)
        {
            return target != null ? 1 : 0;
        }

        public void Dispose()
        {
            
        }
    }
}