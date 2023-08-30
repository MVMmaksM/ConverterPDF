using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Settings
{
    public class SpecialFolders
    {
        private static Dictionary<string, Environment.SpecialFolder> _folders = new Dictionary<string, Environment.SpecialFolder>
        {
            {"Рабочий стол", Environment.SpecialFolder.Desktop },
            { "Мой компьютер", Environment.SpecialFolder.MyComputer},
            {"Мои документы" , Environment.SpecialFolder.MyDocuments },
            {"Моя музыка" , Environment.SpecialFolder.MyMusic },
            {"Мои картинки" , Environment.SpecialFolder.MyPictures },
            {"Мои видео" , Environment.SpecialFolder.MyVideos },
            {"Program Files" , Environment.SpecialFolder.ProgramFiles },
            {"Program Files x86" , Environment.SpecialFolder.ProgramFilesX86 },
            {"Application Data" , Environment.SpecialFolder.ApplicationData },

        };

        public static Dictionary<string, Environment.SpecialFolder> Folders { get => _folders; }
    }
}
