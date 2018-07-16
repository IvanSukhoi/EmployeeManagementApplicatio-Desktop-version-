using System.Threading.Tasks;

namespace EmployeeManagement.UI.UiInterfaces.Services
{
    public interface IWindowService
    {
        Task CreateMainWindowAsync();
        void CreateAuthorizationWindow();
    }
}
