using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public class VersionAppServices : IVersionAppServices
    {
        public string GetVersionApp()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            return $"{assemblyName.Name} ver. {assemblyName?.Version?.Major}.{assemblyName?.Version?.Minor} build:{assemblyName?.Version?.Build}";
        }
    }
}
