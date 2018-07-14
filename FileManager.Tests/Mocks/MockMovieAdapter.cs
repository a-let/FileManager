using System.Collections.Generic;
using FileManager.BusinessLayer.Interfaces;
using FileManager.Models;

namespace FileManager.Tests.Mocks
{
    public class MockMovieAdapter : IFileManagerObjectRepository<Movie>
    {
        public IEnumerable<Movie> Get()
        {
            return new List<Movie>();
        }

        public Movie GetById(int id)
        {
            return new Movie();
        }

        public Movie GetByName(string name)
        {
            return new Movie();
        }

        public IEnumerable<Movie> GetByParentId(int parentId)
        {
            return new List<Movie>();
        }

        public bool Save(Movie target)
        {
            return target != null;
        }
    }
}