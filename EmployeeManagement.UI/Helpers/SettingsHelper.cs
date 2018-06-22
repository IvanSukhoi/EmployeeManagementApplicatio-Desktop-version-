using System;
using System.Linq;
using System.Windows;
using EmployeeManagement.DataEF.Enums;

namespace EmployeeManagement.UI.Helpers
{
    public static class SettingsHelper
    {
        public static void SetTheme(DataEF.Entities.Settings settings)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri($"../Settings/Themes/{settings.Topic}.xaml", UriKind.Relative);

            ResourceDictionary oldDictionary = Application.Current.Resources.MergedDictionaries.First(x =>
                x.Source != null && x.Source.OriginalString.StartsWith("../Settings/Themes/"));

            Application.Current.Resources.MergedDictionaries.Remove(oldDictionary);
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }

        public static void SetLanguage(DataEF.Entities.Settings settings)
        {
            //ResourceDictionary dictionary = new ResourceDictionary();

            //switch (settings.Language)
            //{
            //    case Language.Russian:
            //        dictionary.Source = new Uri("../Settings/Localization/lang.ru-RU.xaml", UriKind.Relative);
            //        break;
            //    default:
            //        dictionary.Source = new Uri("../Settings/Localization/lang.xaml", UriKind.Relative);
            //        break;
            //}

            //ResourceDictionary oldDictionary = Application.Current.Resources.MergedDictionaries.First(x =>
            //    x.Source != null && x.Source.OriginalString.StartsWith("../Settings/Localization/lang."));

            //Application.Current.Resources.MergedDictionaries.Remove(oldDictionary);
            //Application.Current.Resources.MergedDictionaries.Add(dictionary);

            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(settings.Language == Language.Russian ? "ru-RU" : "en-US");
        }
    }
}
