using Autofac;
using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Bl.Servicies;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.Helpers;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.ViewModel;

namespace NetMastery.Lab05.FileManager.CompositionRoot
{
    public static class ContainerConfiguration
    {
        public static IContainer Config()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LoginController>();
            builder.RegisterType<DirectoryController>();
            //builder.RegisterType<FileController>();
            builder.RegisterType<UserController>();

            var model = new AppViewModel();
            builder.RegisterInstance(model);

            builder.RegisterType<AuthenticationService>()
                   .As<IAuthenticationService>()
                   .InstancePerLifetimeScope()
                   .WithParameter(new TypedParameter(
                       typeof(AppViewModel),
                       model));

            //builder.RegisterType<FileService>()
            //       .As<IFileService>()
            //       .InstancePerLifetimeScope()
            //       .WithParameter(new TypedParameter(
            //           typeof(AppViewModel),
            //           model));

            builder.RegisterType<UserService>()
                   .As<IUserService>()
                   .InstancePerLifetimeScope()
                   .WithParameter(new TypedParameter(
                       typeof(AppViewModel),
                       model));

            builder.RegisterType<DirectoryService>()
                   .As<IDirectoryService>()
                   .InstancePerLifetimeScope()
                   .WithParameter(new TypedParameter(
                       typeof(AppViewModel),
                       model));

            builder.RegisterType<UnitOfWork>()
                   .As<IUnitOfWork>()
                   .InstancePerLifetimeScope()
                   .WithParameter(new TypedParameter(
                       typeof(FileManagerDbContext), 
                       new FileManagerDbContext()));


            builder.RegisterGeneric(typeof(ConsoleWriter<>))
                   .As(typeof(IInfoWriter<>))
                   .InstancePerLifetimeScope();



            //    builder.RegisterGeneric(typeof(Repository<>))
            //          .As(typeof(IRepository<>))
            //           .InstancePerLifetimeScope();
            return builder.Build();
        }
    }
}