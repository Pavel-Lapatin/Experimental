using System;
using NetMastery.Lab05.FileManager.CompositionRoot;
using NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.ViewModel;
using Autofac;

namespace NetMastery.Lab05.FileManager
{
   
    class Program
    {
        static void Main()
        {
            try
            { 
                var container = ContainerConfiguration.Config();
                AutoMapperInitializer.Initialize();
                DirectoryInitializer.SetCurrentDirectory();

                var arguments = "login -l admin -p admin";
                if (arguments == null) throw new NullReferenceException();
                var cmd = CommandLineInitializer.Initialize(new CommandLineApplication(), container);
                var args = ParseArguemts(arguments);
                cmd.Execute(args);

                while (true)
                {
                    try
                    {
                        var model = container.Resolve<AppViewModel>();
                        if(model.AuthenticatedLogin == null)
                        {
                            Console.Clear();
                            Console.WriteLine("Please signin in the system");
                        }

                        cmd = CommandLineInitializer.Initialize(new CommandLineApplication(), container);
                        Console.Write(container.Resolve<AppViewModel>().QueryLineView);
                        arguments = Console.ReadLine().Trim();
                        if (arguments == null) throw new NullReferenceException();
                        args = ParseArguemts(arguments);
                        cmd.Execute(args);
                        continue;
                    }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch(CommandParsingException e)
                    {
                        e.Command.ShowHelp();
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    

                    Console.WriteLine();
                    Console.WriteLine("Please press any button for continue ...");
                    Console.ReadKey();
                }
            }
            catch(Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
        }

        private static string[] ParseArguemts(string arguments)
        {
            var separators = new char[] { ' ' };
            return arguments.Split(separators);

        }
    }
}
