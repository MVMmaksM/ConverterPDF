using ConverterPDF.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public class ExistsFoldersServices : IExistsFodersServices
    {
        private static ILoggerServices _logger;
        private static IShowInfoUserServices _showInfoUserServices;

        public ExistsFoldersServices(ILoggerServices logger, IShowInfoUserServices showInfoUserServices)
        {
            _logger = logger;
            _showInfoUserServices = showInfoUserServices;
        }
        public void ExistAndCreateFolders(SettingsModel settingsModel)
        {
            _logger.Info("Проверка существования директории");

            if (!Directory.Exists(settingsModel.PathFolderSaveConverting))
            {
                Directory.CreateDirectory(settingsModel.PathFolderSaveConverting);
                _logger.Info($"Создана директория {settingsModel.PathFolderSaveConverting}");
                _showInfoUserServices.ShowInfo($"Создана директория {settingsModel.PathFolderSaveConverting}\n");
            }
        }
    }
}
