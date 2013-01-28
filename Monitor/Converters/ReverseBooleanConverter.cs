using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DDnsSharp.Monitor.Converters
{
    public class ReverseBooleanConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((parameter as String) == "Visibility")
            {
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            }
            else
                return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((parameter as String) == "Visiblility")
            {
                return (Visibility)value == Visibility.Visible ? false : true;
            }
            else
                return !(bool)value;
        }
    }
}
