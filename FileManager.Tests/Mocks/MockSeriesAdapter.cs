using System.Collections.Generic;

using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;

namespace FileManager.Tests.Mocks
{
    public class MockSeriesAdapter : IFileManagerObjectAdapter<Series>
    {
        public IEnumerable<Series> Get()
        {
            return new List<Series>();
        }

        public Series GetById(int id)
        {
            return new Series();
        }

        public Series GetByName(string name)
        {
            return new Series();
        }

        public IEnumerable<Series> GetByParentId(int parentId)
        {
            return new List<Series>();
        }

        public bool Save(Series target)
        {
            return target != null;
        }
    }
}