namespace NetMastery.Lab05.FileManager.DAL.Migrations
{
    using NetMastery.Lab05.FileManager.Domain;
    using System;
    using System.Data.Entity.Migrations;


    internal sealed class Configuration : DbMigrationsConfiguration<NetMastery.Lab05.FileManager.DAL.FileManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "NetMastery.Lab05.FileManager.DAL.FileManagerDBContext";
        }

        protected override void Seed(NetMastery.Lab05.FileManager.DAL.FileManagerDbContext context)
        {
            var rootDirectories = new[]
            {
                new DirectoryStructure
                {
                    DirectoryId = 1,
                    Name = "adminRoot",
                    CreationDate = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FullPath = "~\\adminRoot"
                },

                new DirectoryStructure
                {
                    DirectoryId = 2,
                    Name = "PashaRoot",
                    CreationDate = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FullPath = "~\\PashaRoot"
                },

            };

            //Directory.CreateDirectory(".\\CommonStorage\\adminRoot");
            //Directory.CreateDirectory(".\\CommonStorage\\PashaRoot");

            var directories = new[]
            {

                new DirectoryStructure
                {
                    DirectoryId = 3,
                    Name = "admin-Dir1-Lvl1",
                    CreationDate = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    ParentDirectory = rootDirectories[0],
                    FullPath = "~\\adminRoot\\admin-Dir2-Lvl1"
                },

                new DirectoryStructure
                {
                    DirectoryId = 4,
                    Name = "admin-Dir2-Lvl1",
                    CreationDate = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    ParentDirectory = rootDirectories[0],
                    FullPath = "~\\admin-Root\\admin-Dir1-Lvl1"
                },

                 new DirectoryStructure
                {
                    DirectoryId = 5,
                    Name = "admin-Dir2.1-Lvl2",
                    CreationDate = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FullPath = "~\\admin-Root\\admin-Dir1-Lvl1\\admin-Dir2.1-Lvl2"

                }
            };

            directories[2].ParentDirectory = directories[1];

            //Directory.CreateDirectory(".\\CommonStorage\\adminRoot\\admin-Dir1-Lvl1");
            //Directory.CreateDirectory(".\\CommonStorage\\adminRoot\\admin-Dir2-Lvl1");
            //Directory.CreateDirectory(".\\CommonStorage\\adminRoot\\admin-Dir1-Lvl1\\admin-Dir2.1-Lvl2");

            var files = new[]
            {
                new FileStructure
                {
                    FileId = 1,
                    Extension = ".txt",
                    Name = "file1",
                    Directory = directories[2],
                    CreationTime = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FileSize = 1024,
                    DownloadsNumber = 5
                     
                },
                new FileStructure
                {
                    FileId = 2,
                    Extension = ".html",
                    Name = "file2",
                    Directory = rootDirectories[0],
                    CreationTime = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FileSize = 2048,
                    DownloadsNumber = 0
                },
                new FileStructure
                {
                    FileId = 3,
                    Extension = ".txt",
                    Name = "file3",
                    Directory = rootDirectories[0],
                    CreationTime = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FileSize = 1024,
                    DownloadsNumber = 5
                },
                new FileStructure
                {
                    FileId = 4,
                    Extension = ".txt",
                    Name = "file4",
                    Directory = directories[2],
                    CreationTime = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FileSize = 18000,
                    DownloadsNumber = 2
                },
                new FileStructure
                {
                    FileId = 5,
                    Extension = ".pdf",
                    Name = "file5",
                    Directory = directories[2],
                    CreationTime = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FileSize = 15000,
                    DownloadsNumber = 10
                }
            };

            context.Files.AddOrUpdate<FileStructure>(files);

            //var file = File.Create(".\\CommonStorage\\adminRoot\\admin-Dir1-Lvl1\\admin-Dir2.1-Lvl2\\"+files[0].Name+files[0].Extension);
            //file.SetLength(1024*1024);
            //file = File.Create(".\\CommonStorage\\adminRoot\\" + files[1].Name + files[1].Extension);
            //file.SetLength(2048 * 1024);
            //file = File.Create(".\\CommonStorage\\adminRoot\\" + files[2].Name + files[2].Extension);
            //file.SetLength(1024 * 1024);
            //file = File.Create(".\\CommonStorage\\adminRoot\\admin-Dir1-Lvl1\\admin-Dir2.1-Lvl2\\" + files[3].Name + files[3].Extension);
            //file.SetLength(1024 * 18000);
            //file = File.Create(".\\CommonStorage\\admin-Root\\admin-Dir1-Lvl1\\admin-Dir2.1-Lvl2\\" + files[4].Name + files[4].Extension);
            //file.SetLength(1024 * 15000);

            var accounts = new[]
            {
                new Account
                {
                   AccountId = 1,
                   Login = "admin",
                   Password = "$2a$10$q4Tpdy6rhVqWAIQgWNCzd.04Td7g4xy55RikeKYJP0CBHWtGBoJkW",
                   CreationDate = DateTime.Now,
                   RootDirectory = rootDirectories[0],
                },
                new Account
                {
                   AccountId = 2,
                   Login = "Pasha",
                   Password = "$2a$10$euq/KV3PAGkUsfqc3kA7Zu0qNXr5SHZ97Y57lo1n7qzYR9vLTgJWG",
                   CreationDate = new DateTime(2015,01,18),
                   RootDirectory = rootDirectories[1]
                },
            };

            context.Accounts.AddOrUpdate(accounts);
            context.Directories.AddOrUpdate(rootDirectories);
            context.Directories.AddOrUpdate(directories);
            context.Files.AddOrUpdate(files);
            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}