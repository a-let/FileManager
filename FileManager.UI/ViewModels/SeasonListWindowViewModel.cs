using System;
using System.Collections.Generic;

using FileManager.BusinessLayer;
using FileManager.UI.Interfaces;

namespace FileManager.UI.ViewModels
{
    public class SeasonListWindowViewModel : IFileManagerViewModel
    {
        public Action<IFileManagerViewModel> OpenWindow { get; set; }

        public Season Season { get; set; }
        public IEnumerable<Season> SeasonList { get; set; }

        public SeasonListWindowViewModel()
        {
            SeasonList = Season.GetSeasons();
        }
    }
}