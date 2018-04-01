using System;
using System.Collections.Generic;
using System.Windows.Input;

using FileManager.BusinessLayer;
using FileManager.UI.Interfaces;

using GalaSoft.MvvmLight.Command;

namespace FileManager.UI.ViewModels
{
    public class EpisodeListWindowViewModel : IFileManagerViewModel
    {
        public Action OpenWindow { get; set; }

        public Episode Episode { get; set; }
        public IEnumerable<Episode> EpisodeList { get; set; }

        public ICommand AddNewEpisodeCommand { get; set; }

        public EpisodeListWindowViewModel()
        {
            InitCommands();
            EpisodeList = Episode.GetEpisodes();
        }

        private void AddNewEpisode()
        {

        }

        private void InitCommands()
        {
            AddNewEpisodeCommand = new RelayCommand(AddNewEpisode);
        }
    }
}