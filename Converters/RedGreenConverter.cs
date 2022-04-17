using System;
using System.Globalization;
using System.Windows.Media;

namespace TenderHackUI.Converters
{
    [System.Windows.Data.ValueConversion(typeof(bool), typeof(SolidColorBrush))]
    public class RedGreenConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? new SolidColorBrush(Color.FromRgb(51, 219, 88)):new SolidColorBrush(Color.FromRgb(250, 60, 93));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
