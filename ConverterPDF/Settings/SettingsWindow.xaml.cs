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
        public SettingsWindow(SettingsModel settings)
        {
            InitializeComponent();           

            _settingsModel = settings;
            this.DataContext = settings;

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
           var s = _settingsModel;
        }
    }
}
