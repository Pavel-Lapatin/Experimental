using Ninject.Web.Common.WebHost;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using System;
using NetMastery.InventoryManager.DAL;
using NetMastery.InventoryManager.App_Start;
using System.Web.Optimization;

namespace NetMastery.InventoryManager
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            new InventoryDbContext().Database.Initialize(true);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            NinjectConfig.RegisterNinject(kernel);
            return kernel;
        }
    }
}
