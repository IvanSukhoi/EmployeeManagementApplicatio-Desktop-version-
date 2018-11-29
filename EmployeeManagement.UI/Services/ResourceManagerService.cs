using EmployeeManagement.UI.Settings.Localization;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.Services;

namespace EmployeeManagement.UI.Services
{
    public class ResourceManagerService : IResourceManagerService
    {
        public string GetString(string local)
        {
            return Resource.ResourceManager.GetString(local);
        }
    }
}
