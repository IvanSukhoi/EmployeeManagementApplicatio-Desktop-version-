using System.Threading.Tasks;
using EmployeeManagement.Contracts.Enums;

namespace EmployeeManagement.UI.UiInterfaces
{
    public interface INavigationManager
    {
        //void SetNavigationService(NavigationService navigationService);
        Task Navigate(Contracts.Enums.Pages pages, Departments departmentd);
    }
}
