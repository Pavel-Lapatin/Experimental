using Autofac;
using NetMastery.Lab05.FileManager.UI;
using NetMastery.Lab05.FileManager.UI.Commands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using Serilog;
using System;
using System.IO;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using NetMastery.Lab05.FileManager.DAL;

namespace NetMastery.Lab05.FileManager.Composition
{
    public static class CompositionRoot
    {
        public static Engine Initialize()
        {
            Logger.LoggerConfiguration();
            Log.Logger.Information("Logger initialized successfully");
            Log.Logger.Information("Data Base set initializer initializing ...");
            new FileManagerDbContext().Database.Initialize(true);
            Log.Logger.Information("Successfully");
            Log.Logger.Information("IoC initializing ...");
            var container = ContainerConfiguration.Configurate();
            Log.Logger.Information("Successfully");
            Log.Logger.Information("Set current directory ...");
            DirectoryInitializer.SetCurrentDirectory();
            Log.Logger.Information($"Successfully. Current directory {Directory.GetCurrentDirectory()}");
            Log.Logger.Information("UI Engine initializing ...");
            return container.Resolve<Engine>();
        }
    }
}
