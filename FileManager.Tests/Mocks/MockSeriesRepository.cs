using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;
using System.Collections.Generic;

namespace FileManager.Tests.Mocks
{
    public class MockSeriesRepository : ISeriesRepository
    {
        public IEnumerable<Series> GetSeries()
        {
            return new List<Series>();
        }

        public Series GetSeriesById(int id)
        {
            return new Series();
        }

        public Series GetSeriesByName(string name)
        {
            return new Series();
        }

        public bool SaveSeries(Series series)
        {
            return series != null;
        }

        public void Dispose()
        {
            
        }
    }
}