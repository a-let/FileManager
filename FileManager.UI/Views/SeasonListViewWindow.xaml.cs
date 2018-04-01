using System.Windows.Controls;
using FileManager.UI.ViewModels;

namespace FileManager.UI.Views
{
    /// <summary>
    /// Interaction logic for SeasonListViewWindow.xaml
    /// </summary>
    public partial class SeasonListViewWindow : UserControl
    {
        public SeasonListWindowViewModel ViewModel => DataContext as SeasonListWindowViewModel;

        public SeasonListViewWindow()
        {
            InitializeComponent();
            DataContext = new SeasonListWindowViewModel();
        }
    }
}