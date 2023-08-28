using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public class AppFacade
    {
        private IConvertPdfServices _converterPdf;
        private IGetPathFilesServices _pathFilesServices;
        private ILogsServices _logsService;
        private IMessageUser _messageUser;
        private static ILogger _logger;
        private static AppFacade _instance;
        private List<string> pathFiles;
        private static string pathFolderLogs = $"{Environment.CurrentDirectory}\\logs";
        private static string pathCurrentLogFile = $"{pathFolderLogs}\\{DateTime.Now.ToString("yyyy-MM-dd")}.log";
        private AppFacade(IConvertPdfServices convertPdfServices, IGetPathFilesServices getPathFilesServices, ILogsServices logsServices, IMessageUser messageUser, ILogger logger)
        {
            _converterPdf = convertPdfServices;
            _pathFilesServices = getPathFilesServices;
            _logsService = logsServices;
            _messageUser = messageUser;
            _logger = logger;
        }

        public static AppFacade GetInstance(IConvertPdfServices convertPdfServices, IGetPathFilesServices getPathFilesServices, ILogsServices logsServices, IMessageUser messageUser, ILogger logger)
        {
            if (_instance is null)
            {
                _instance = new AppFacade(convertPdfServices, getPathFilesServices, logsServices, messageUser, logger);
            }

            return _instance;
        }

        public void GetPath()
        {
            try
            {
                pathFiles = _pathFilesServices.GetPathFiles();
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }

        public void ConvertPdf()
        {
            if (pathFiles is null || pathFiles.Count == 0)
            {
                _messageUser.Warning("Не загружено ни одного файла!");
                return;
            }

            var pathExcelFiles = pathFiles
                .Where(pathFile => (Path.GetExtension(pathFile) is ".xlsx") || (Path.GetExtension(pathFile) is ".xls"))
                .ToList();
            var pathWordFiles = pathFiles
                .Where(pathFile => (Path.GetExtension(pathFile) is ".docx") || (Path.GetExtension(pathFile) is ".doc"))
                .ToList();
            var pathPowerPointFiles = pathFiles
                .Where(pathFile => Path.GetExtension(pathFile) is ".pptx")
                .ToList();

            try
            {
                if (pathExcelFiles.Count > 0)
                {
                    _logger.Info("Конвертация Excel");
                    Task.Run(() => _converterPdf.ConvertExcelToPdf(pathExcelFiles));
                }

                if (pathWordFiles.Count > 0)
                {
                    _logger.Info("Конвертация Word");
                    Task.Run(() => _converterPdf.ConvertWordToPdf(pathWordFiles));
                }

                if (pathPowerPointFiles.Count > 0)
                {
                    _logger.Info("Конвертация Power Point");
                    Task.Run(() => _converterPdf.ConvertPowerPointToPdf(pathPowerPointFiles));
                }
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }

        public void OpenCurrentLogFile() => _logsService.OpenCurrentLogFile(pathCurrentLogFile);
        public void OpenFolderLogs() => _logsService.OpenFolderLogs(pathFolderLogs);
        public void DeleteAllLogFiles() => _logsService.DeleteAllLogFiles(pathFolderLogs);
    }
}
