using System.Collections.Generic;
using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;

namespace FileManager.Tests.Mocks
{
    public class MockShowAdapter : IFileManagerObjectAdapter<Show>
    {
        public IEnumerable<Show> Get()
        {
            return new List<Show>();
        }

        public Show GetById(int id)
        {
            return new Show();
        }

        public Show GetByName(string name)
        {
            return new Show();
        }

        public IEnumerable<Show> GetByParentId(int parentId)
        {
            return new List<Show>();
        }

        public bool Save(Show target)
        {
            return target != null;
        }
    }
}