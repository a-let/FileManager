using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using FileManager.BusinessLayer;
using FileManager.UI.Interfaces;

using GalaSoft.MvvmLight.Command;

namespace FileManager.UI.ViewModels
{
    public class EpisodeMaintenanceWindowViewModel : IFileManagerViewModel, IEditableObject
    {
        public Action<string> CloseWindow { get; set; }
        public Action<IFileManagerViewModel> OpenWindow { get; set; }
        public Action<string, string> DisplayMessage { get; set; }

        private Episode _episode;
        public Episode Episode { get; set; }

        public ICommand CancelCommand { get; set; }
        public RelayCommand<string> CopyCommand { get; set; }
        public RelayCommand<string> OpenFileLocationCommand { get; set; }

        public EpisodeMaintenanceWindowViewModel(Episode episode, Action<string> closeWindow, Action<string, string> displayMessage)
        {
            CloseWindow = closeWindow;
            DisplayMessage = displayMessage;

            InitCommands();
            Episode = episode;
            BeginEdit();
        }

        private void OpenFileLocation(string path)
        {
            if (!File.Exists(path))
            {
                DisplayMessage?.Invoke("Path not found.", "Invalid Path");
                return;
            }                

            Process.Start("explorer.exe", "/select, " + path);
        }

        private void Cancel()
        {
            CancelEdit();
            EndEdit();
            CloseWindow?.Invoke("Episode Maintenance");
        }

        private void InitCommands()
        {
            CancelCommand = new RelayCommand(Cancel);
            CopyCommand = new RelayCommand<string>((path) => Clipboard.SetText(path));
            OpenFileLocationCommand = new RelayCommand<string>(OpenFileLocation);
        }

        public void BeginEdit()
        {
            if (_episode == null)
                _episode = Episode.NewEpisode();

            _episode.EpisodeId = Episode.EpisodeId;
            _episode.SeasonId = Episode.SeasonId;
            _episode.Name = Episode.Name;
            _episode.EpisodeNumber = Episode.EpisodeNumber;
            _episode.Format = Episode.Format;
            _episode.Path = Episode.Path;
        }

        public void EndEdit()
        {
            _episode.EpisodeId = 0;
            _episode.SeasonId = 0;
            _episode.Name = string.Empty;
            _episode.EpisodeNumber = 0;
            _episode.Format = string.Empty;
            _episode.Path = string.Empty;
        }

        public void CancelEdit()
        {
            Episode.EpisodeId = _episode.EpisodeId;
            Episode.SeasonId = _episode.SeasonId;
            Episode.Name = _episode.Name;
            Episode.EpisodeNumber = _episode.EpisodeNumber;
            Episode.Format = _episode.Format;
            Episode.Path = _episode.Path;
        }
    }
}