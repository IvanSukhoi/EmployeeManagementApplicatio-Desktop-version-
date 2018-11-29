using System.Windows;
using EmployeeManagement.UI.DI;
using EmployeeManagement.UI.DI.WindowFactory;
using Microsoft.HockeyApp;
using Microsoft.Practices.Unity;

namespace EmployeeManagement.UI.Windows
{
    public partial class App
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            HockeyClient.Current.Configure("6e556c8e5ac64fd4a7f9c56d52888a4d");
            await HockeyClient.Current.SendCrashesAsync(true);
            
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            await bootstrapper.Container.Resolve<WindowFactory>().Create<TrayWindow>().InitAsync();
        }
    }
}