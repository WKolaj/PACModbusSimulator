using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace PACModbusSimulator
{
    class UInt16ToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return ((UInt16)value).ToString(CultureInfo.InvariantCulture);
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
                Console.WriteLine(UInt16.Parse(newText, CultureInfo.InvariantCulture));
                return UInt16.Parse(newText, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
