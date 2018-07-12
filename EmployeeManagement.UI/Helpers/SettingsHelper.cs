using System;
using System.Linq;
using System.Windows;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.UI.Helpers
{
    public static class SettingsHelper
    {
        public static void SetTheme(SettingsModel settingsModel)
        {
            var dictionary = new ResourceDictionary
            {
                Source = new Uri($"../Settings/Themes/{settingsModel.Theme}.xaml", UriKind.Relative)
            };

            var oldDictionary = Application.Current.Resources.MergedDictionaries.First(x =>
                x.Source != null && x.Source.OriginalString.StartsWith("../Settings/Themes/"));

            Application.Current.Resources.MergedDictionaries.Remove(oldDictionary);
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }

        public static void SetLanguage(SettingsModel settings)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(settings.Language == Language.Russian ? "ru-RU" : "en-US");
        }
    }
}
