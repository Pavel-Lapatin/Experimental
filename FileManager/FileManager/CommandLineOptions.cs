using System;
using System.Resources;
using System.Security.Authentication;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManeger.Bl.Servicies;
using NetMastery.Lab05.FileManager.BL.Servicies;
using NetMastery.Lab05.FileManager.BL;

namespace NetMastery.Lab05.FileManager
{
    public static class CommandLineOptions
    {
        public static void AddCommands(CommandLineApplication cmd, FileManagerModel model)
        {
            var rm = new ResourceManager(typeof(EnglishLocalisation));
            cmd.Name = "ConspleArgs";
            cmd.Description = "FileInfo Manager";
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
                    using (var unitOfWork = new UnitOfWork(new FileManagerDbContext()))
                    {
                        var accountService = new AccountService(unitOfWork.Accounts);
                        model.Account = accountService.VerifyPassword(login.Value(), password.Value());
                        if (model.Account != null)
                        {
                            Console.WriteLine(rm.GetString("WelcomeNote") + model.Account.Login);
                        }
                        else
                        {
                            Console.WriteLine(rm.GetString("FailedAuthenticationNote"));
                        }
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
                    if (model.Account == null)
                    {
                        throw new AuthenticationException();
                    }
                    using (var unitOfWork = new UnitOfWork(new FileManagerDbContext()))
                    {
                        var directoryService = new DirectoryService(unitOfWork.Directories);
                        Console.WriteLine();
                        Console.WriteLine(rm.GetString("UserInfoCmdOutputLogin") +
                                          model.Account.Login);
                        Console.WriteLine(rm.GetString("UserInfoCmdOutputCreationDate") +
                                          model.Account.CreationDate);
                        Console.WriteLine(rm.GetString("UserInfoCmdOutputStorageSize") +
                                         directoryService.CalculateFullSize(model.Account.Login));
                        Console.WriteLine();
                    }
                    return 0;
                });
            });

            #endregion

            #region CommandsForDirrectory

            cmd.Command(CommandLineNames.DirectoryCommand, x =>
            {
                x.HelpOption(CommandLineNames.HelpOption);
                var info = x.Option(CommandLineNames.InfoOption, rm.GetString("DirectoryCreateOptionNote"),
                    CommandOptionType.NoValue);
                var create = x.Option(CommandLineNames.CreateOption, rm.GetString("DirectoryCreateOptionNote"),
                    CommandOptionType.MultipleValue);
                var move = x.Option(CommandLineNames.MoveOption, rm.GetString("DirectoryMoveOptionNote"),
                    CommandOptionType.MultipleValue);
                var remove = x.Option(CommandLineNames.RemoveOption, rm.GetString("DirectoryRemoveOptionNote"),
                    CommandOptionType.MultipleValue);
                var list = x.Option(CommandLineNames.ListOption, rm.GetString("DirectoryListOptionNote"),
                    CommandOptionType.MultipleValue);
                var search = x.Option(CommandLineNames.SearchOption, rm.GetString("DirectorySearchOptionNote"),
                    CommandOptionType.MultipleValue);

                x.OnExecute(() =>
                {
                    using (var unitOfWork = new UnitOfWork(new FileManagerDbContext()))
                    {
                        var StorageController = new DirectoryService(unitOfWork.Storagies);
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

            #region CommandsForFiles
            cmd.Command(CommandLineNames.FileCommand, x =>
            {
                x.HelpOption(CommandLineNames.HelpOption);
                var info = x.Option(CommandLineNames.InfoOption, rm.GetString("DirectoryCreateOptionNote"),
                    CommandOptionType.NoValue);
                var upload = x.Option(CommandLineNames.UploadOption, rm.GetString("DirectoryCreateOptionNote"),
                    CommandOptionType.MultipleValue);
                var download = x.Option(CommandLineNames.MoveOption, rm.GetString("DirectoryMoveOptionNote"),
                    CommandOptionType.MultipleValue);
                var remove = x.Option(CommandLineNames.RemoveOption, rm.GetString("DirectoryRemoveOptionNote"),
                    CommandOptionType.MultipleValue);
                var move = x.Option(CommandLineNames.DownloadOption, rm.GetString("DirectoryListOptionNote"),
                    CommandOptionType.MultipleValue);
               

                x.OnExecute(() =>
                {
                    using (var unitOfWork = new UnitOfWork(new FileManagerDbContext()))
                    {
                        var StorageController = new DirectoryService(unitOfWork.Storagies);
                        if (info != null)
                        {
                            Console.WriteLine("Info");
                        }
                        else if (upload != null)
                        {
                            if (upload.Values.Count != 2) throw new ArgumentException();
                            Console.WriteLine("Upload");
                        }
                        else if (move != null)
                        {
                            if (move.Values.Count != 2) throw new ArgumentException();
                            Console.WriteLine("Move");
                        }
                        else if (download != null)
                        {
                            if (download.Values.Count != 2) throw new ArgumentException();
                            Console.WriteLine("Download");
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
            #endregion
        }
    }
}
