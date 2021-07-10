using FileManager.Models;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileManager.Web.Services
{
    public class SeriesControllerService : IControllerService<Series>
    {
        private readonly IRepository<Series> _seriesRepository;

        public SeriesControllerService(IRepository<Series> seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        public async Task<Series> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SeriesId");

            return await _seriesRepository.GetByIdAsync(id);
        }

        public IEnumerable<Series> Get() =>
            _seriesRepository.Get();

        public Series GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _seriesRepository.GetByName(name);
        }

        public async Task SaveAsync(Series series)
        {
            if (series == null)
                throw new ArgumentNullException(nameof(series));

            await _seriesRepository.SaveAsync(series);
        }
    }
}