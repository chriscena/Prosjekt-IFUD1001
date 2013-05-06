using System;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using Sluttprosjekt.Design;

namespace Sluttprosjekt.Helpers
{
    /// <summary>
    /// Converts a boolean value to highlight background color when true or default background when false.
    /// </summary>
    public class BooleanToStyleConverter : IValueConverter
    {
		
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? ((Color)Application.Current.Resources["HighlightBackground"]).ToString() : ((Color)Application.Current.Resources["LowlightBackground"]).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (string)value == (string)Application.Current.Resources["HighlightBackground"];
        }
    }
}
