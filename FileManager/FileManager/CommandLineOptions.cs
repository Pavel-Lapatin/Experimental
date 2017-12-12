using System;
using System.Resources;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.FileManeger.Controller;
using NetMastery.FileManeger;

namespace NetMastery.FileManeger.ConsoleApp
{
    public static class CommandLineOptions
    {
        public static void AddCommands(CommandLineApplication cmd)
        {
            var rm = new ResourceManager(typeof(EnglishLocalisation));
            cmd.Name = "ConspleArgs";
            cmd.Description = "File Manager";
            cmd.HelpOption(CommandLineNames.HelpOption);

            cmd.Command(CommandLineNames.LoginCommand, x =>
            {
                x.HelpOption(CommandLineNames.HelpOption);
                var login = x.Option(CommandLineNames.LoginOption, rm.GetString("LoginOptionHelpNote"), CommandOptionType.SingleValue);
                var password = x.Option(CommandLineNames.PasswordOption, rm.GetString("PasswordOtionHelpNote"), CommandOptionType.SingleValue);
                x.OnExecute(() =>
                {
                    if (login.Value() == null || password.Value() == null)
                    {
                        throw new NullReferenceException();
                    }
                    try
                    {
                        if (Program.AccountController.VerifyPassword(login.Value(), password.Value()))
                        {
                            Console.WriteLine(rm.GetString("WelcomeNote") + login.Value());
                        }
                        else
                        {
                            Console.WriteLine(rm.GetString("FailedAthenticationNote"));
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine(rm.GetString("FailedAthenticationNote") + login);
                    }
                    Console.WriteLine();
                    return 0;
                });
            });

            cmd.Command(CommandLineNames.UserInfoCommand, x =>
            {
                x.HelpOption(CommandLineNames.HelpOption);
                x.OnExecute(() =>
                {
                    if (Program.AccountController.CurrentUser == null)
                    {
                        throw new NullReferenceException();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(rm.GetString("UserInfoCmdOutputLogin") + Program.AccountController.CurrentUser.Login);
                        Console.WriteLine(rm.GetString("UserInfoCmdOutputCreationDate") + Program.AccountController.CurrentUser.Login);
                        Console.WriteLine(rm.GetString("UserInfoCmdOutputStorageSize") + Program.AccountController.CurrentUser.Login);
                    }
                    Console.WriteLine();
                    return 0;
                });
            });





        }
    }
}
