using FileManager.Models;
using FileManager.Web.Services.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Tests.Mocks
{
    public class MockSeriesControllerService : ISeriesControllerService
    {
        public async Task<IEnumerable<Series>> GetAsync() => await Task.FromResult(new List<Series>());

        public async Task<Series> GetAsync(int id) => await Task.FromResult(id != 1 ? null : new Series
        {
            SeriesId = 1
        });

        public async Task<Series> GetAsync(string name) => await Task.FromResult(string.IsNullOrWhiteSpace(name) ? null : new Series
        {
            Name = "Test Movie"
        });

        public async Task<int> SaveAsync(Series series) => await Task.FromResult(series != null ? 1 : 0);
    }
}