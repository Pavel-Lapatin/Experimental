using AutoMapper;
using Microsoft.Owin;
using NetMastery.InventoryManager.Bl.Servicies.Implementations;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using NetMastery.InventoryManager.DAL.UnitOfWork;
using Ninject;
using Ninject.Web.Common;
using System.Reflection;
using System.Web;
using System.Linq;
using System;

namespace NetMastery.InventoryManager
{
    public class NinjectConfig
    {
        public static void RegisterNinject(IKernel kernel)
        {
            kernel.Bind<IBusinessService>().To<BusinessService>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            //kernel.Bind<IMapper>().To<RoleService>();
            kernel.Bind<IOwinContext>().ToMethod(ctx => HttpContext.Current.GetOwinContext()).InRequestScope();

            //automapper config
            var profiles = typeof(NinjectConfig).Assembly.GetTypes().Where(x=> typeof(Profile).IsAssignableFrom(x)).ToList();
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
                }
            }));
            kernel.Bind<IMapper>().ToConstant(mapper).InSingletonScope();


        }
    }
}