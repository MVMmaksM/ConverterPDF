using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Settings
{
    public class Themes
    {
        private static List<string> _themesList = new List<string>
        {
            "dark",
            "light",
            "blue",
            "red"
        };

        public static List<string> ThemesList
        {
            get => _themesList;
        }
    }
}
