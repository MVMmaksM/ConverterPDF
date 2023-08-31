using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office;
using Microsoft.Office.Core;

namespace ConverterPDF.Services
{
    public class ConvertPdfServices : IConvertPdfServices
    {
        public void ConvertExcelToPdf(List<string> pathExcelFiles, string pathFolderSave, bool visible)
        {
            var appExcel = new Excel.Application();
            appExcel.Visible = visible;
            Excel.Workbook? workbook = null;
            var pathFileConverting = string.Empty;

            try
            {
                foreach (var pathFile in pathExcelFiles)
                {
                    pathFileConverting = pathFile;
                    var fullNameConvertFile = Path.Combine(pathFolderSave, $"{Path.GetFileNameWithoutExtension(pathFile)}.pdf"); // полный путь сохранения

                    workbook = appExcel.Workbooks.Open(pathFile); //к вашей книге
                    appExcel.ActiveWorkbook.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, fullNameConvertFile);//куда сохраняете
                    workbook.Close();                    
                }
            }
            catch (Exception ex)
            {
                workbook?.Close();
                throw new Exception($"Ошибка при конвертации файла: {pathFileConverting}", ex);
            }
            finally
            {
                appExcel.Quit();
            }
        }

        public void ConvertPowerPointToPdf(List<string> pathPowerPointFiles, string pathFolderSave)
        {
            var appPowerPoint = new PowerPoint.Application();
            appPowerPoint.Visible = MsoTriState.msoTrue;
            PowerPoint.Presentation? docPres = null;
            var pathFileConverting = string.Empty;

            try
            {
                foreach (var pathFile in pathPowerPointFiles)
                {
                    pathFileConverting = pathFile;

                    var fullNameConvertFile = Path.Combine(pathFolderSave, $"{Path.GetFileNameWithoutExtension(pathFile)}.pdf"); // полный путь сохранения

                    docPres = appPowerPoint.Presentations.Open(pathFile);
                    appPowerPoint.ActivePresentation.ExportAsFixedFormat(fullNameConvertFile, PowerPoint.PpFixedFormatType.ppFixedFormatTypePDF);
                    docPres.Close();
                }                
            }
            catch (Exception ex)
            {
                docPres?.Close();
                throw new Exception($"Ошибка при конвертации файла: {pathFileConverting}", ex);
            }
            finally
            {                
                appPowerPoint.Quit();
            }
        }
        public void ConvertWordToPdf(List<string> pathWordFiles, string pathFolderSave, bool visible)
        {
            var appWord = new Word.Application();
            appWord.Visible = visible;
            Word.Document? document = null;
            var pathFileConverting = string.Empty;

            try
            {
                foreach (var pathFile in pathWordFiles)
                {
                    pathFileConverting = pathFile;

                    var fullNameConvertFile = Path.Combine(pathFolderSave, $"{Path.GetFileNameWithoutExtension(pathFile)}.pdf"); // полный путь сохранения

                    document = appWord.Documents.Open(pathFile);
                    appWord.ActiveDocument.ExportAsFixedFormat(fullNameConvertFile, Word.WdExportFormat.wdExportFormatPDF);
                    document.Close();
                }               
            }
            catch (Exception ex)
            {
                document?.Close();
                throw new Exception($"Ошибка при конвертации файла: {pathFileConverting}", ex);
            }
            finally
            {               
                appWord.Quit();
            }
        }
    }
}
