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
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        private static IMessageUser _messageUser = new MessageUser();
        private static AppFacade _appFacade;
        public MainWindow()
        {
            InitializeComponent();
            _logger.Info("Запуск приложения");

            _appFacade = AppFacade.GetInstance(new UnitePdfFilesServices(), new ConvertPdfServices(), new GetPathFilesServices(), new LogsServices(_logger, _messageUser), _messageUser, _logger);

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
    }
}
