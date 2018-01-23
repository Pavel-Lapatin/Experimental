using Ninject.Web.Common.WebHost;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using System;

namespace NetMastery.InventoryManager
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            NinjectConfig.RegisterNinject(kernel);
            return kernel;
        }
    }
}
