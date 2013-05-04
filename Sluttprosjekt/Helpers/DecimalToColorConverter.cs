using System;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace Sluttprosjekt.Helpers
{
    public class DecimalToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((decimal)value == 0) ? "#FF000000" : ((decimal)value < 0) ? "#FF5CE41D" : "#FFF53030";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
