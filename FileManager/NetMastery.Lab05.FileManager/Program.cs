using System;
using NetMastery.Lab05.FileManager.CompositionRoot;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.ViewModel;
using System.Collections.Generic;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager
{
   
    class Program
    {
        static void Main()
        {
            try
            {
                CompositionRoot.CompRoot.Initialize();
                while (true)
                {
                    try
                    {
                        
                        var arguments = Console.ReadLine();
                        if (arguments == null) throw new NullReferenceException();
                        var args = ParseArguemts(arguments);
                        CompositionRoot.CompRoot.cmd.Execute(args);
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
            catch(Exception e)
            {
                
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
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
