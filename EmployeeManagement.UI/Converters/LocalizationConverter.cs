using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using EmployeeManagement.Contacts.Enums;
using EmployeeManagement.UI.Settings.Localization;

namespace EmployeeManagement.UI.Converters
{
    public class LocalizationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.GetType() == typeof(List<Sex>))
            {
                 var sexTypes = ((value as List<Sex>) ?? throw new InvalidOperationException())
                    .Select(x => Resource.ResourceManager.GetString(x.ToString())).ToList();

                return sexTypes;
            }

            if (value != null && value.GetType() == typeof(List<Profession>))
            {
                var professionTypes = ((value as List<Profession>) ?? throw new InvalidOperationException())
                    .Select(x => Resource.ResourceManager.GetString(x.ToString())).ToList();

                return professionTypes;
            }

            if (value != null && value.GetType() == typeof(Profession))
            {
                return Resource.ResourceManager.GetString(((Profession) value).ToString());
            }

            if (value != null && value.GetType() == typeof(Sex))
            {
                return Resource.ResourceManager.GetString(((Sex)value).ToString());
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("EmployeeManagement.UI.Settings.Localization.Resource", GetType().Assembly);

            var key = resourceManager.GetResourceSet(System.Threading.Thread.CurrentThread.CurrentUICulture, true, true)
                .OfType<DictionaryEntry>()
                .FirstOrDefault(x => x.Value.ToString() == (string) value).Key.ToString();

            if (Enum.IsDefined(typeof(Sex), key))
            {
                return (Sex)Enum.Parse(typeof(Sex), key);
            }

            if (Enum.IsDefined(typeof(Profession), key))
            {
                return (Profession) Enum.Parse(typeof(Profession), key);
            }

            throw new NotImplementedException();
        }
    }
}
