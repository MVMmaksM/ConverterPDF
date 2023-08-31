using ConverterPDF.Settings;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public class AppFacade : IAppFacade
    {
        private IConvertPdfServices _converterPdf;
        private IGetPathFilesServices _pathFilesServices;
        private ILogsServices _logsService;
        private IMessageUser _messageUser;
        private IUnitePdfFileServices _unitePdfFileServices;
        private IShowInfoUserServices _showInfoUserServices;
        private IShowAboutServices _showAboutServices;
        private static ILoggerServices _logger;
        private static ISettingsServices _settingsServices;
        private static IVersionAppServices _versionAppServices;
        private static SettingsModel _settings;
        private List<string> pathFilesForConverting = new List<string>();
        private List<string> pathFilesForUnite = new List<string>();
        private string filterFileConverting = "Microsoft Excel|*.xlsx;*.xls|Microsoft Word|*.docx;*.doc|Power Point|*.pptx|Все файлы|*.xlsx;*.xls;*.docx;*.doc;*.pptx";
        private string filterFileUnite = "PDF|*.pdf";
        private string defaultExtConverting = ".xlsx|.pptx|.docx";
        private string defaultExtUnite = ".pdf";

        public AppFacade(IVersionAppServices versionAppServices, ISettingsServices settingsServices, IShowAboutServices showAboutServices, IUnitePdfFileServices unitePdfFileServices, IConvertPdfServices convertPdfServices, IGetPathFilesServices getPathFilesServices, ILogsServices logsServices, IMessageUser messageUser, ILoggerServices logger, IShowInfoUserServices showInfoUserServices)
        {
            _unitePdfFileServices = unitePdfFileServices;
            _converterPdf = convertPdfServices;
            _pathFilesServices = getPathFilesServices;
            _logsService = logsServices;
            _messageUser = messageUser;
            _logger = logger;
            _showInfoUserServices = showInfoUserServices;
            _showAboutServices = showAboutServices;
            _settingsServices = settingsServices;
            _versionAppServices = versionAppServices;

            try
            {
                _logger.Info("Получение настроек");
                _settings = _settingsServices.GetSettings();
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
        public void GetPathForConverting()
        {
            try
            {
                var pathFiles = _pathFilesServices.GetPathFiles(defaultExtConverting, filterFileConverting, _settings.SelectedPathFolderOpenFile.Value);
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
                _messageUser.Warning("Не выбрано ни одного файла!");
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
                    await Task.Run(() => _converterPdf.ConvertExcelToPdf(pathExcelFiles, _settings.SelectedIsVisibleExcel.Value));
                }

                if (pathWordFiles.Count > 0)
                {
                    _logger.Info("Конвертация Word");
                    await Task.Run(() => _converterPdf.ConvertWordToPdf(pathWordFiles, _settings.SelectedIsVisibleWord.Value));
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
        public void OpenCurrentLogFile()
        {
            var fullNameCurrentLog = Path.Combine(_settings.PathFolderLogs, $"{DateTime.Now.ToString("yyyy-MM-dd")}.log");
            _logsService.OpenCurrentLogFile(fullNameCurrentLog);
        }
        public void OpenFolderLogs() => _logsService.OpenFolderLogs(_settings.PathFolderLogs);
        public void DeleteAllLogFiles() => _logsService.DeleteAllLogFiles(_settings.PathFolderLogs);
        public void GetPathForUnite()
        {
            try
            {
                var pathFiles = _pathFilesServices.GetPathFiles(defaultExtUnite, filterFileUnite, _settings.SelectedPathFolderOpenFile.Value);

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
                var fullNameOutputPdf = Path.Combine(_settings.SelectedPathSavePdf.Value, $"{_settings.NameUnitePdf}.pdf");

                await Task.Run(() => _unitePdfFileServices.UnitePdfFiles(pathFilesForUnite.OrderBy(p => p).ToList(), fullNameOutputPdf));
                _messageUser.Info("Файлы успешно объединены!");
                _showInfoUserServices.ShowInfo("Объединение выполнено!\n");

            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
        public void ShowAbout() => _showAboutServices.ShowAbout(_settings.PathAbout);
        public void ShowSettings(MainWindow mainWindow)
        {
            try
            {
                var settingWindow = new SettingsWindow(_settings, _settingsServices, _messageUser, _logger);
                settingWindow.Owner = mainWindow;
                settingWindow.Show();
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
        public void ClearSelectFileConvert(MainWindow mainWindow)
        {
            try
            {
                mainWindow.Info.Clear();
                pathFilesForConverting.Clear();
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
        public void ClearSelectFileUnite(MainWindow mainWindow)
        {
            try
            {
                mainWindow.Info.Clear();
                pathFilesForUnite.Clear();
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
        public void ShowVersionApp(MainWindow mainWindow)
        {
            try
            {
                _logger.Info("Получение версии приложения");
                mainWindow.Title += _versionAppServices.GetVersionApp();
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
    }
}
