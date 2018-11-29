using System;
using System.Linq;
using System.Windows;
using EmployeeManagement.Contracts.Enums;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.UI.UiInterfaces;

namespace EmployeeManagement.UI.Helpers
{
    public class SettingsHelper : ISettingsHelper
    {
        public void SetTheme(SettingsModel settingsModel)
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

        public void SetLanguage(SettingsModel settings)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                System.Globalization.CultureInfo.GetCultureInfo(settings.Language == Language.Russian ? "ru-RU" : "en-US");

            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Threading.Thread.CurrentThread.CurrentUICulture;
        }
    }
}
