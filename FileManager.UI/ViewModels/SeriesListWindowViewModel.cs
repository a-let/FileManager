using System;
using System.Collections.Generic;

using FileManager.BusinessLayer;
using FileManager.UI.Interfaces;

namespace FileManager.UI.ViewModels
{
    public class SeriesListWindowViewModel : IFileManagerViewModel
    {
        public Action<IFileManagerViewModel> OpenWindow { get; set; }

        public Series Series { get; set; }
        public IEnumerable<Series> SeriesList { get; set; }

        public SeriesListWindowViewModel()
        {
            SeriesList = Series.GetSeries();
        }
    }
}