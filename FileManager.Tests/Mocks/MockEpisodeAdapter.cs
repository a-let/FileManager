using System.Collections.Generic;
using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;

namespace FileManager.Tests.Mocks
{
    public class MockEpisodeAdapter : IFileManagerObjectRepository<Episode>
    {
        public IEnumerable<Episode> Get()
        {
            return new List<Episode>();
        }

        public Episode GetById(int id)
        {
            return new Episode();
        }

        public Episode GetByName(string name)
        {
            return new Episode();
        }

        public IEnumerable<Episode> GetByParentId(int parentId)
        {
            return new List<Episode>();
        }

        public bool Save(Episode target)
        {
            return target != null;
        }
    }
}