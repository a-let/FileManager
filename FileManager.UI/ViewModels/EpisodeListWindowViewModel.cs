using FileManager.BusinessLayer;
using FileManager.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.UI.ViewModels
{
    public class EpisodeListWindowViewModel : IFileManagerViewModel
    {
        public Action OpenWindow { get; set; }

        public Episode Episode { get; set; }
        public IEnumerable<Episode> EpisodeList { get; set; }

        public EpisodeListWindowViewModel()
        {
            EpisodeList = Episode.GetEpisodes();
        }
    }
}