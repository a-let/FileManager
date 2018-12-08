using FileManager.Models;
using FileManager.DataAccessLayer.Interfaces;
using FileManager.Web.Services.Interfaces;

using System;
using System.Collections.Generic;

namespace FileManager.Web.Services
{
    public class SeriesControllerService : ISeriesControllerService
    {
        private readonly ISeriesRepository _seriesRepository;

        public SeriesControllerService(ISeriesRepository seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        public IEnumerable<Series> GetSeries()
        {
            return _seriesRepository.GetSeries();
        }

        public Series GetSeriesById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SeriesId");

            return _seriesRepository.GetSeriesById(id);
        }

        public Series GetSeriesByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _seriesRepository.GetSeriesByName(name);
        }

        public int SaveSeries(Series series)
        {
            if (series == null)
                throw new ArgumentNullException(nameof(series));

            return _seriesRepository.SaveSeries(series);
        }
    }
}