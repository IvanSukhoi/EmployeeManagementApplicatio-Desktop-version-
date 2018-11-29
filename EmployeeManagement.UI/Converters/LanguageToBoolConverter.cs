using System;
using System.Globalization;
using System.Windows.Data;
using EmployeeManagement.Contracts.Enums;

namespace EmployeeManagement.UI.Converters
{
    public class LanguageToBoolConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var language = (Language)value;
                if (parameter != null && language == (Language)parameter)
                    return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
