using System;
using NetMastery.Lab05.FileManager.CompositionRoot;
using NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.ViewModels;

namespace NetMastery.Lab05.FileManager
{
   
    class Program
    {
        static void Main()
        {
            try
            { 
                var container = ContainerConfiguration.Configurate();
                AutoMapperInitializer.Initialize();
                DirectoryInitializer.SetCurrentDirectory();
                var appViewModel = new AppViewModel();
                var cmd = new CommandLineApplication();
                while (true)
                {
                    try
                    {
                        Console.Write(appViewModel.QueryLineView);
                        var arguments = Console.ReadLine();
                        if (arguments == null) throw new NullReferenceException();
                        var args = ParseArguemts(arguments);
                        cmd.Execute(args);
                    }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch(Exception) { }
        }

        private static string[] ParseArguemts(string arguments)
        {
            var separators = new char[] { ' ' };
            return arguments.Split(separators);

        }
    }
}
