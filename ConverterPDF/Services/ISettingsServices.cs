using ConverterPDF.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public interface ISettingsServices
    {
        void SaveSettings(SettingsModel settings);
        SettingsModel GetSettings();
        SettingsModel CreateDefaultSettings();
    }
}
