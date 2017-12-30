using Autofac;
using Microsoft.Extensions.CommandLineUtils;

using NetMastery.Lab05.FileManager.Bl.Servicies;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.UI;
using NetMastery.Lab05.FileManager.UI.Commands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Implementation;
using NetMastery.Lab05.FileManager.UI.Forms;
using System.Linq;
using System.Reflection;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.DAL.UnitOfWork.Factory;

namespace NetMastery.Lab05.FileManager.CompositionRoot
{
    public static class ContainerConfiguration
    {
        public static IContainer Config()
        {
            var builder = new ContainerBuilder();

            var uiAssembly = Assembly.GetAssembly(typeof(Controller));

            var compositionBaseAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterType<UserContext>()
                .As<IUserContext>()
                .SingleInstance();

            var rerdirectEvent = new RedirectEvent();

            builder.RegisterInstance(rerdirectEvent)
               .SingleInstance();

            builder.RegisterAssemblyTypes(uiAssembly)
                .Where(t=> t.IsSubclassOf(typeof(CommandLineApplication))
                || t.IsSubclassOf(typeof(Form)));


            builder.RegisterAssemblyTypes(uiAssembly)
                .Where(t => t.IsSubclassOf(typeof(Controller)))
                .WithParameter(new TypedParameter(typeof(RedirectEvent), rerdirectEvent));

            builder.Register(c=> new DBRepositoryFactory())
                              .As<IDBRepositoryFactory>();

            builder.RegisterType<FSRepositoryFactory>()
                   .As<IFSRepositoryFactory>();


            builder.RegisterType<AuthenticationService>()
                   .As<IAuthenticationService>();

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

            builder.Register(c => new DirectoryCommand(
                c.Resolve<AddDirectoryCommand>(),
                c.Resolve<ChangeWorkDirectoryCommand>(),
                c.Resolve<InfoDirectoryCommand>(),
                c.Resolve<MoveDirectoryCommand>(),
                c.Resolve<SearchDirectoryCommand>(),
                c.Resolve<ListDirectoryCommand>(),
                c.Resolve<RemoveDirectoryCommand>()));

            builder.Register(c => new UserCommand(
                c.Resolve<InfoUserCommand>()));


            builder.Register(c => new FileCommand(
                c.Resolve<DownloadFileCommand>(),
                c.Resolve<UploadFileCommand>(),
                c.Resolve<MoveFileCommand>(),
                c.Resolve<RemoveFileCommand>(),
                c.Resolve<InfoFileCommand>()));


            builder.Register(c => new CommandLineApplicationRoot(
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