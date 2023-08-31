using ConverterPDF.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Office.Interop.Excel;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<IAppFacade, AppFacade>();
                    services.AddSingleton<IConvertPdfServices, ConvertPdfServices>();
                    services.AddSingleton<IGetPathFilesServices, GetPathFilesServices>();
                    services.AddSingleton<ILogsServices, LogsServices>();
                    services.AddSingleton<IMessageUser, MessageUser>();
                    services.AddSingleton<IShowAboutServices, ShowAboutServices>();
                    services.AddSingleton<IShowInfoUserServices, ShowInfoUserServices>();
                    services.AddSingleton<IUnitePdfFileServices, UnitePdfFilesServices>();
                    services.AddSingleton<ILoggerServices, LoggerServices>();
                    services.AddSingleton<ISettingsServices, SettingsServices>();
                    services.AddSingleton<IVersionAppServices, VersionAppServices>();
                    services.AddSingleton<IExistsFodersServices, ExistsFoldersServices>();
                }).Build();

            var app = host.Services.GetService<App>();
            app?.Run();
        }
    }
}
