using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.RootCommandChildren;
using NetMastery.Lab05.FileManager.CompositionRoot.Database;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Migrations;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot
{
    public static class CompRoot
    {
        public static CommandLineApplication cmd;

        public static void Initialize()
        {
            var container = ContainerConfiguration.Config();
            AutoMapperInitializer.Initialize();
            DirectoryInitializer.SetCurrentDirectory();
            Logger.LoggerConfig();
            new DatabaseInitialiser<FileManagerDbContext, Configuration>().InitializeDatabase(new FileManagerDbContext());
            cmd = container.Resolve<CommandLineRoot>();
        }
    }
}
