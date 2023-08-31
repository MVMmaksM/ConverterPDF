using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Settings
{
    public class LibraryforConverting
    {
        public static readonly List<string> libraries = new List<string>
        {
            "Microsoft Office Interop (рекомендуется если установлен Microsoft Office)",
            "iTextSharp (рекомендуется если не установлен Microsoft Office)"
        };
    }
}
