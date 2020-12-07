using FileManager.Models;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class SeriesControllerService : ISeriesControllerService
    {
        private readonly ISeriesRepository _seriesRepository;

        public SeriesControllerService(ISeriesRepository seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        public async Task<IEnumerable<Series>> GetAsync()
        {
            return await Task.Run(() => _seriesRepository.GetSeries());
        }

        public async Task<Series> GetAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SeriesId");

            return await _seriesRepository.GetSeriesByIdAsync(id);
        }

        public async Task<Series> GetAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return await Task.Run(() => _seriesRepository.GetSeriesByName(name));
        }

        public async Task<int> SaveAsync(Series series)
        {
            if (series == null)
                throw new ArgumentNullException(nameof(series));

            return await _seriesRepository.SaveSeriesAsync(series);
        }
    }
}