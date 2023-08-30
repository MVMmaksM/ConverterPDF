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
        public SettingsWindow()
        {
            InitializeComponent();
            var settigs = new SettingsModel()
            {
                PathFolderLogs = @"dfgfdgdfg\dfgd\fg\dfg",
                PathAbout = @"fgdfg\df\gdf\g",
                NameUnitePdf = "unite",
                IndexFolderOpenFile = 1,
                IndexFolderSavePdf = 2,
                IndexIsVisibleExcel = 1,
                IndexIsVisibleWord = 1
            };

            this.DataContext = settigs;

            CmbBxPathFolderFile.DataContext = settigs;
            CmbBxPathFolderFile.ItemsSource = SpecialFolders.Folders;
            CmbBxPathFolderFile.DisplayMemberPath = "Key";

            CmbBxIsOpenExcel.DataContext = settigs;
            CmbBxIsOpenExcel.ItemsSource = VisibleFileConverting.IsVisible;
            CmbBxIsOpenExcel.DisplayMemberPath = "Key";


            CmbBxIsOpenWord.DataContext = settigs;
            CmbBxIsOpenWord.ItemsSource = VisibleFileConverting.IsVisible;
            CmbBxIsOpenWord.DisplayMemberPath = "Key";


            CmbBxFolderSaveUnitePdf.DataContext = settigs;
            CmbBxFolderSaveUnitePdf.ItemsSource = SpecialFolders.Folders;
            CmbBxFolderSaveUnitePdf.DisplayMemberPath = "Key";
            
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
