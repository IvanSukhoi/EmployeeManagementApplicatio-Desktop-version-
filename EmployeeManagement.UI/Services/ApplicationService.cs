using System.Windows;
using EmployeeManagement.UI.UiInterfaces;

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
