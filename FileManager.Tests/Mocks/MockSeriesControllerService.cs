using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockSeriesControllerService : ISeriesControllerService
    {
        public IEnumerable<Series> GetSeries() => new List<Series>();

        public async Task<Series> GetSeriesByIdAsync(int id) => await Task.FromResult(id != 1 ? null : new Series
        {
            SeriesId = 1
        });

        public Series GetSeriesByName(string name) => string.IsNullOrWhiteSpace(name) ? null : new Series
        {
            Name = "Test Movie"
        };

        public async Task<int> SaveSeriesAsync(Series series) => await Task.FromResult(series != null ? 1 : 0);
    }
}