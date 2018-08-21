using System.Configuration;

namespace EmployeeManagement.API.Settings
{
    public static class SettingsConfiguration
    {
        public static string BaseUrl => ConfigurationManager.AppSettings ["BaseURL"];

        public static class ApiUrls
        {
            public static string GetEmployeeUrl => "employee/";
            public static string GetEmployeeByDepartmentIdUrl => "employee/department/";
            public static string GetDepartmentUrl => "department/";
            public static string GetSettingsUrl => "settings/";
            public static string GetUserUrl => "account/";
            public static string GetUserByRefreshtokenUrl = "account/refreshtoken";
            public static string GetTokenUrl => "jwt/signin";
            public static string GetRefreshTokenUrl => "jwt/token";
        }
    }
}
