using System;
using System.Configuration;
using System.Reflection;
using System.IO;
using NetMastery.Lab05.FileManager.BL;
using Autofac;
using NetMastery.Lab05.FileManager.Composition_root;
using NetMastery.Lab05.FileManager.CompositionRoot;
using NetMastery.Lab05.FileManager.CompositionRoot.AutoMapping;
using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.Lab05.FileManager
{
   
    class Program
    {
        static void Main()
        {
            try
            {
                
                var container = ContainerConfiguration.Configurate(new ContainerBuilder());
                AutoMapperInitializer.Initialize();
                var cmdViewModel = DirectoryInitializer.SetCurrentDirectory();
                var cmd = new CommandLineApplication();
                CommandLineOptions.AddCommands(cmd);
                while (true)
                {
                    try
                    {
                        
                        Console.Write(model.currentFullPath + "->");
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
            catch(Exception e) { }
        }


        public static void InitialiseMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AccountDto, Account>();
                cfg.CreateMap<DirectoryStructureDto, DirectoryStructure>();
                cfg.CreateMap<FileStructureDto, FileStructure>();
            });
        }

        private static string[] ParseArguemts(string arguments)
        {
            var separators = new char[] { ' ' };
            return arguments.Split(separators);

        }

        


    }
}
