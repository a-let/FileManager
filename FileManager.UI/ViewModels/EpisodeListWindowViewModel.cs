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
        public Action<string> CloseWindow { get; set; }
        public Action<IFileManagerViewModel> OpenWindow { get; set; }
        public Action<string, string> DisplayMessage { get; set; }

        public IEnumerable<Episode> EpisodeList { get; set; }

        public ICommand AddNewEpisodeCommand { get; set; }
        public RelayCommand<Episode> DoubleClickEpisodeCommand { get; set; }

        public EpisodeListWindowViewModel(Action<IFileManagerViewModel> openWindow, Action<string> closeWindow, Action<string, string> displayMessage)
        {
            CloseWindow = closeWindow;
            OpenWindow = openWindow;
            DisplayMessage = displayMessage;

            InitCommands();            

            EpisodeList = Episode.GetEpisodes();
        }

        private void AddNewEpisode()
        {
            OpenWindow?.Invoke(new EpisodeMaintenanceWindowViewModel(Episode.NewEpisode(), CloseWindow, DisplayMessage));
        }

        private void DoubleClickEpisode(Episode episode)
        {
            if (episode == null)
                return;

            OpenWindow?.Invoke(new EpisodeMaintenanceWindowViewModel(episode, CloseWindow, DisplayMessage));
        }

        private void InitCommands()
        {
            AddNewEpisodeCommand = new RelayCommand(AddNewEpisode);
            DoubleClickEpisodeCommand = new RelayCommand<Episode>(DoubleClickEpisode);
        }
    }
}