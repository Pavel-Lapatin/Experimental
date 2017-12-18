using NetMastery.Lab05.FileManager.DAL.Entities;
using System;
using Microsoft.Extensions.CommandLineUtils;
using AutoMapper;
using System.Configuration;
using System.Reflection;
using System.IO;
using NetMastery.Lab05.FileManager.BL;
using NetMastery.Lab05.FileManager.BL.Dto;

namespace NetMastery.Lab05.FileManager
{
   
    class Program
    {

        static Program()
        {
            InitialiseMapper();
        }


        static void Main()
        {
           // try
           // {
            SetCurrentDirectory();
            Console.WriteLine("WorkDirectory " + Directory.GetCurrentDirectory()); 
            FileManagerModel model = new FileManagerModel
            {
                currentFullPath = @"~\",
            };
            
            while (true)
            {
                try
                {
                    CommandLineApplication cmd = new CommandLineApplication();
                    CommandLineOptions.AddCommands(cmd, model);
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

        private static void SetCurrentDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var directoryPath = Path.GetDirectoryName(path);
            var workDirectory = Path.Combine(directoryPath, ConfigurationManager.AppSettings["StoragePath"]);
            if (!Directory.Exists(workDirectory))
            {
                Directory.CreateDirectory(workDirectory);
            }
            Directory.SetCurrentDirectory(workDirectory);
        }


    }
}
