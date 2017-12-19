using System;
using System.Resources;
using System.Security.Authentication;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.DAL;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.BL.Servicies;
using NetMastery.Lab05.FileManager.BL;
using NetMastery.Lab05.FileManager.BL.Dto;

namespace NetMastery.Lab05.FileManager
{
    public static class CommandLineOptions
    {
        public static void AddCommands(CommandLineApplication cmd, FileManagerModel model)
        {
            var rm = new ResourceManager(typeof(EnglishLocalisation));
            cmd.Name = "ConsoleArgs";
            cmd.Description = "FileInfo Manager";
            cmd.HelpOption(CommandLineNames.HelpOption);

            #region login command
            cmd.Command(CommandLineNames.LoginCommand, x =>
            {
                x.HelpOption(CommandLineNames.HelpOption);
                var login = x.Option(CommandLineNames.LoginOption, rm.GetString("LoginOptionHelpNote"),
                    CommandOptionType.SingleValue);
                var password = x.Option(CommandLineNames.PasswordOption, rm.GetString("PasswordOptionHelpNote"),
                    CommandOptionType.SingleValue);
                x.OnExecute(() =>
                {
                    using (var accountService = new AccountService(rm, new UnitOfWork(new FileManagerDbContext())))
                    {
                        if (accountService.VerifyPassword(login.Value(), password.Value(), model))
                        {
                            Console.WriteLine(rm.GetString("WelcomeNote") + login.Value());
                        }
                        else
                        {
                            Console.WriteLine(rm.GetString("FailedAuthenticationNote"));
                        }
                    }
                    return 0;
                });
            });
            #endregion
            #region singup command
            cmd.Command(CommandLineNames.SingupCommand, x =>
            {
                x.HelpOption(CommandLineNames.HelpOption);
                var login = x.Option(CommandLineNames.LoginOption, rm.GetString("LoginOptionHelpNote"),
                    CommandOptionType.SingleValue);
                var password = x.Option(CommandLineNames.PasswordOption, rm.GetString("PasswordOptionHelpNote"),
                    CommandOptionType.SingleValue);
                x.OnExecute(() =>
                {
                    using (var accountService = new AccountService(rm, new UnitOfWork(new FileManagerDbContext())))
                    {
                        accountService.RegisterUser(login.Value(), password.Value());
                        Console.WriteLine(login.Value() + rm.GetString("CreateUserNote"));
                        Console.WriteLine();
                    }
                    return 0;
                });
            });
            #endregion
            #region user command
            cmd.Command(CommandLineNames.UserCommand, x =>
            {
                x.HelpOption(CommandLineNames.HelpOption);
                var info = x.Option(CommandLineNames.InfoOption, rm.GetString("DirectoryCreateOptionNote"),
                    CommandOptionType.NoValue);
                x.OnExecute(() =>
                {
                    if (model.AccountId == 0)
                    {
                        throw new AuthenticationException("Please sign in first");
                    }
                    using (var accountService = new AccountService(rm, new UnitOfWork(new FileManagerDbContext())))
                    {
                        var accountInfo = accountService.GetUserInfo(model.AccountId);
                        WriteUserInfo(accountInfo, rm);
                    }
                    return 0;
                });
            });

            #endregion
            #region directory command

            cmd.Command(CommandLineNames.DirectoryCommand, x =>
            {
                x.HelpOption(CommandLineNames.HelpOption);
                x.Command(CommandLineNames.InfoOption, c =>
                {
                    var info = c.Argument("path", rm.GetString("DirectoryInfoDescriptionNote"), false);
                    c.OnExecute(() =>
                    {
                        using (var directoryService = new DirectoryService(rm, new UnitOfWork(new FileManagerDbContext())))
                        {
                            var directoryInfo = directoryService.GetDirectoryInfo(info.Value, model.RootDirectoryId);
                            WriteDirectoryInfo(directoryInfo, model, rm);
                        }
                        return 0;
                    });
                });

                x.Command(CommandLineNames.CreateOption, c =>
                {
                    var cerate = c.Argument("path", rm.GetString("DirectoryCreateOptionNote"), true);
                    c.OnExecute(() =>
                    {
                        using (var directoryService = new DirectoryService(rm, new UnitOfWork(new FileManagerDbContext())))
                        {
                            if (cerate.Values.Count != 2) throw new ArgumentException("Create option has two parameters");
                            directoryService.AddNewCatalog(cerate.Values[0], cerate.Values[1], model.RootDirectoryId);
                            Console.WriteLine("Directory hsd been created successfully");
                        }
                        return 0;
                    });
                });
                x.Command(CommandLineNames.MoveOption, c =>
                {
                    var move = c.Argument("path", rm.GetString("DirectoryMoveOptionNote"), true);
                    c.OnExecute(() =>
                    {
                        using (var directoryService = new DirectoryService(rm, new UnitOfWork(new FileManagerDbContext())))
                        {
                            if (move.Values.Count != 2) throw new ArgumentException();
                            directoryService.MoveCatalog(move.Values[0], move.Values[1], model.RootDirectoryId);
                            Console.WriteLine("Directory hsd been moved successfully");
                        }
                        return 0;
                    });
                });
                x.Command(CommandLineNames.ListOption, c =>
                {
                    var list = c.Argument("path", rm.GetString("DirectoryListOptionNote"), false);
                    c.OnExecute(() =>
                    {
                        using (var directoryService = new DirectoryService(rm, new UnitOfWork(new FileManagerDbContext())))
                        {
                            var directory = directoryService.GetRootDirectory(list.Value, model.RootDirectoryId);
                            RecursiveWriter(directory);
                        }
                        return 0;
                    });
                });
                x.Command(CommandLineNames.RemoveOption, c =>
                {
                    var remove = c.Argument("path", rm.GetString("DirectoryRemoveOptionNote"), false);
                    c.OnExecute(() =>
                    {
                        using (var directoryService = new DirectoryService(rm, new UnitOfWork(new FileManagerDbContext())))
                        {
                            directoryService.RemoveCatalog(remove.Value, model.RootDirectoryId);
                            Console.WriteLine("Directory has been removed successfully");
                        }
                        return 0;
                    });
                });
                x.Command(CommandLineNames.SearchOption, c =>
                {
                    var search = c.Argument("path", rm.GetString("DirectorySearchOptionNote"), true);
                    c.OnExecute(() =>
                    {
                        using (var directoryService = new DirectoryService(rm, new UnitOfWork(new FileManagerDbContext())))
                        {
                            if (search.Values.Count != 2) throw new ArgumentException();
                            var results = directoryService.Search(search.Values[0], search.Values[1], model.RootDirectoryId);
                            foreach (var result in results)
                            {
                                Console.WriteLine(result);
                            }
                            Console.WriteLine();
                        }
                        return 0;
                    });
                });
                x.Command(CommandLineNames.ChangeDirectoryOption, c =>
                {
                    var cd = c.Argument("path", rm.GetString("ChangeDirectoryOptionNote"), false);
                    c.OnExecute(() =>
                    {
                        using (var directoryService = new DirectoryService(rm, new UnitOfWork(new FileManagerDbContext())))
                        {
                            directoryService.ChangeWorkDirectory(cd.Value, model);
                        }
                        return 0;
                    });
                });   
            });
            #endregion

            #region fileCommand
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

                    if (model.AccountId == 0)
                    {
                        throw new AuthenticationException("Please sign in first");
                    }
                    using (var fileService = new DirectoryService(rm, new UnitOfWork(new FileManagerDbContext())))
                    {
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

        public static void WriteUserInfo(AccountDto account, ResourceManager rm)
        {
            
        }

        public static void WriteDirectoryInfo(DirectoryStructureDto directory, FileManagerModel model, ResourceManager rm)
        {
            Console.WriteLine("name: " + directory.Name);
            Console.WriteLine("path " + directory.FullPath);
            Console.WriteLine("creation date: " + directory.CreationDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("modification date " + directory.ModificationDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("size " + directory.DirectorySize + " kB");
            Console.WriteLine("login " + model.LoginName);
            Console.WriteLine();
        }

        private static void RecursiveWriter(DirectoryStructureDto rootDirectory)
        {
            while (rootDirectory.ChildrenDirectories != null || rootDirectory.ChildrenDirectories.Count != 0)
            {
                Console.WriteLine(rootDirectory.Name);
                if (rootDirectory.Files != null)
                {
                    foreach (var file in rootDirectory.Files)
                    {
                        Console.WriteLine(file.Name);
                    }
                }
                foreach (var child in rootDirectory.ChildrenDirectories)
                {
                    RecursiveWriter(child);
                }
            }
            
        }
    }
}
