using ConverterPDF.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConverterPDF.Services
{
    public class SettingsServices : ISettingsServices
    {
        private static string pathSettings = Path.Combine(Environment.CurrentDirectory, "Settings.json");
        public SettingsModel CreateDefaultSettings() => new SettingsModel()
        {
            PathFolderLogs = Path.Combine(Environment.CurrentDirectory, "logs"),
            PathAbout = Path.Combine(Environment.CurrentDirectory, "About", "About.txt"),
            NameUnitePdf = "Unite",
            SelectedIsVisibleExcel = new KeyValuePair<string, bool>("Да", VisibleFileConverting.IsVisible["Да"]),
            SelectedIsVisibleWord = new KeyValuePair<string, bool>("Да", VisibleFileConverting.IsVisible["Да"]),
            SelectedPathFolderOpenFile = new KeyValuePair<string, string>("Рабочий стол", SpecialFolders.Folders["Рабочий стол"]),
            SelectedPathSavePdf = new KeyValuePair<string, string>("Рабочий стол", SpecialFolders.Folders["Рабочий стол"]),
            PathFolderSaveConverting = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            LibraryForConverting = LibraryforConverting.libraries[0]
        };

        public SettingsModel GetSettings()
        {
            if (!File.Exists(pathSettings))
            {
                var settings = CreateDefaultSettings();
                SaveSettings(settings);
                return settings;
            }

            using (var strReader = new StreamReader(pathSettings))
            {
                var strSettings = strReader.ReadToEnd();
                return JsonConvert.DeserializeObject<SettingsModel>(strSettings);
            }
        }

        public void SaveSettings(SettingsModel settings)
        {
            var strSettings = JsonConvert.SerializeObject(settings);
            using (var strWriter = new StreamWriter(new FileStream(pathSettings, FileMode.Create)))
            {
                strWriter.WriteLine(strSettings);
            }
        }

        public string? ShowFolderDialog()
        {
            string? pathFolder = null;
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                pathFolder = folderBrowserDialog.SelectedPath;
                return pathFolder;
            }

            return pathFolder;
        }
    }
}
