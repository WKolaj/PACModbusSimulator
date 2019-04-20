using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;

namespace PACModbusSimulator
{
    public class DoubleToSingleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return System.Convert.ToSingle(value);
            }
            catch (Exception err)
            {
                return new ValidationResult(false, err);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return System.Convert.ToDouble(value);
            }
            catch (Exception err)
            {
                return new ValidationResult(false, err);
            }
        }
    }
}
