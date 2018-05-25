using System.Windows;
using EmployeeManagement.UI.DI;
using Unity;

namespace EmployeeManagement.UI.Windows
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            RegistrationUnity.Setup().Resolve<TrayWindow>().Init();
        }
    }
}