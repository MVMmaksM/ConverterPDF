﻿using NLog;
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
        private IShowInfoUserServices _showInfoUserServices;
        private IShowAboutServices _showAboutServices;
        private static ILogger _logger;
        private static AppFacade _instance;
        private List<string> pathFilesForConverting = new List<string>();
        private List<string> pathFilesForUnite = new List<string>();
        private static string pathFolderLogs = $"{Environment.CurrentDirectory}\\logs";
        private static string pathCurrentLogFile = $"{pathFolderLogs}\\{DateTime.Now.ToString("yyyy-MM-dd")}.log";
        private string filterFileConverting = "Microsoft Excel|*.xlsx;*.xls|Microsoft Word|*.docx;*.doc|Power Point|*.pptx|Все файлы|*.xlsx;*.xls;*.docx;*.doc;*.pptx";
        private string filterFileUnite = "PDF|*.pdf";
        private string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private string defaultExtConverting = ".xlsx|.pptx|.docx";
        private string defaultExtUnite = ".pdf";
        private string outputUnitePdf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "unite.pdf");
        private string pathAboubtFile = Path.Combine(Environment.CurrentDirectory, "About", "About.txt");

        private AppFacade(IShowAboutServices showAboutServices, IUnitePdfFileServices unitePdfFileServices, IConvertPdfServices convertPdfServices, IGetPathFilesServices getPathFilesServices, ILogsServices logsServices, IMessageUser messageUser, ILogger logger, IShowInfoUserServices showInfoUserServices)
        {
            _unitePdfFileServices = unitePdfFileServices;
            _converterPdf = convertPdfServices;
            _pathFilesServices = getPathFilesServices;
            _logsService = logsServices;
            _messageUser = messageUser;
            _logger = logger;
            _showInfoUserServices = showInfoUserServices;
            _showAboutServices = showAboutServices;
        }

        public static AppFacade GetInstance(IShowAboutServices showAboutServices,IUnitePdfFileServices unitePdfFileServices, IConvertPdfServices convertPdfServices, IGetPathFilesServices getPathFilesServices, ILogsServices logsServices, IMessageUser messageUser, ILogger logger, IShowInfoUserServices showInfoUserServices)
        {
            if (_instance is null)
            {
                _instance = new AppFacade(showAboutServices, unitePdfFileServices, convertPdfServices, getPathFilesServices, logsServices, messageUser, logger, showInfoUserServices);
            }

            return _instance;
        }

        public void GetPathForConverting()
        {
            try
            {
                var pathFiles = _pathFilesServices.GetPathFiles(defaultExtConverting, filterFileConverting, initialDirectory);
                if (pathFiles is not null)
                {
                    pathFilesForConverting.AddRange(pathFiles);
                    _showInfoUserServices.ShowInfo("Добавлены файлы для конвертации:", pathFiles);
                }
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
        public async void ConvertPdf()
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

            _showInfoUserServices.ShowInfo("Выполенение конвертации\n");

            try
            {
                if (pathExcelFiles.Count > 0)
                {
                    _logger.Info("Конвертация Excel");
                    await Task.Run(() => _converterPdf.ConvertExcelToPdf(pathExcelFiles));
                }

                if (pathWordFiles.Count > 0)
                {
                    _logger.Info("Конвертация Word");
                    await Task.Run(() => _converterPdf.ConvertWordToPdf(pathWordFiles));
                }

                if (pathPowerPointFiles.Count > 0)
                {
                    _logger.Info("Конвертация Power Point");
                    await Task.Run(() => _converterPdf.ConvertPowerPointToPdf(pathPowerPointFiles));
                }

                _showInfoUserServices.ShowInfo("Конвертация выполнена!\n");
                _messageUser.Info("Конвертация успешно выполнена!");
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
                {
                    pathFilesForUnite.AddRange(pathFiles);
                    _showInfoUserServices.ShowInfo("Добавлены файлы для объединения:", pathFiles);
                }
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
        public async void UnitePdf()
        {
            if (pathFilesForUnite.Count < 2)
            {
                _messageUser.Warning("Количество файлов для объединения должно быть не менее 2");
                return;
            }

            _showInfoUserServices.ShowInfo("Выполнение объединения\n");

            try
            {
                await Task.Run(() => _unitePdfFileServices.UnitePdfFiles(pathFilesForUnite.OrderBy(p => p).ToList(), outputUnitePdf));
                _messageUser.Info("Файлы успешно объединены!");
                _showInfoUserServices.ShowInfo("Объединение выполнено!\n");

            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
        public void ShowAbout() => _showAboutServices.ShowAbout(pathAboubtFile);
    }
}
