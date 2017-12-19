using Autofac;
using NetMastery.FileManeger.Bl.Interfaces;
using NetMastery.Lab05.FileManager.BlExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.Infrastructure.IoC_Container
{
    public  class AutofucContainer
    {
        public AutofucContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserInfoConsoleWriter>().As<IUserInfoWriter>();
            builder.RegisterType<>().As<>();
            builder.RegisterType<>().As<>();
            builder.RegisterType<>().As<>();
            builder.RegisterType<>().As<>();
            builder.RegisterType<>().As<>();

        }
    }
}
