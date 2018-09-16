using System.Collections.Generic;

using FileManager.Models;
using FileManager.Web.Services.Interfaces;

namespace FileManager.Tests.Mocks
{
    public class MockSeriesControllerService : ISeriesControllerService
    {
        public IEnumerable<Series> GetSeries()
        {
            return new List<Series>();
        }

        public Series GetSeriesById(int id)
        {
            return id != 1 ? null : new Series
            {
                SeriesId = 1
            };
        }

        public Series GetSeriesByName(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : new Series
            {
                Name = "Test Movie"
            };
        }

        public bool SaveSeries(Series series)
        {
            return series != null;
        }
    }
}