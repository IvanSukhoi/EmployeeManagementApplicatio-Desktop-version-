using EmployeeManagement.API.ApiInterfaces;
using Microsoft.Win32;

namespace EmployeeManagement.API.Managers
{
    public class RegistryManager : IRegistryManager
    {
        public void SetData(string name, string value)
        {
            using (var userKey = Registry.CurrentUser.OpenSubKey("Software", true)?.CreateSubKey("ApplicationUsers"))
            {
                userKey?.SetValue(name, value);
            }
        }

        public string GetData(string name)
        {
            using (var userKey = Registry.CurrentUser.OpenSubKey("Software", true)?.CreateSubKey("ApplicationUsers"))
            {
                var value = userKey?.GetValue(name);

                return value as string;
            }
        }

        public void RemoveData(string name)
        {
            using (var userKey = Registry.CurrentUser.OpenSubKey("Software", true)?.CreateSubKey("ApplicationUsers"))
            {
                userKey?.DeleteValue(name);
            }
        }
    }
}