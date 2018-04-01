using System.Windows.Controls;
using FileManager.UI.ViewModels;

namespace FileManager.UI.Views
{
    /// <summary>
    /// Interaction logic for SeriesListWindow.xaml
    /// </summary>
    public partial class SeriesListViewWindow : UserControl
    {
        public SeriesListWindowViewModel ViewModel => DataContext as SeriesListWindowViewModel;

        public SeriesListViewWindow()
        {
            InitializeComponent();

            DataContext = new SeriesListWindowViewModel();
        }
    }
}