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
using System.Windows.Shapes;

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

            DataContext = new EpisodeListWindowViewModel();
        }
    }
}