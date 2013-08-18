using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AppsWatcher.Common.Core;
using Autofac.Integration.WebApi;

namespace AppsWatcher.Api
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Components injection
            ComponentsContainer.Instance.RegisterApiControllers(Assembly.GetExecutingAssembly());
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(ComponentsContainer.Instance.GetComponentsContainer());

            new AutoProxy.ProxyGenerator().ResolveProxies();
        }
    }
}