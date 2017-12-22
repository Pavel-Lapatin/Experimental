using Autofac;
using NetMastery.FileManeger.Bl.Interfaces;
using NetMastery.FileManeger.Bl.Servicies;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.Helpers;


namespace NetMastery.Lab05.FileManager.CompositionRoot
{
    public static class ContainerConfiguration
    {
        public static IContainer Configurate()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AuthenticationService>()
                   .As<IAuthenticationService>()
                   .InstancePerLifetimeScope();


            builder.RegisterGeneric(typeof(ConsoleWriter<>))
                   .As(typeof(IInfoWriter<>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                   .As<IUnitOfWork>()
                   .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                   .As(typeof(IRepository<>))
                   .InstancePerLifetimeScope();


            return builder.Build();
        }
    }
}
