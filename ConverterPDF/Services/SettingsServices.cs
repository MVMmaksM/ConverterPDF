using ConverterPDF.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public class SettingsServices : ISettingsServices
    {
        public SettingsModel CreateDefaultSettings() => new SettingsModel()
        {
            PathFolderLogs = Path.Combine(Environment.CurrentDirectory, "logs"),
            PathAbout = Path.Combine(Environment.CurrentDirectory, "About", "About.txt"),
            NameUnitePdf = "Unite",
            SelectedIsVisibleExcel = new KeyValuePair<string, bool>("Да", VisibleFileConverting.IsVisible["Да"]),
            SelectedIsVisibleWord = new KeyValuePair<string, bool>("Да", VisibleFileConverting.IsVisible["Да"]),
            SelectedPathFolderOpenFile = new KeyValuePair<string, string>("Рабочий стол", SpecialFolders.Folders["Рабочий стол"]),
            SelectedPathSavePdf = new KeyValuePair<string, string>("Рабочий стол", SpecialFolders.Folders["Рабочий стол"])
        };

        public SettingsModel GetSettings()
        {
            throw new NotImplementedException();
        }

        public void SaveSettings(SettingsModel settings)
        {
            throw new NotImplementedException();
        }
    }
}
