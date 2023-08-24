using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConverterPDF.Services
{
    internal class LogsServiceFacade : ILogsServiceFacade
    {
        private static ILogger _logger;
        private static IMessageUser _messageUser;

        public LogsServiceFacade(ILogger logger, IMessageUser messageUser)
        {
            _logger = logger;
            _messageUser = messageUser;
        }
        public void DeleteAllLogFiles(string pathFolderLogFile)
        {
            _logger.Info("Удаление log-файлов");

            try
            {
                int countDeleteFile = 0;
                var directiryInfo = new DirectoryInfo(pathFolderLogFile);

                foreach (FileInfo logFile in directiryInfo.EnumerateFiles())
                {
                    logFile.Delete();
                    countDeleteFile++;
                }

                _messageUser.Info($"Удално log-файлов: {countDeleteFile}");
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");               
            }
        }

        public void OpenCurrentLogFile(string pathCurrentLogFile)
        {
            _logger.Info("Откртыие директории с log-файлами");

            try
            {              
                OpenLog(pathCurrentLogFile);
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }

        public void OpenFolderLogs(string pathFolderLogs)
        {
            _logger.Info("Открытие директории с log-файлами");

            try
            {              
                OpenLog(pathFolderLogs);
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }

        private void OpenLog(string path)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.UseShellExecute = true;
            processStartInfo.FileName = path;
            Process.Start(processStartInfo);
        }
    }
}
