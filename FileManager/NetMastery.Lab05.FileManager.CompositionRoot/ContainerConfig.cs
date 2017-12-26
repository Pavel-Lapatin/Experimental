using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Bl.Servicies;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.RootCommandChildren;
using NetMastery.Lab05.FileManager.CompositionRoot.Database;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.Migrations;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.Helpers;
using NetMastery.Lab05.FileManager.UI;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Implementation;
using NetMastery.Lab05.FileManager.UI.ViewModel;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System.Data.Entity;
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

            
           // var dbcontextType = 
            
            //var directoryCommandTypes = compositionBaseAssembly.GetTypes()
            //    .Where(t => t.Name.Contains("DirectoryChild") 
            //    && t.IsSubclassOf(typeof(CommandLineApplication)));

            //var fileCommandTypes = compositionBaseAssembly.GetTypes()
            //    .Where(t => t.Namespace.Contains("FileCommand") 
            //    && t.IsSubclassOf(typeof(CommandLineApplication)));

            //var rootCommandTypes = compositionBaseAssembly.GetTypes()
            //.Where(t => t.Name.Contains("RootChild")
            //    && t.IsSubclassOf(typeof(CommandLineApplication)));








            //builder.RegisterType<CommandLineRoot>().
            //    UsingConstructor(rootCommandTypes.ToArray());



            builder.RegisterAssemblyTypes(uiAssembly)
                .Where(t => t.IsSubclassOf(typeof(Controller)));

            builder.RegisterAssemblyTypes(compositionBaseAssembly)
            .Where(t => t.IsSubclassOf(typeof(CommandLineApplication)));

            builder.RegisterType<UserContext>().As<IUserContext>()
                .SingleInstance();

            var userContext = new UserContext();
            builder.RegisterInstance(userContext);

            //builder.RegisterType<CommandLineRoot>()
            //    .WithParameter(new TypedParameter(
            //           typeof(CommandLineApplication),
            //           new LoginCommand()));
            ;


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


            builder.RegisterGeneric(typeof(ConsoleWriter<>))
                   .As(typeof(IInfoWriter<>))
                   .InstancePerLifetimeScope();


            builder.Register(c => new FileRootChildCommand(
                c.Resolve<AddDirectoryChildCommand>(),
                c.Resolve<ChangeWorkDirectoryChildCommand>(),
                c.Resolve<InfoDirectoryChildCommand>(),
                c.Resolve<MoveDirectoryChildCommand>(),
                c.Resolve<RemoveDirectoryChildCommand>(),
                c.Resolve<SearchDirectoryChildCommand>(),
                c.Resolve<ListDirectoryChildCommand>()));



            builder.Register(c => new DirectoryRootChildCommand(
                c.Resolve<AddDirectoryChildCommand>(),
                c.Resolve<ChangeWorkDirectoryChildCommand>(),
                c.Resolve<InfoDirectoryChildCommand>(),
                c.Resolve<MoveDirectoryChildCommand>(),
                c.Resolve<RemoveDirectoryChildCommand>(),
                c.Resolve<SearchDirectoryChildCommand>(),
                c.Resolve<ListDirectoryChildCommand>()));


            builder.Register(c => new CommandLineRoot(
            c.Resolve<LoginRootChildCommand>(),
            c.Resolve<FileRootChildCommand>(),
            c.Resolve<DirectoryRootChildCommand>(),
            c.Resolve<ExitRootChildCommand>(),
            c.Resolve<LogoffRootChildCommand>(),
            c.Resolve<UserRootChildCommand>()));

            return builder.Build();
        }
    }
}