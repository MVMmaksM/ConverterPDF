using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public class ShowAboutServices : IShowAboutServices
    {
        private static ILogger _logger;
        private static IMessageUser _messageUser;

        public ShowAboutServices(ILogger logger, IMessageUser messageUser)
        {
            _logger = logger;
            _messageUser = messageUser;
        }
        public void ShowAbout(string pathAbout)
        {
            _logger.Info("Откртыие файла about");

            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.UseShellExecute = true;
                processStartInfo.FileName = pathAbout;
                Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                _messageUser.Error(ex.Message);
                _logger.Error($"{ex.Message}\nтрассировка стека: {ex.StackTrace}");
            }
        }
    }
}
