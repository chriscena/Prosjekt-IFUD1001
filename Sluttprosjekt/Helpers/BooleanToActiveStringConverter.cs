using System;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using Sluttprosjekt.Design;

namespace Sluttprosjekt.Helpers
{
    public class BooleanToActiveStringConverter : IValueConverter
    {
		const string _active = "valgt";
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? _active : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (string)value == _active;
        }
    }
}
