using System;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace Sluttprosjekt.Helpers
{
    /// <summary>
    /// Converts a decimal value to a string indicating wether or not the amount is payable.
    /// </summary>
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((decimal)value == 0) ? "" : ((decimal)value < 0) ? "til gode" : "å betale";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
