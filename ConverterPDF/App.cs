using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConverterPDF
{
    public partial class App : Application
    {
        readonly MainWindow mainWindow;

        // через систему внедрения зависимостей получаем объект главного окна
        public App(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            mainWindow.Show();  // отображаем главное окно на экране
            base.OnStartup(e);
            ShutdownMode = ShutdownMode.OnExplicitShutdown; // не закрывает главное окно при закрытии дочернего
        }
    }
}
