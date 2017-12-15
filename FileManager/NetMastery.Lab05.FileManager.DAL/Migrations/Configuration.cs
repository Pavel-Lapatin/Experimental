namespace NetMastery.Lab05.FileManager.DAL.Migrations
{
    using NetMastery.Lab05.FileManager.DAL.Entities;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<NetMastery.Lab05.FileManager.DAL.FileManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "NetMastery.Lab05.FileManager.DAL.FileManagerDBContext";
        }

        protected override void Seed(NetMastery.Lab05.FileManager.DAL.FileManagerDbContext context)
        {
            /*
            var fileTypes = new[]
            {
                new FileType
                {
                    TypeId = 1,
                    Extension = ".exe",
                    RelatedProgram = "",
                },
                new FileType
                {
                    TypeId = 2,
                    Extension = ".doc",
                    RelatedProgram = "MicrosoftWord",
                },
                new FileType
                {
                    TypeId = 3,
                    Extension = ".txt",
                    RelatedProgram = "NotePad",
                },
                new FileType
                {
                    TypeId = 4,
                    Extension = ".html",
                    RelatedProgram = "MozilaFireFox",
                }
            };

            var directories = new[]
            {
                new DirectoryStructure
                {
                    DirectoryId = 1,
                    Name = "adminId1-Root",
                    CreationDate = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    ParentDirectoryId = null,
                },

                new DirectoryStructure
                {
                    DirectoryId = 2,
                    Name = "PashaId2-Root",
                    CreationDate = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    ParentDirectoryId = null,
                },

                new DirectoryStructure
                {
                    DirectoryId = 3,
                    Name = "adminId1-Dir1-Lvl1",
                    CreationDate = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    ParentDirectoryId = 1,
                },

                new DirectoryStructure
                {
                    DirectoryId = 4,
                    Name = "adminId1-Dir2-Lvl1",
                    CreationDate = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    ParentDirectoryId = 1,
                },

                 new DirectoryStructure
                {
                    DirectoryId = 5,
                    Name = "adminId1-Dir2-Lvl1",
                    CreationDate = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    ParentDirectoryId = 4
                }
            };

            context.Directories.AddOrUpdate(directories);

            var files = new[]
            {
                new FileStructure
                {
                    FileId = 1,
                    FileTypeId = 1,
                    Name = "DirId5-FileId1",
                    DirectoryId = 5,
                    CreationTime = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FileSize = 1024,
                    DownloadsNumber = 5
                },
                new FileStructure
                {
                    FileId = 2,
                    FileTypeId = 2,
                    Name = "DirId1-FileId2",
                    DirectoryId = 1,
                    CreationTime = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FileSize = 2048,
                    DownloadsNumber = 0
                },
                new FileStructure
                {
                    FileId = 3,
                    FileTypeId = 3,
                    Name = "DirId1-FileId3",
                    DirectoryId = 1,
                    CreationTime = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FileSize = 1024,
                    DownloadsNumber = 5
                },
                new FileStructure
                {
                    FileId = 4,
                    FileTypeId = 1,
                    Name = "DirId1-FileId4",
                    DirectoryId = 5,
                    CreationTime = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FileSize = 18000,
                    DownloadsNumber = 2
                },
                new FileStructure
                {
                    FileId = 1,
                    FileTypeId = 1,
                    Name = "DirId4-FileId3",
                    DirectoryId = 5,
                    CreationTime = new DateTime(2016,2,6),
                    ModificationDate = new DateTime(2016,2,6),
                    FileSize = 15000,
                    DownloadsNumber = 10
                }
            };

            context.Files.AddOrUpdate<FileStructure>(files);

            var accounts = new[]
            {
                new Account
                {
                    AccoountId = 1,
                   Login = "admin",
                   Password = "admin",
                   CreationDate = DateTime.Now,
                   RootDirectory = directories[0],
                },
                new Account
                {
                   AccoountId = 2,
                   Login = "Pasha",
                   Password = "PashaTheBest",
                   CreationDate = new DateTime(2015,01,18),
                   RootDirectory = directories[1]
                },
            };

            base.Seed(context);
            context.SaveChanges();
            */

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
