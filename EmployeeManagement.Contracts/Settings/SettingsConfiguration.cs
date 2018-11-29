using System.Configuration;

namespace EmployeeManagement.Contracts.Settings
{
    public static class SettingsConfiguration
    {
        public static string BaseUrl => ConfigurationManager.AppSettings ["BaseURL"];

        public static class ApiUrls
        {
            public static string GetUserByRefreshToken = "account/refreshtoken";
            public static string GetUser => "account/";
            public static string GetEmployee => "employee/";
            public static string GetEmployeeByDepartmentId => "employee/department/";
            public static string GetDepartment => "department/";
            public static string GetSettings => "settings/";
            public static string GetToken => "jwt/signin";
            public static string GetRefreshToken => "jwt/token";
        }

        public static class RegistrySettings
        {
            public static string RefreshToken => "RefreshToken";
        }
    }
}
