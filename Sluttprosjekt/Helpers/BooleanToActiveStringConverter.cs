using System;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using Sluttprosjekt.Design;

namespace Sluttprosjekt.Helpers
{
    /// <summary>
    /// Converts a boolean to Active string when true or empty string when false.
    /// </summary>
    public class BooleanToActiveStringConverter : IValueConverter
    {
		const string Active = "valgt";
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Active : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (string)value == Active;
        }
    }
}
