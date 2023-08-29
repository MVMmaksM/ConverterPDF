using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public class ShowInfoUserServices : IShowInfoUserServices
    {
        public event Action<string> AppFacadeNotify;
        public void ShowInfo(string messageInfo, List<string> eventArgs)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine(messageInfo);

            foreach (var arg in eventArgs)
            {
                strBuilder.AppendLine(arg);
            }

            AppFacadeNotify?.Invoke(strBuilder.ToString());
        }

        public void ShowInfo(string messageInfo)
        {
            AppFacadeNotify?.Invoke(messageInfo);
        }
    }
}
