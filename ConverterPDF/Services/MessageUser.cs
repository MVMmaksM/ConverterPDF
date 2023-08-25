using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConverterPDF.Services
{
    public class MessageUser : IMessageUser
    {
        public void Error(string message) => MessageBox.Show($"{message}\nПодробная информация находится в log-файле", "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
        public void Info(string message) => MessageBox.Show(message, "Информация", MessageBoxButton.OKCancel, MessageBoxImage.Information);
        public bool Question(string message) => MessageBox.Show(message, "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        public void Warning(string message) => MessageBox.Show(message, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
    }
}
