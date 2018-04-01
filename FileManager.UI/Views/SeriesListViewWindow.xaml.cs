﻿using FileManager.UI.ViewModels;
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