﻿using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ConverterPDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        private static string pathFolderLogs = $"{Environment.CurrentDirectory}\\logs";
        private static string pathCurrentLogFile = $"{pathFolderLogs}\\{DateTime.Now.ToString("yyyy-MM-dd")}.log";
        public MainWindow()
        {
            InitializeComponent();
        }          
        private void MenuOpenCurrentLog_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void MenuOpenFolderLog_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuDeleteAllLogs_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
