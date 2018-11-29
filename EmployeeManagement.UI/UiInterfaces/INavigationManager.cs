using System.Threading.Tasks;
using System.Windows.Navigation;
using EmployeeManagement.Contracts.Enums;

namespace EmployeeManagement.UI.UiInterfaces
{
    public interface INavigationManager
    {
        void SetNavigationService(NavigationService navigationService);
        Task Navigate(Contracts.Enums.Pages pages, Departments departmentd);
    }
}
