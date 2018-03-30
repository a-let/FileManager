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
using FileManager.UI.ViewModels;

namespace FileManager.UI.Views
{
    /// <summary>
    /// Interaction logic for SeasonListViewWindow.xaml
    /// </summary>
    public partial class SeasonListViewWindow : Window
    {
        public SeasonListWindowViewModel ViewModel => DataContext as SeasonListWindowViewModel;

        public SeasonListViewWindow()
        {
            InitializeComponent();
            DataContext = new SeasonListWindowViewModel();
        }
    }
}