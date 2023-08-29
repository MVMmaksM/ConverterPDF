using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public interface IShowInfoUserServices
    {
        event Action<string> AppFacadeNotify;
        void ShowInfo(string messageInfo, List<string> eventArgs);
        void ShowInfo(string messageInfo);
    }
}
