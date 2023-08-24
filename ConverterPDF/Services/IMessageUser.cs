using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public interface IMessageUser
    {
        void Info(string message);
        void Error(string message);
        void Warning(string message);
        bool Question(string message);
    }
}
