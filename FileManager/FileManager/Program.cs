using System.ComponentModel;
using NetMastery.FileManeger.Controller;
using NetMastery.Lab05.FileManager.DAL.Entities;
using NetMastery.Lab05.FileManager.BL;
using NetMastery.Lab05.FileManager.Repository.Repository;
using System;
using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.FileManeger.ConsoleApp
{
   
    class Program
    {
        internal static AccountController AccountController;

        static Program()
        {
            InitializeControllers();
        }

        static void Main()
        {
            Console.WriteLine($"Hi, please login");
            while (true)
            {
                Console.Write("command->");
                Console.WriteLine(" login -l admin -p admin");
                var args = Console.ReadLine();
                args = " login -l admin -p admin";
                CommandLineApplication cmd = new CommandLineApplication();
                CommandLineOptions.AddCommands(cmd);
                cmd.Execute(ParseArguemts(args.Trim(' ')));
            }
        }

        private static void InitializeControllers()
        {
            AccountController = new AccountController(new AccountRepository());
        }

        private static string[] ParseArguemts(string arguments)
        {
            var separators = new char[] { ' ' };
            return arguments.Split(separators);

        }
    }
}
