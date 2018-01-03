using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping;
using NetMastery.Lab05.FileManager.UI;
using NetMastery.Lab05.FileManager.UI.Commands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using Serilog;
using System;
using System.IO;
using NetMastery.Lab05.FileManager.UI.events;

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
            var cmd = container.Resolve<CommandLineApplicationRoot>();
            Log.Logger.Information("Factory initializing ...");
            Func<Type, Controller> controllerFactory = t => container.Resolve(t) as Controller;
            Log.Logger.Information("Successfully");
            Log.Logger.Information("Redirecting initializing ...");
            var engine = new Engine(controllerFactory, (CommandLineApplicationRoot)cmd);
            var redirectEvent = container.Resolve<RedirectEvent>();
            redirectEvent.Redirected += engine.RedirecteRedirectEventHandler;
            Log.Logger.Information("Successfully");
            return engine;
        }
    }
}
