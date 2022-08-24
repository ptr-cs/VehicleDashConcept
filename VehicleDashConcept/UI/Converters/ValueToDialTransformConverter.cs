using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VehicleDashConcept.UI.Converters
{
    /// <summary>
    /// Converts the raw fuel level value to the corresponding dial rotation value.
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public class FuelDialConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            // Assume the range of raw fuel values is 0.0 to 10.0,
            // and that the dial needle rotation range is 90° to 270° (180° range):
            double baseRotation = 90.0;
            return (baseRotation + Math.Round(val * 18.0, 2));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts the raw speed level value to the corresponding dial rotation value.
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public class SpeedDialConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            // Assume the range of raw speed values is 0.0 to 120.0,
            // and that the dial needle rotation range is 45° to 315° (270° range):
            double baseRotation = 45.0;
            return (baseRotation + Math.Round(val * 2.25, 2));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts the raw rev level value to the corresponding dial rotation value.
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public class RevDialConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            // Assume the range of raw revolutions values is 0.0 to 6000.0,
            // and that the dial needle rotation range is 45° to 315° (270° range):
            double baseRotation = 45.0;
            return (baseRotation + Math.Round(val * 0.045, 3));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
