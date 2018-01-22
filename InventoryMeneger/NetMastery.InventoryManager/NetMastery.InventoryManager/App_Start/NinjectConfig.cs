using NetMastery.InventoryManager.Utils;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMastery.InventoryManager
{
    public class NinjectConfig
    {
        public static void RegisterNinject()
        {
            var registrations = new Ninjectregistrations();
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}