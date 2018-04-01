using System.Windows.Controls;
using FileManager.UI.ViewModels;

namespace FileManager.UI.Views
{
    /// <summary>
    /// Interaction logic for MovieListWindow.xaml
    /// </summary>
    public partial class MovieListWindow : UserControl
    {
        public MovieListWindowViewModel ViewModel => DataContext as MovieListWindowViewModel;

        public MovieListWindow()
        {
            InitializeComponent();

            DataContext = new MovieListWindowViewModel();
        }
    }
}