using ConverterPDF.Services;
using NLog;
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
        private static ILoggerServices _logger;
        private static IAppFacade _appFacade;
        private static IShowInfoUserServices _showInfoUserServices;
        public MainWindow(IAppFacade appFacade, ILoggerServices loggerServices, IShowInfoUserServices showInfoUserServices)
        {
            InitializeComponent();

            _logger = loggerServices;
            _logger.Info("Запуск приложения");

            _showInfoUserServices = showInfoUserServices;
            _showInfoUserServices.AppFacadeNotify += ShowInfo;
            _appFacade = appFacade;
            _appFacade.ShowVersionApp(this);
        }
        private void MenuOpenCurrentLog_Click(object sender, RoutedEventArgs e)
        {
            _appFacade.OpenCurrentLogFile();
        }

        private void MenuOpenFolderLog_Click(object sender, RoutedEventArgs e)
        {
            _appFacade.OpenFolderLogs();
        }

        private void MenuDeleteAllLogs_Click(object sender, RoutedEventArgs e)
        {
            _appFacade.DeleteAllLogFiles();
        }

        private void ConvertToPDF_Click(object sender, RoutedEventArgs e)
        {
            _appFacade.ConvertPdf();
        }

        private void LoadFileForConverting_Click(object sender, RoutedEventArgs e)
        {
            _appFacade.GetPathForConverting();
        }

        private void LoadPdfForUnite_Click(object sender, RoutedEventArgs e)
        {
            _appFacade.GetPathForUnite();
        }

        private void UnitePdf_Click(object sender, RoutedEventArgs e)
        {
            _appFacade.UnitePdf();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _logger.Info("Закрытие приложения");
            App.Current.Shutdown(); // закрывает процесс 
        }
        private void ShowInfo(string message) => Info.Text += message;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            _appFacade.ShowAbout();
        }
        private void MenuOpenSettings_Click(object sender, RoutedEventArgs e)
        {
            _appFacade.ShowSettings(this);
        }
        private void ClearSelectFileConvert_Click(object sender, RoutedEventArgs e)
        {
            _appFacade.ClearSelectFileConvert(this);
        }

        private void ClearSelectFileUnite_Click(object sender, RoutedEventArgs e)
        {
            _appFacade.ClearSelectFileUnite(this);
        }
    }
}
