using System;
using System.Collections.Generic;

using FileManager.BusinessLayer;
using FileManager.UI.Interfaces;

namespace FileManager.UI.ViewModels
{
    public class ShowListWindowViewModel : IFileManagerViewModel
    {
        public Action<IFileManagerViewModel> OpenWindow { get; set; }

        public Show Show { get; set; }
        public IEnumerable<Show> ShowList { get; set; }

        public ShowListWindowViewModel()
        {
            ShowList = Show.GetShows();
        }
    }
}