using EmployeeManagement.UI.Events;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;
using Prism.Unity;

namespace EmployeeManagement.UI.DI
{
    public class Bootstrapper: UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            RegistrationUnity.Setup(Container);

            Container.Resolve<IEventAggregator>().GetEvent<UpdateMainWindowEvent>().Subscribe((mainWindow) =>
                {
                    Shell = mainWindow;

                    var regionManager = Container.Resolve<IRegionManager>();
                    regionManager.Regions.Remove("ContentRegion");

                    RegionManager.SetRegionManager(Shell, regionManager);
                    RegionManager.UpdateRegions();
                });
        }
    }
}
