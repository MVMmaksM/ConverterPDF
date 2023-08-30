using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Settings
{
    public class VisibleFileConverting
    {
		private static Dictionary<string, bool> _isVisible = new Dictionary<string, bool> 
		{
			{ "Да", true},
			{ "Нет", false}
		};
		public static Dictionary<string, bool> IsVisible {get => _isVisible; }	
	}
}
