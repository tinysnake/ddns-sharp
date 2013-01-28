using DDnsSharp.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DDnsSharp.Monitor.Converters
{
    class ServiceStatusVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(parameter is String))
                throw new ArgumentException("parameter");
            if (!(value is ServiceStatus))
                throw new ArgumentException("value");
            var str = parameter as String;
            string[] ps;
            bool operatorAnd = true;
            if (str.Contains('|'))
            {
                ps = str.Split('|');
                operatorAnd = false;
            }
            else if (str.Contains('&'))
            {
                ps = str.Split('&');
                operatorAnd = true;
            }
            else
            {
                ps = new[] { str };
                operatorAnd = true;
            }
            var rs = new Visibility[ps.Length];
            var i = rs.Length;
            var type = typeof(ServiceStatus);
            while (i-- > 0)
            {
                ServiceStatus val = (ServiceStatus)value;
                ServiceStatus ss;
                if (Enum.TryParse<ServiceStatus>(ps[i], out ss))
                {
                    rs[i] = ss == val ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                    throw new ArgumentException("parameter");
            }
            if (operatorAnd)
                return rs.Where(_ => _ == Visibility.Visible).Count() == rs.Length ? Visibility.Visible : Visibility.Collapsed;
            else
                return rs.Where(_ => _ == Visibility.Visible).Count() > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
