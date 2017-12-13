using NetMastery.Lab05.FileManager.DAL.Entities;
using System;
using Microsoft.Extensions.CommandLineUtils;
using AutoMapper;
using NetMastery.FileManeger.Controller;
using NetMastery.Lab05.FileManager.BL;

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
            CommandLineApplication cmd = new CommandLineApplication();
            FileManagerModel model = new FileManagerModel();
            CommandLineOptions.AddCommands(cmd, model);

            while (true)
            {
                try
                {
                    Console.Write("command->");
                    var args = Console.ReadLine();
                    if (args == null) throw new NullReferenceException();
                    cmd.Execute(ParseArguemts(args.Trim(' ')));
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
                cfg.CreateMap<StorageDto, DirectoryInfo>();

            });
        }

        private static string[] ParseArguemts(string arguments)
        {
            var separators = new char[] { ' ' };
            return arguments.Split(separators);

        }


    }
}
