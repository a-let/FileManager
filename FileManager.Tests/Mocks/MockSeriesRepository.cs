using FileManager.DataAccessLayer.Interfaces;
using FileManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockSeriesRepository : ISeriesRepository
    {
        public IEnumerable<Series> GetSeries() => new List<Series>();

        public async Task<Series> GetSeriesByIdAsync(int id) => await Task.FromResult(new Series());

        public Series GetSeriesByName(string name) => new Series();

        public async Task<int> SaveSeriesAsync(Series series) => await Task.FromResult(series != null ? 1 : 0);
    }
}