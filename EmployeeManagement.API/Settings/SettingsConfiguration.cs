using System.Configuration;

namespace EmployeeManagement.API.Settings
{
    static class SettingsConfiguration
    {
        public static string GetBaseUrl()
        {
            return ConfigurationManager.AppSettings ["BaseURL"];
        }
    }
}
