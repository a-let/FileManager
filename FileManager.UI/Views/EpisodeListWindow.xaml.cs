using System.Windows.Controls;
using FileManager.UI.ViewModels;

namespace FileManager.UI.Views
{
    /// <summary>
    /// Interaction logic for EpisodeListWindow.xaml
    /// </summary>
    public partial class EpisodeListWindow : UserControl
    {
        public EpisodeListWindowViewModel ViewModel => DataContext as EpisodeListWindowViewModel;

        public EpisodeListWindow()
        {
            InitializeComponent();
        }
    }
}