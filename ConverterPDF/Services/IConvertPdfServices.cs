using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public interface IConvertPdfServices
    {
        void ConvertWordToPdf(List<string> pathWordFiles, string pathFolderSave, bool visible);
        void ConvertExcelToPdf(List<string> pathExcelFiles, string pathFolderSave, bool visible);
        void ConvertPowerPointToPdf(List<string> pathPowerPointFiles, string pathFolderSave);
    }
}
