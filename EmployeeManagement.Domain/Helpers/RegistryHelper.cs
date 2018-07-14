using EmployeeManagement.Domain.DomainInterfaces;
using Microsoft.Win32;

namespace EmployeeManagement.Domain.Helpers
{
    public class RegistryHelper : IRegistryHelper
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

        public void RemoveData(string name, string value)
        {
            using (var userKey = Registry.CurrentUser.OpenSubKey("Software", true)?.CreateSubKey("ApplicationUsers"))
            {
                if (userKey != null)
                {
                    userKey.DeleteValue(name);
                    userKey.DeleteValue(value);
                }
            }
        }
    }
}