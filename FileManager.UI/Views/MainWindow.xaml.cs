using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FileManager.UI.Interfaces;
using FileManager.UI.ViewModels;

namespace FileManager.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel(OpenWindow, CloseWindow);

            this.Show();
        }   

        private void OpenWindow(IFileManagerViewModel viewModel)
        {
            if (viewModel is EpisodeListWindowViewModel)
                OpenWindow(viewModel, new EpisodeListWindow(), "Episodes");
            else if (viewModel is EpisodeMaintenanceWindowViewModel)
                OpenWindow(viewModel, new EpisodeMaintenanceWindow(), "Episode Maintenance");
            else if (viewModel is MovieListWindowViewModel)
                OpenWindow(viewModel, new MovieListWindow(), "Movies");
            else if (viewModel is SeasonListWindowViewModel)
                OpenWindow(viewModel, new SeasonListViewWindow(), "Seasons");
            else if (viewModel is ShowListWindowViewModel)
                OpenWindow(viewModel, new ShowListViewWindow(), "Shows");
            else if (viewModel is SeriesListWindowViewModel)
                OpenWindow(viewModel, new SeriesListViewWindow(), "Series");
            else
                throw new NotImplementedException();
        }

        private void OpenWindow(IFileManagerViewModel viewModel, UserControl control, string title)
        {
            Window window = new Window()
            {
                DataContext = viewModel,
                Content = control,
                Title = title
            };

            window.Show();
        }

        private void CloseWindow(string title)
        {
            var window = Application.Current.Windows.OfType<Window>()
                .Single(x => x is Window && x.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            window.Close();
        }
    }
}