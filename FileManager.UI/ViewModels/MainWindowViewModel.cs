using GalaSoft.MvvmLight.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FileManager.UI.Interfaces;

namespace FileManager.UI.ViewModels
{    
    public class MainWindowViewModel
    {
        public Action<IFileManagerViewModel> OpenWindow { get; set; }

        public ICommand EpisodesClickCommand { get; set; }
        public ICommand MoviesClickCommand { get; set; }
        public ICommand SeasonsClickCommand { get; set; }
        public ICommand SeriesClickCommand { get; set; }
        public ICommand ShowsClickCommand { get; set; }

        public MainWindowViewModel(Action<IFileManagerViewModel> openWindow)
        {
            InitCommands();

            OpenWindow = openWindow;
        }

        private void InitCommands()
        {
            EpisodesClickCommand = new RelayCommand(() => OpenWindow?.Invoke(new EpisodeListWindowViewModel()));
            MoviesClickCommand = new RelayCommand(() => OpenWindow?.Invoke(new MovieListWindowViewModel()));
            SeasonsClickCommand = new RelayCommand(() => OpenWindow?.Invoke(new SeasonListWindowViewModel()));
            SeriesClickCommand = new RelayCommand(() => OpenWindow?.Invoke(new SeriesListWindowViewModel()));
            ShowsClickCommand = new RelayCommand(() => OpenWindow?.Invoke(new ShowListWindowViewModel()));
        }
    }
}