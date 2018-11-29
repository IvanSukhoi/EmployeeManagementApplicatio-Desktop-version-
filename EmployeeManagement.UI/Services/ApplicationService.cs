using System.Windows;
using EmployeeManagement.UI.UiInterfaces;
using EmployeeManagement.UI.UiInterfaces.Services;

namespace EmployeeManagement.UI.Services
{
    public class ApplicationService : IApplicationService
    {
        public void Shutdown()
        {
            Application.Current.Shutdown();
        }
    }
}
