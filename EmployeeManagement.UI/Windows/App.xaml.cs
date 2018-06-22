using System.Windows;
using EmployeeManagement.UI.DI;
using EmployeeManagement.UI.DI.WindowFactory;
using Unity;

namespace EmployeeManagement.UI.Windows
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            RegistrationUnity.Setup().Resolve<WindowFactory>().Create<TrayWindow>().Init();
        }
    }
}