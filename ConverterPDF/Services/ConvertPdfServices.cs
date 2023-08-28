using Microsoft.Office.Interop.Word;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office;
using Microsoft.Office.Core;

namespace ConverterPDF.Services
{
    public class ConvertPdfServices : IConvertPdfServices
    {
        public void ConvertExcelToPdf(List<string> pathExcelFiles)
        {
            var appExcel = new Excel.Application();
            appExcel.Visible = true;
            Excel.Workbook workbook = null;
            var pathFileConverting = string.Empty;

            try
            {
                foreach (var pathFile in pathExcelFiles)
                {
                    pathFileConverting = pathFile;

                    workbook = appExcel.Workbooks.Open(pathFile); //к вашей книге
                    appExcel.ActiveWorkbook.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, Path.ChangeExtension(pathFile, ".pdf"));//куда сохраняете
                    workbook.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при конвертации файла: {pathFileConverting}", ex);
            }
            finally
            {
                workbook.Close();
                appExcel.Quit();
            }
        }

        public void ConvertPowerPointToPdf(List<string> pathPowerPointFiles)
        {
            var appPowerPoint = new Microsoft.Office.Interop.PowerPoint.Application();
            Microsoft.Office.Interop.PowerPoint.Presentation docPres = null;

            foreach (var pathFile in pathPowerPointFiles)
            {
                docPres = appPowerPoint.Presentations.Open(pathFile);
                appPowerPoint.ActivePresentation.ExportAsFixedFormat(Path.ChangeExtension(pathFile, ".pdf"), Microsoft.Office.Interop.PowerPoint.PpFixedFormatType.ppFixedFormatTypePDF);
                docPres.Close();
            }
            
            appPowerPoint.Quit();
        }
        public void ConvertWordToPdf(List<string> pathWordFiles)
        {
            var appWord = new Word.Application();
            appWord.Visible = true;
            Word.Document document = null;
            var pathFileConverting = string.Empty;

            try
            {
                foreach (var pathFile in pathWordFiles)
                {
                    pathFileConverting = pathFile;

                    document = appWord.Documents.Open(pathFile);
                    appWord.ActiveDocument.ExportAsFixedFormat(Path.ChangeExtension(pathFile, ".pdf"), Word.WdExportFormat.wdExportFormatPDF);
                    document.Close();
                }

                appWord.Quit();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при конвертации файла: {pathFileConverting}", ex);
            }
            finally
            {
                document.Close();
                appWord.Quit();
            }
        }
    }
}
