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
        private SettingsModel _prototypeSettings;
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
            _prototypeSettings = _settingsModel.Clone();

            this.DataContext = _prototypeSettings;

            CmbBxLibConvert.DataContext = _prototypeSettings;
            CmbBxLibConvert.ItemsSource = LibraryforConverting.libraries;           

            CmbBxPathFolderFile.DataContext = _prototypeSettings;
            CmbBxPathFolderFile.ItemsSource = SpecialFolders.Folders;
            CmbBxPathFolderFile.DisplayMemberPath = "Key";


            CmbBxIsOpenExcel.DataContext = _prototypeSettings;
            CmbBxIsOpenExcel.ItemsSource = VisibleFileConverting.IsVisible;
            CmbBxIsOpenExcel.DisplayMemberPath = "Key";


            CmbBxIsOpenWord.DataContext = _prototypeSettings;
            CmbBxIsOpenWord.ItemsSource = VisibleFileConverting.IsVisible;
            CmbBxIsOpenWord.DisplayMemberPath = "Key";


            CmbBxFolderSaveUnitePdf.DataContext = _prototypeSettings;
            CmbBxFolderSaveUnitePdf.ItemsSource = SpecialFolders.Folders;
            CmbBxFolderSaveUnitePdf.DisplayMemberPath = "Key";
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _logger.Info("Сохранение настроек");
                _settingsModel.NameUnitePdf = _prototypeSettings.NameUnitePdf;
                _settingsModel.PathAbout = _prototypeSettings.PathAbout;
                _settingsModel.PathFolderLogs = _prototypeSettings.PathFolderLogs;
                _settingsModel.PathFolderSaveConverting = _prototypeSettings.PathFolderSaveConverting;
                _settingsModel.SelectedPathFolderOpenFile = _prototypeSettings.SelectedPathFolderOpenFile;
                _settingsModel.SelectedPathSavePdf = _prototypeSettings.SelectedPathSavePdf;
                _settingsModel.SelectedIsVisibleExcel = _prototypeSettings.SelectedIsVisibleExcel;
                _settingsModel.SelectedIsVisibleWord = _prototypeSettings.SelectedIsVisibleWord;               
                _settingsModel.LibraryForConverting = _prototypeSettings.LibraryForConverting;               

                _settingsServices.SaveSettings(_settingsModel);
                _messageUser.Info("Настройки успешно сохранены!");
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }

        private void BtnOpenFolderDialog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var pathFolderSaveConverting = _settingsServices.ShowFolderDialog();
                if (pathFolderSaveConverting is not null)
                    _prototypeSettings.PathFolderSaveConverting = pathFolderSaveConverting;
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }

        private void CmbBxLibConvert_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {           

            if (CmbBxLibConvert.SelectedValue.ToString().Contains("Microsoft Office Interop"))
            {
                CmbBxIsOpenWord.IsEnabled = true;
                CmbBxIsOpenExcel.IsEnabled = true;
            }
            else if(CmbBxLibConvert.SelectedValue.ToString().Contains("iTextSharp"))
            {
                CmbBxIsOpenWord.IsEnabled = false;
                CmbBxIsOpenExcel.IsEnabled = false;                
            }
        }
    }
}
