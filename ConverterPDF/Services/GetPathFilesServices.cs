using Microsoft.Win32;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public class GetPathFilesServices : IGetPathFilesServices
    {
        public List<string> GetPathFiles(string defaultExtFile, string filter, string initialDirectory)
        {
            List<string> pathFiles = null;
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = initialDirectory;
            openFileDialog.DefaultExt = defaultExtFile;
            openFileDialog.Filter = filter;

            bool? dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                pathFiles = openFileDialog.FileNames.ToList<string>();
            }

            return pathFiles;
        }
    }
}
