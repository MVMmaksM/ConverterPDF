using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF
{
    internal class LogsServiceFacade : ILogsServiceFacade
    {
        private static ILogger _logger;  

        public LogsServiceFacade(ILogger logger)
        {
            _logger = logger;
        }
        public int DeleteAllLogFiles(string pathFolderLogFile)
        {
            throw new NotImplementedException();
        }

        public void OpenCurrenLogFile(string pathCurrentLogFile)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.UseShellExecute = true;
                processStartInfo.FileName = pathCurrentLogFile;
                Process.Start(processStartInfo);

                _logger.Info("Откртыие директории с log-файлами");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + ex.StackTrace);
            }
        }

        public void OpenFolderLogs(string pathFolderLogs)
        {
            throw new NotImplementedException();
        }
    }
}
