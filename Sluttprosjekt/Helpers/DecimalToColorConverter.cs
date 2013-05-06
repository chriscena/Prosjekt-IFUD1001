using System;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace Sluttprosjekt.Helpers
{
    /// <summary>
    /// Converts a decimal value to green when negative, red when positive or transparent when 0.
    /// </summary>
    public class DecimalToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((decimal)value == 0) ? "#00000000" : ((decimal)value < 0) ? "#FF5CE41D" : "#FFF53030";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
