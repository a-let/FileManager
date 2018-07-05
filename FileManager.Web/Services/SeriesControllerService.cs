﻿using System;
using System.Collections.Generic;
using FileManager.Models;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.Web.Services
{
    public class SeriesControllerService : ISeriesControllerService
    {
        private readonly IFileManagerObjectAdapter<Series> _seriesAdapter;

        public SeriesControllerService(IFileManagerObjectAdapter<Series> seriesAdapter)
        {
            _seriesAdapter = seriesAdapter;
        }

        public IEnumerable<Series> GetSeries()
        {
            return _seriesAdapter.Get();
        }

        public Series GetSeriesById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid SeriesId");

            return _seriesAdapter.GetById(id);
        }

        public Series GetSeriesByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _seriesAdapter.GetByName(name);
        }

        public bool SaveSeries(Series series)
        {
            if (series == null)
                throw new ArgumentNullException(nameof(series));

            return _seriesAdapter.Save(series);
        }
    }
}