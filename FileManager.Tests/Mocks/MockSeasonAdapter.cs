using System.Collections.Generic;
using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;

namespace FileManager.Tests.Mocks
{
    public class MockSeasonAdapter : IFileManagerObjectAdapter<Season>
    {
        public IEnumerable<Season> Get()
        {
            return new List<Season>();
        }

        public Season GetById(int id)
        {
            return new Season();
        }

        public Season GetByName(string name)
        {
            return new Season();
        }

        public IEnumerable<Season> GetByParentId(int parentId)
        {
            return new List<Season>();
        }

        public bool Save(Season target)
        {
            return target != null;
        }
    }
}