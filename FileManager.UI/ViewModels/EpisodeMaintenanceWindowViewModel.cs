using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using FileManager.BusinessLayer;
using FileManager.UI.Interfaces;

using GalaSoft.MvvmLight.Command;

namespace FileManager.UI.ViewModels
{
    public class EpisodeMaintenanceWindowViewModel : IFileManagerViewModel
    {
        public Action<string> CloseWindow { get; set; }
        public Action<IFileManagerViewModel> OpenWindow { get; set; }
        public Action<string, string> DisplayMessage { get; set; }

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

        private void InitCommands()
        {
            CancelCommand = new RelayCommand(() => CloseWindow?.Invoke("Episode Maintenance"));
            CopyCommand = new RelayCommand<string>((path) => Clipboard.SetText(path));
            OpenFileLocationCommand = new RelayCommand<string>(OpenFileLocation);
        }
    }
}