using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.Bl.Servicies;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.UI;
using NetMastery.Lab05.FileManager.UI.Commands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Implementation;
using System.Linq;
using System.Reflection;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using AutoMapper;
using System;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.DAL.UnitOfWork.Factory;
using NetMastery.Lab05.FileManager.UI.Interfaces;

namespace NetMastery.Lab05.FileManager.Composition
{
    public static class ContainerConfiguration
    {
        public static IContainer Configurate()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UserContext>()
                .As<IUserContext>()
                .SingleInstance();

            builder.RegisterType<ResultProvider>()
                .As<IResultProvider>()
                .SingleInstance();

            var userInterfaceAssembly = Assembly.GetAssembly(typeof(Controller));
            builder.RegisterAssemblyTypes(userInterfaceAssembly)
                .Where(t => t.IsSubclassOf(typeof(CommandLineApplication))
                         || t.IsSubclassOf(typeof(ActionResult))
                         || t.IsSubclassOf(typeof(Controller)));

            var compositionRootAssembly = Assembly.GetExecutingAssembly();
            var profiles = compositionRootAssembly.GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Profile))).ToArray();

            builder.RegisterTypes(profiles);

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return new Mapper(
                    new MapperConfiguration(cfg =>
                    {
                        foreach (var profile in profiles)
                        {
                            cfg.AddProfile(c.Resolve(profile) as Profile);
                        }
                    }));
            }).As<IMapper>().SingleInstance();

            builder.RegisterType<RepositoryFactory>()
                   .As<IRepositoryFactory>()
                   .InstancePerLifetimeScope(); ;

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

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return new Func<Type, Controller>(type => context.Resolve(type) as Controller);       
            });
            builder.Register(c => new Engine(c.Resolve<IResultProvider>(), 
                                             c.Resolve<IUserContext>(), 
                                             c.Resolve<CommandLineApplicationRoot>(),   
                                             c.Resolve<Func<Type, Controller>>()));

            return builder.Build();
        }
    }
}