using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VehicleDashConcept.UI.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class TextToSpacedTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueToConvert = (string)value;
            string result = "";
            for (int i = 0; i < valueToConvert.Length; ++i)
            {
                result += valueToConvert[i] + " "; // this is a 'hair space' character (Unicode: U+200A)
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
