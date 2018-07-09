using System;
using System.Globalization;
using System.Windows.Data;
using EmployeeManagement.Contracts.Enums;

namespace EmployeeManagement.UI.Converters
{
    public class DepartmentBoolToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var department = (Departments)value;
                if (parameter != null && department == (Departments)parameter)
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
