using NetMastery.FileManeger.Controller;
using NetMastery.Lab05.FileManager.DAL.Entities;
using NetMastery.Lab05.FileManager.BL;
using System;
using Microsoft.Extensions.CommandLineUtils;
using AutoMapper;
using NetMastery.Lab05.FileManager.DAL.Repository;
using System.Resources;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DTO;

namespace NetMastery.FileManeger.ConsoleApp
{
   
    class Program
    {
        internal static AccountController AccountController;
        internal static StorageController StorageController;
        static Program()
        {
            InitializeControllers();

        }

        static void Main()
        {
            using(var UnitOfWork = new UnitOfWork(new FileManagerDBContext()))
            {
                AccountController = new AccountController(UnitOfWork.Accounts);
                StorageController = new StorageController(UnitOfWork.Storagies);


                while (true)
                {
                    Console.Write("command->");
                    var args = Console.ReadLine();
                    if (args == null) throw new NullReferenceException();
                    CommandLineApplication cmd = new CommandLineApplication();
                    CommandLineOptions.AddCommands(cmd);
                    cmd.Execute(ParseArguemts(args.Trim(' ')));
                }

            }        
        }

        public static void InitialiseMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AccountDto, Account>();
                cfg.CreateMap<StorageDto, Storage>();

            });
        }

        private static void InitializeControllers()
        {
            
        }

        private static string[] ParseArguemts(string arguments)
        {
            var separators = new char[] { ' ' };
            return arguments.Split(separators);

        }

        private ResourceManager  InitializeResourceManager(string language)
        {
            ResourceManager rm = new ResourceManager();
            var separators = new char[] { ' ' };
            return arguments.Split(separators);

        }
    }
}
