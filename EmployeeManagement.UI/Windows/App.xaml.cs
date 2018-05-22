using System.Windows;
using EmployeeManagement.DataEF.DAL;
using EmployeeManagement.Domain.Services;
using EmployeeManagement.UI.ViewModels;

namespace EmployeeManagement.UI.Windows
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var userService = new UserService(new ManagementContext());
            var authorizationService = new AuthorizationService(userService);
            var trayWindow = new TrayWindow();
            var trayViewModel = new TrayViewModel(trayWindow, authorizationService);
            trayWindow.DataContext = trayViewModel;
            trayViewModel.Init();
        }
    }
}