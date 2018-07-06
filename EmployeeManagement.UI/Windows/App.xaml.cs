using System.Threading.Tasks;
using System.Windows;
using EmployeeManagement.UI.DI;
using EmployeeManagement.UI.DI.WindowFactory;
using Unity;

namespace EmployeeManagement.UI.Windows
{
    public partial class App
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var trayWindow = RegistrationUnity.Setup().Resolve<WindowFactory>().Create<TrayWindow>();
            await trayWindow.Init();

            //Task.Run(async () => await trayWindow.Init());
            //var t = this.Dispatcher.InvokeAsync(async () => await ((TrayViewModel)DataContext).InitAsync());
        }
    }
}