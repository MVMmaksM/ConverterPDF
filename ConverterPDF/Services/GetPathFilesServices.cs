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
        public List<string> GetPathFiles()
        {
            List<string> pathFiles = null;
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openFileDialog.DefaultExt = ".xlsx|.pptx|.docx";
            openFileDialog.Filter = "Microsoft Excel|*.xlsx;*.xls|Microsoft Word|*.docx;*.doc|Power Point|*.pptx|Все файлы|*.xlsx;*.xls;*.docx;*.doc;*.pptx";

            bool? dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                pathFiles = openFileDialog.FileNames.ToList<string>();
            }

            return pathFiles;
        }
    }
}
