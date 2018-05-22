using Microsoft.Win32;

namespace EmployeeManagement.Domain.Helpers
{
    public static class RegistryHelper
    {
        public static void SetData(string name, string value)
        {
            using (RegistryKey userKey = Registry.CurrentUser.OpenSubKey("Software", true)?.CreateSubKey("ApplicationUsers"))
            {
                userKey?.SetValue(name, value);
            }
        }

        public static string GetData(string name)
        {
            using (RegistryKey userKey = Registry.CurrentUser.OpenSubKey("Software", true)?.CreateSubKey("ApplicationUsers"))
            {
                var value = userKey?.GetValue(name);

                return value as string;
            }
        }

        public static void RemoveData(string name, string value)
        {
            using (RegistryKey userKey = Registry.CurrentUser.OpenSubKey("Software", true)?.CreateSubKey("ApplicationUsers"))
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
