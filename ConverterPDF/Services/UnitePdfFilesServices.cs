using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public class UnitePdfFilesServices : IUnitePdfFileServices
    {
        public void UnitePdfFiles(List<string> pathFiles, string outputPathFile)
        {
            PdfDocument.MergeFiles(pathFiles.ToArray(), outputPathFile);            
        }
    }
}
