using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot
{
    public static class ContainerConfiguration
    {
        public static IContainer Configurate(ContainerBuilder builder)
        {
            builder.RegisterType<UserInfoConsoleWriter>().As<IUserInfoWriter>();
            
            return builder.Build();
        }
    }
}
