using ConverterPDF.Services;
using ConverterPDF.Settings;
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

namespace ConverterPDF
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsModel _settingsModel;
        private ISettingsServices _settingsServices;
        private IMessageUser _messageUser;
        private ILoggerServices _logger;
        public SettingsWindow(SettingsModel settings, ISettingsServices settingsServices, IMessageUser messageUser, ILoggerServices logger)
        {
            InitializeComponent();

            _logger = logger;
            _messageUser = messageUser;
            _settingsModel = settings;
            _settingsServices = settingsServices;

            this.DataContext = _settingsModel;

            CmbBxPathFolderFile.DataContext = settings;
            CmbBxPathFolderFile.ItemsSource = SpecialFolders.Folders;
            CmbBxPathFolderFile.DisplayMemberPath = "Key";


            CmbBxIsOpenExcel.DataContext = settings;
            CmbBxIsOpenExcel.ItemsSource = VisibleFileConverting.IsVisible;
            CmbBxIsOpenExcel.DisplayMemberPath = "Key";


            CmbBxIsOpenWord.DataContext = settings;
            CmbBxIsOpenWord.ItemsSource = VisibleFileConverting.IsVisible;
            CmbBxIsOpenWord.DisplayMemberPath = "Key";


            CmbBxFolderSaveUnitePdf.DataContext = settings;
            CmbBxFolderSaveUnitePdf.ItemsSource = SpecialFolders.Folders;
            CmbBxFolderSaveUnitePdf.DisplayMemberPath = "Key";
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _settingsServices.SaveSettings(_settingsModel);
                _messageUser.Info("Настройки успешно сохранены!");           
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
    }
}
