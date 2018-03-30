using FileManager.BusinessLayer;
using FileManager.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.UI.ViewModels
{
    public class SeasonListWindowViewModel : IFileManagerViewModel
    {
        public Action OpenWindow { get; set; }

        public Season Season { get; set; }
        public IEnumerable<Season> SeasonList { get; set; }

        public SeasonListWindowViewModel()
        {
            SeasonList = Season.GetSeasons();
        }
    }
}