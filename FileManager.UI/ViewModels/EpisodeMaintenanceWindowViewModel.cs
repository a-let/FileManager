using System;
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

        public Episode Episode { get; set; }

        public ICommand CancelCommand { get; set; }
        public RelayCommand<string> CopyCommand { get; set; }

        public EpisodeMaintenanceWindowViewModel(Episode episode, Action<string> closeWindow)
        {
            CloseWindow = closeWindow;
            InitCommands();
            Episode = episode;
        }

        private void InitCommands()
        {
            CancelCommand = new RelayCommand(() => CloseWindow?.Invoke("Episode Maintenance"));
            CopyCommand = new RelayCommand<string>((path) => Clipboard.SetText(path));
        }
    }
}