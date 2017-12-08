using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;

namespace FileManager
{
    internal static class CommandLineHelpers
    {
        public static void AddCommands(CommandLineApplication  cmd, ResourceManager rm)
        {
            cmd.Name = "ConspleArgs";
            cmd.Description = "File Manager";
            cmd.HelpOption(CommandLineNames.HelpOption);
            //cmd.Command(CommandLineNames.LsCommand, x =>
            //{
            //    x.HelpOption(CommandLineNames.HelpOption);
            //    x.OnExecute(() =>
            //    {
            //        Console.WriteLine();
            //        return 0;
            //    });
            //});
            cmd.Command(CommandLineNames.LoginCommand, x =>
            {
                x.HelpOption(CommandLineNames.HelpOption);
                var login = x.Option(CommandLineNames.LoginOption, rm.GetString(), CommandOptionType.SingleValue);
                var password = x.Option(CommandLineNames.PasswordOption, rm.GetString(), CommandOptionType.SingleValue);
                x.OnExecute(() =>
                {
                    if (login == null || password == null)
                    {
                        throw new NullReferenceException("Login or Password are required");
                    }
                    VerifyPassword(login, password);
                    Console.WriteLine();
                    return 0;
                });
            });



            
            
        }
    }
}
