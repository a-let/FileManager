using FileManager.BusinessLayer;
using FileManager.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.UI.ViewModels
{
    public class ShowListWindowViewModel : IFileManagerViewModel
    {
        public Action OpenWindow { get; set; }

        public Show Show { get; set; }
        public IEnumerable<Show> ShowList { get; set; }

        public ShowListWindowViewModel()
        {
            ShowList = Show.GetShows();
        }
    }
}