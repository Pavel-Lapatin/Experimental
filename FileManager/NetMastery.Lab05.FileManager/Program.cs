using System;
using NetMastery.Lab05.FileManager.CompositionRoot;
using NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.ViewModel;
using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using System.Collections.Generic;

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
                var cmd = new CommandLineRoot(container);
                var model = container.Resolve<AppViewModel>();

                while (true)
                {
                    try
                    {
                        if(model.AuthenticatedLogin == null)
                        {
                            Console.Clear();
                            Console.WriteLine("Please signin in the system");
                        }

                        Console.Write(model.QueryLineView);
                        var arguments = Console.ReadLine().Trim();
                        if (arguments == null) throw new NullReferenceException();
                        var args = ParseArguemts(arguments);
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

            var strs = arguments.Trim('\"').Split('\"');
            List<string> args = new List<string>();
            int i = 1;
            foreach (var item in strs)
            {
                if(i%2 != 0)
                {
                    args.AddRange(item.Trim().Split(' '));
                }
                else
                {
                    args.Add(item);
                }
                i++;
            }
            return args.ToArray();

        }
    }
}
