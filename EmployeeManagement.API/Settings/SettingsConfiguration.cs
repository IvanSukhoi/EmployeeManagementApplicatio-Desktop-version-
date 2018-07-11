using System.Configuration;

namespace EmployeeManagement.API.Settings
{
    public static class SettingsConfiguration
    {
        public static string BaseUrl => ConfigurationManager.AppSettings ["BaseURL"];

        public static class ApiUrls
        {
            public static class Employee
            {
                public static string GetByDepartmentId => "Employee/GetByDepartmentId/";
                public static string GetbyId => "Employee/GetById/";
                public static string Create => "Employee/Create";
                public static string Save => "Employee/Update";
                public static string Delete => "Employee/Delete/";
            }

            public static class Department
            {
                public static string GetAll => "Department/GetAll";
                public static string GetById => "Department/GetById/";
            }
        }
    }
}
