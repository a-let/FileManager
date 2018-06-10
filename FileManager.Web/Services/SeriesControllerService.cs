using System;
using System.Collections.Generic;
using FileManager.BusinessLayer;
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

        public bool SaveSeries(Series series)
        {
            if (series == null)
                throw new ArgumentNullException(nameof(series));

            return _seriesAdapter.Save(series);
        }
    }
}