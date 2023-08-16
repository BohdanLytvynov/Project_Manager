using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Project_Manager.Converters
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = String.Empty;

            if (value is DateTime)
            {
                str= value.ToString();
            }

            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;

            DateTime temp = new DateTime();

            if (String.IsNullOrWhiteSpace(str))
            {
                return temp;
            }

            return DateTime.Parse(str);            
        }
    }
}
