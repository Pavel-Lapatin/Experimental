using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Authentication;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.FileManeger.Controller;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Repository;

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

            #region CommandsForAccount  

            cmd.Command(CommandLineNames.LoginCommand, x =>
            {
                x.HelpOption(CommandLineNames.HelpOption);
                var login = x.Option(CommandLineNames.LoginOption, rm.GetString("LoginOptionHelpNote"),
                    CommandOptionType.SingleValue);
                var password = x.Option(CommandLineNames.PasswordOption, rm.GetString("PasswordOptionHelpNote"),
                    CommandOptionType.SingleValue);
                x.OnExecute(() =>
                {
                    if (login.Value() == null || password.Value() == null)
                    {
                        throw new NullReferenceException();
                    }
                    try
                    {
                        using (var unitOfWork = new UnitOfWork(new FileManagerDbContext()))
                        {
                            var accountController = new AccountController(unitOfWork.Accounts);
                            if (accountController.VerifyPassword(login.Value(), password.Value()))
                            {
                                Console.WriteLine(rm.GetString("WelcomeNote") + login.Value());
                            }
                            else
                            {
                                Console.WriteLine(rm.GetString("FailedAuthenticationNote"));
                            }
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
                    Console.WriteLine(rm.GetString("UserInfoCmdOutputLogin") +
                                      Program.AccountController.CurrentUser.Login);
                    Console.WriteLine(rm.GetString("UserInfoCmdOutputCreationDate") +
                                      Program.AccountController.CurrentUser.Login);
                    Console.WriteLine(rm.GetString("UserInfoCmdOutputStorageSize") +
                                      Program.AccountController.CurrentUser.Login);
                    Console.WriteLine();
                    return 0;
                });
            });

            #endregion

            #region CommandsForDirrectory

            cmd.Command(CommandLineNames.DirectoryCommand, x =>
            {
                x.HelpOption(CommandLineNames.HelpOption);
                var info = x.Option(CommandLineNames.DirectoryInfoOption, rm.GetString("DirectoryCreateOptionNote"),
                    CommandOptionType.NoValue);
                var create = x.Option(CommandLineNames.DirectoryCreateOption, rm.GetString("DirectoryCreateOptionNote"),
                    CommandOptionType.MultipleValue);
                var move = x.Option(CommandLineNames.DirectoryMoveOption, rm.GetString("DirectoryMoveOptionNote"),
                    CommandOptionType.MultipleValue);
                var remove = x.Option(CommandLineNames.DirectoryRemoveOption, rm.GetString("DirectoryRemoveOptionNote"),
                    CommandOptionType.MultipleValue);
                var list = x.Option(CommandLineNames.DirectoryListOption, rm.GetString("DirectoryListOptionNote"),
                    CommandOptionType.MultipleValue);
                var search = x.Option(CommandLineNames.DirectorySearchOption, rm.GetString("DirectorySearchOptionNote"),
                    CommandOptionType.MultipleValue);

                x.OnExecute(() =>
                {
                    using (var unitOfWork = new UnitOfWork(new FileManagerDbContext()))
                    {
                        var StorageController = new StorageController(unitOfWork.Storagies);
                        if (info != null)
                        {
                            Console.WriteLine("Info");
                        }
                        else if (create != null)
                        {
                            if(create.Values.Count != 2) throw new ArgumentException();
                            Console.WriteLine("Create");
                        }
                        else if (move != null)
                        {
                            if (move.Values.Count != 2) throw new ArgumentException();
                            Console.WriteLine("Move");
                        }
                        else if (list != null)
                        {
                            if (list.Values.Count != 2) throw new ArgumentException();
                            Console.WriteLine("List");
                        }
                        else if (search != null)
                        {
                            if (search.Values.Count != 2) throw new ArgumentException();
                            Console.WriteLine("Search");
                        }
                        else if (remove != null)
                        {
                            if (remove.Values.Count != 2) throw new ArgumentException();
                            Console.WriteLine("Remove");
                        }
                    }
                    return 0;
                });

            });
            #endregion CommandsForDirrectory
        }
    }
}
