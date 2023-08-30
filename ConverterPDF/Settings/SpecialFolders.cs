using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Settings
{
    public class SpecialFolders
    {
        private static Dictionary<string, string> _folders = new Dictionary<string, string>
        {
            {"Рабочий стол", Environment.GetFolderPath(Environment.SpecialFolder.Desktop)},
            {"Мои документы" , Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)},
            {"Моя музыка" , Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)},
            {"Мои картинки" , Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)},
            {"Мои видео" , Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)},
            {"Program Files" , Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)},
            {"Program Files x86" , Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)},
            {"Application Data" , Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)},

        };
        public static Dictionary<string, string> Folders { get => _folders; }
    }
}
