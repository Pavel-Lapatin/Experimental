using NetMastery.Lab05.FileManager.DAL.Entities;
using System;
using Microsoft.Extensions.CommandLineUtils;
using AutoMapper;
using NetMastery.Lab05.FileManager.BL;
using NetMastery.Lab05.FileManeger.Bl.Dto;

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

            Console.Write("command->");
            Console.WriteLine(" login -l admin -p admin");
            //var args = Console.ReadLine();
            var args = " login -l admin -p admin";
            cmd.Execute(ParseArguemts(args.Trim(' ')));
            while (true)
            {
                try
                {
                    Console.Write("command->");
                    args = Console.ReadLine();
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
                cfg.CreateMap<DirectoryInfo, DirectoryInfo>();

            });
        }

        private static string[] ParseArguemts(string arguments)
        {
            var separators = new char[] { ' ' };
            return arguments.Split(separators);

        }


    }
}
