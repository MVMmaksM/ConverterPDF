using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public interface IAppFacade
    {
        void OpenCurrentLogFile();
        void OpenFolderLogs();
        void DeleteAllLogFiles();
        void ConvertPdf();
        void GetPathForConverting();
        void GetPathForUnite();
        void UnitePdf();
        void ShowAbout();
        void ShowSettings(MainWindow mainWindow);
        void ClearSelectFileConvert(MainWindow mainWindow);
        void ClearSelectFileUnite(MainWindow mainWindow);
    }
}
