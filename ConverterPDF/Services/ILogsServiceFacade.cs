using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public interface ILogsServiceFacade
    {
        void OpenCurrentLogFile(string pathCurrentLogFile);
        void OpenFolderLogs(string pathFolderLogs);
        void DeleteAllLogFiles(string pathFolderLogFile);
    }
}
