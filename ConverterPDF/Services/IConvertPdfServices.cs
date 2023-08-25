using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public interface IConvertPdfServices
    {
        void ConvertWordToPdf(List<string> pathWordFiles);
        void ConvertExcelToPdf(List<string> pathExcelFiles);
        void ConvertPowerPointToPdf(List<string> pathPowerPointFiles);
    }
}
