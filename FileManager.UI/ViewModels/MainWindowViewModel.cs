using System;
using System.Windows.Input;

using FileManager.UI.Interfaces;

using GalaSoft.MvvmLight.Command;

namespace FileManager.UI.ViewModels
{    
    public class MainWindowViewModel
    {
        public Action<string> CloseWindow { get; set; }
        public Action<IFileManagerViewModel> OpenWindow { get; set; }
        
        public ICommand EpisodesClickCommand { get; set; }
        public ICommand MoviesClickCommand { get; set; }
        public ICommand SeasonsClickCommand { get; set; }
        public ICommand SeriesClickCommand { get; set; }
        public ICommand ShowsClickCommand { get; set; }

        public MainWindowViewModel(Action<IFileManagerViewModel> openWindow, Action<string> closeWindow)
        {
            CloseWindow = closeWindow;
            OpenWindow = openWindow;
            InitCommands();            
        }

        private void InitCommands()
        {
            EpisodesClickCommand = new RelayCommand(() => OpenWindow?.Invoke(new EpisodeListWindowViewModel(OpenWindow, CloseWindow)));
            MoviesClickCommand = new RelayCommand(() => OpenWindow?.Invoke(new MovieListWindowViewModel()));
            SeasonsClickCommand = new RelayCommand(() => OpenWindow?.Invoke(new SeasonListWindowViewModel()));
            SeriesClickCommand = new RelayCommand(() => OpenWindow?.Invoke(new SeriesListWindowViewModel()));
            ShowsClickCommand = new RelayCommand(() => OpenWindow?.Invoke(new ShowListWindowViewModel()));
        }
    }
}