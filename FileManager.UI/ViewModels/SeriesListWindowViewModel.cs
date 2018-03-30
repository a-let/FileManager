using FileManager.BusinessLayer;
using FileManager.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.UI.ViewModels
{
    public class SeriesListWindowViewModel : IFileManagerViewModel
    {
        public Action OpenWindow { get; set; }

        public Series Series { get; set; }
        public IEnumerable<Series> SeriesList { get; set; }

        public SeriesListWindowViewModel()
        {
            SeriesList = Series.GetSeries();
        }
    }
}