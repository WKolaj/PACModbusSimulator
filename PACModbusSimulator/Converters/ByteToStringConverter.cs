using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace PACModbusSimulator
{
    class ByteToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return ((Byte)value).ToString(CultureInfo.InvariantCulture);
            }
            catch(Exception)
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var newText = ((String)value).Replace(",", ".");
                return Byte.Parse(newText, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
