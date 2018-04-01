using FileManager.UI.Interfaces;
using FileManager.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            DataContext = new MainWindowViewModel(OpenWindow);

            this.Show();
        }

        private void OpenWindow(IFileManagerViewModel viewModel)
        {
            Window window;

            // TODO: Refactor to case/switch
            if(viewModel is EpisodeListWindowViewModel)
            {
                window = new Window()
                {
                    DataContext = viewModel,
                    Content = new EpisodeListWindow(),
                    Title = "Episodes"
                };

                window.Show();
            } 
            else if(viewModel is MovieListWindowViewModel)
            {
                window = new Window()
                {
                    DataContext = viewModel,
                    Content = new MovieListWindow(),
                    Title = "Movies"
                };

                window.Show();
            }
            else if(viewModel is SeasonListWindowViewModel)
            {
                window = new Window()
                {
                    DataContext = viewModel,
                    Content = new SeasonListViewWindow(),
                    Title = "Seasons"
                };

                window.Show();
            }
            else if(viewModel is SeriesListWindowViewModel)
            {
                window = new Window()
                {
                    DataContext = viewModel,
                    Content = new SeriesListViewWindow(),
                    Title = "Series"
                };

                window.Show();
            }
        }
    }
}