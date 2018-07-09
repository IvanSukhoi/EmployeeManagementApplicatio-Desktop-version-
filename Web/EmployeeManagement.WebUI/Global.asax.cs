using System.Web.Http;
using System.Web.Http.Dispatcher;
using EmployeeManagement.WebUI.DI;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EmployeeManagement.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CastleWindsor.Setup();
            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(CastleWindsor.GetWindsorContainer()));
        }

        protected void Application_End()
        {
            CastleWindsor.Dispose();
        }
    }
}
