using System;
using System.Diagnostics.Eventing.Reader;
using System.Resources;
using System.Security.Authentication;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.FileManeger.Controller;

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
                var password = x.Option(CommandLineNames.PasswordOption, rm.GetString("PasswordOptionHelpNote"), CommandOptionType.SingleValue);
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
                            Console.WriteLine(rm.GetString("FailedAuthenticationNote"));
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine(rm.GetString("FailedAuthenticationNote") + login);
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
                        throw new AuthenticationException();
                    }
                    Console.WriteLine();
                    Console.WriteLine(rm.GetString("UserInfoCmdOutputLogin") + Program.AccountController.CurrentUser.Login);
                    Console.WriteLine(rm.GetString("UserInfoCmdOutputCreationDate") + Program.AccountController.CurrentUser.Login);
                    Console.WriteLine(rm.GetString("UserInfoCmdOutputStorageSize") + Program.AccountController.CurrentUser.Login);
                    Console.WriteLine();
                    return 0;
                });
            });

            cmd.Command(CommandLineNames.CdCommand, x =>
            {

                x.HelpOption(CommandLineNames.HelpOption);
                var login = x.Argument("path", "full or relative path for directory", false);
                
                x.OnExecute(() =>
                {
                    if (login.Value == null)
                    {
                        StorageController FindCurrentDirrectory(EventLogInformation value);
                    }
                    try
                    {
                        Console.WriteLine();
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine(rm.GetString("FailedAuthenticationNote") + login);
                    }
                    Console.WriteLine();
                    return 0;
                });
            });



        }
    }
}
