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
            var authorizationService = new AuthorizationService();
            //var userModel = new AuthorizationViewModel(userService);
            //var window = new AuthorizationWindow(userModel);
            //window.Show();

            var trayWindow = new TrayWindow();
            trayWindow.DataContext = new TrayViewModel(trayWindow, userService, authorizationService);
            trayWindow.Show();
        }
    }
}