using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF
{
    public interface ILogsServiceFacade
    {
        void OpenCurrenLogFile(string pathCurrentLogFile);
        void OpenFolderLogs(string pathFolderLogs);
        int DeleteAllLogFiles(string pathFolderLogFile);
    }
}
