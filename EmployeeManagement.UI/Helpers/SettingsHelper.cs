using System;
using System.Windows;
using EmployeeManagement.DataEF.Entities;

namespace EmployeeManagement.UI.Helpers
{
    public static class SettingsHelper
    {
        public static void SetTheme(Settings settings)
        {
            var uri = new Uri($"Settings/Themes/{settings.Topic}.xaml", UriKind.Relative);

            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}
