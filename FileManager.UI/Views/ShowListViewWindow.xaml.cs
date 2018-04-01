using System.Windows.Controls;
using FileManager.UI.ViewModels;

namespace FileManager.UI.Views
{
    /// <summary>
    /// Interaction logic for ShowListViewWindow.xaml
    /// </summary>
    public partial class ShowListViewWindow : UserControl
    {
        ShowListWindowViewModel ViewModel => DataContext as ShowListWindowViewModel;

        public ShowListViewWindow()
        {
            InitializeComponent();
            DataContext = new ShowListWindowViewModel();
        }
    }
}