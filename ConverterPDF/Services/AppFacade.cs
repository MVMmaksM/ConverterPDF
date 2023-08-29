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
        private IUnitePdfFileServices _unitePdfFileServices;
        private static ILogger _logger;
        private static AppFacade _instance;
        private List<string> pathFilesForConverting;
        private List<string> pathFilesForUnite = new List<string>();
        private static string pathFolderLogs = $"{Environment.CurrentDirectory}\\logs";
        private static string pathCurrentLogFile = $"{pathFolderLogs}\\{DateTime.Now.ToString("yyyy-MM-dd")}.log";
        private string filterFileConverting = "Microsoft Excel|*.xlsx;*.xls|Microsoft Word|*.docx;*.doc|Power Point|*.pptx|Все файлы|*.xlsx;*.xls;*.docx;*.doc;*.pptx";
        private string filterFileUnite = "PDF|*.pdf";
        private string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private string defaultExtConverting = ".xlsx|.pptx|.docx";
        private string defaultExtUnite = ".pdf";
        private string outputUnitePdf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "unite.pdf");

        private AppFacade(IUnitePdfFileServices unitePdfFileServices, IConvertPdfServices convertPdfServices, IGetPathFilesServices getPathFilesServices, ILogsServices logsServices, IMessageUser messageUser, ILogger logger)
        {
            _unitePdfFileServices = unitePdfFileServices;
            _converterPdf = convertPdfServices;
            _pathFilesServices = getPathFilesServices;
            _logsService = logsServices;
            _messageUser = messageUser;
            _logger = logger;
        }

        public static AppFacade GetInstance(IUnitePdfFileServices unitePdfFileServices, IConvertPdfServices convertPdfServices, IGetPathFilesServices getPathFilesServices, ILogsServices logsServices, IMessageUser messageUser, ILogger logger)
        {
            if (_instance is null)
            {
                _instance = new AppFacade(unitePdfFileServices, convertPdfServices, getPathFilesServices, logsServices, messageUser, logger);
            }

            return _instance;
        }

        public void GetPathForConverting()
        {
            try
            {
                pathFilesForConverting = _pathFilesServices.GetPathFiles(defaultExtConverting, filterFileConverting, initialDirectory);
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }

        public void ConvertPdf()
        {
            if (pathFilesForConverting is null || pathFilesForConverting.Count == 0)
            {
                _messageUser.Warning("Не загружено ни одного файла!");
                return;
            }

            var pathExcelFiles = pathFilesForConverting
                .Where(pathFile => (Path.GetExtension(pathFile) is ".xlsx") || (Path.GetExtension(pathFile) is ".xls"))
                .ToList();
            var pathWordFiles = pathFilesForConverting
                .Where(pathFile => (Path.GetExtension(pathFile) is ".docx") || (Path.GetExtension(pathFile) is ".doc"))
                .ToList();
            var pathPowerPointFiles = pathFilesForConverting
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
        public void GetPathForUnite()
        {
            try
            {
                var pathFiles = _pathFilesServices.GetPathFiles(defaultExtUnite, filterFileUnite, initialDirectory);

                if (pathFiles is not null)
                    pathFilesForUnite.AddRange(pathFiles);
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
        public void UnitePdf()
        {
            if (pathFilesForUnite.Count < 2)
            {
                _messageUser.Warning("Количество файлов для объединения должно быть не менее 2");
                return;
            }

            try
            {
                Task.Run(() => _unitePdfFileServices.UnitePdfFiles(pathFilesForUnite.OrderBy(p => p).ToList(), outputUnitePdf));
                _messageUser.Info("Файлы успешно объединены!");
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
    }
}
