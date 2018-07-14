using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.Web.Services
{
    public class SeriesControllerService : ISeriesControllerService
    {
        private readonly IFileManagerObjectRepository<Series> _seriesRepository;

        public SeriesControllerService(IFileManagerObjectRepository<Series> seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        public IEnumerable<Series> GetSeries()
        {
            return _seriesRepository.Get();
        }

        public Series GetSeriesById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SeriesId");

            return _seriesRepository.GetById(id);
        }

        public Series GetSeriesByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _seriesRepository.GetByName(name);
        }

        public bool SaveSeries(Series series)
        {
            if (series == null)
                throw new ArgumentNullException(nameof(series));

            return _seriesRepository.Save(series);
        }
    }
}