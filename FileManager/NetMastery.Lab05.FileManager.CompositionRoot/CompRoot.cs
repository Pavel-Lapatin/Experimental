using Autofac;
using NetMastery.Lab05.FileManager.UI;
using NetMastery.Lab05.FileManager.UI.Commands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using Serilog;
using System;
using System.IO;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.Interfaces;

namespace NetMastery.Lab05.FileManager.CompositionRoot
{
    public static class CompRoot
    {
        
        public static Engine Initialize()
        {
            Logger.LoggerConfig();
            Log.Logger.Information("Logger initialized successfully");
            Log.Logger.Information("IoC initializing ...");
            var container = ContainerConfiguration.Config();
            Log.Logger.Information("Successfully");
            Log.Logger.Information("Set current directory ...");
            DirectoryInitializer.SetCurrentDirectory();
            Log.Logger.Information($"Successfully. Current directory {Directory.GetCurrentDirectory()}");
            Log.Logger.Information("UI Engine initializing ...");
            var resultProvider = container.Resolve<IResultProvider>();
            resultProvider.Result = container
                                    .Resolve<Func<Type, string, object[], RedirectResult>>()
                                    .Invoke(typeof(StartController), nameof(StartController.Start), null);

            var engine = new Engine(resultProvider, 
                                    container.Resolve<IUserContext>(),
                                    container.Resolve<CommandLineApplicationRoot>());

            Log.Logger.Information("Successfully");
            return engine;
        }
    }
}
