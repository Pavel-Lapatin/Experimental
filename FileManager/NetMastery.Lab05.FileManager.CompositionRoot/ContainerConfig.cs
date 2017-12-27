using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Bl.Servicies;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.UI;
using NetMastery.Lab05.FileManager.UI.Commands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Implementation;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System.Linq;
using System.Reflection;

namespace NetMastery.Lab05.FileManager.CompositionRoot
{
    public static class ContainerConfiguration
    {
        public static IContainer Config()
        {
            var builder = new ContainerBuilder();

            var uiAssembly = Assembly.GetAssembly(typeof(Controller));
            var compositionBaseAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterType<UserContext>().As<IUserContext>()
                .SingleInstance();

            var userContext = new UserContext();
            builder.RegisterInstance(userContext);

            builder.RegisterAssemblyTypes(uiAssembly)
                .Where(t => t.IsSubclassOf(typeof(Controller)) 
                || t.IsSubclassOf(typeof(ViewModel))
                || t.IsSubclassOf(typeof(CommandLineApplication)));

            builder.RegisterAssemblyTypes(compositionBaseAssembly)
            .Where(t => t.IsSubclassOf(typeof(CommandLineApplication)));

            builder.RegisterType<AuthenticationService>()
                   .As<IAuthenticationService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<FileService>()
                   .As<IFileService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UserService>()
                   .As<IUserService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<DirectoryService>()
                   .As<IDirectoryService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                   .As<IUnitOfWork>()
                   .InstancePerLifetimeScope()
                   .WithParameter(new TypedParameter(
                       typeof(FileManagerDbContext),
                       new FileManagerDbContext()));


            builder.Register(c => new FileCommand(
                c.Resolve<AddDirectoryCommand>(),
                c.Resolve<ChangeWorkDirectoryCommand>(),
                c.Resolve<InfoDirectoryCommand>(),
                c.Resolve<MoveDirectoryCommand>(),
                c.Resolve<RemoveDirectoryCommand>(),
                c.Resolve<SearchDirectoryCommand>(),
                c.Resolve<ListDirectoryCommand>()));



            builder.Register(c => new DirectoryCommand(
                c.Resolve<AddDirectoryCommand>(),
                c.Resolve<ChangeWorkDirectoryCommand>(),
                c.Resolve<InfoDirectoryCommand>(),
                c.Resolve<MoveDirectoryCommand>(),
                c.Resolve<RemoveDirectoryCommand>(),
                c.Resolve<SearchDirectoryCommand>(),
                c.Resolve<ListDirectoryCommand>()));


            builder.Register(c => new CommandLineRoot(
                c.Resolve<LoginCommand>(),
                c.Resolve<FileCommand>(),
                c.Resolve<DirectoryCommand>(),
                c.Resolve<ExitCommand>(),
                c.Resolve<LogoffCommand>(),
                c.Resolve<UserCommand>()));

            return builder.Build();
        }
    }
}