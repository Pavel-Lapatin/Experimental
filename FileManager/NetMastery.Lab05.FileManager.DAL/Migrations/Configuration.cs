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
            context.Accounts.AddRange(new[]
           {

               new Account
               {
                   Login = "admin",
                   Password = "admin",
                   CreationDate = DateTime.Now,
                   Directory = new DirectoryInfo
                   {
                       DirectoryId = 1,
                       Name = "adminRoot",
                       CreationDate = new DateTime(2000,2,6),
                       ModificationDate = new DateTime(2015,4,23),
                       ChildrenDirectories = new []
                       {
                           new DirectoryInfo
                           {
                               DirectoryId = 2,
                               Name = "adminId2-Lvl1",
                               CreationDate = new DateTime(2016,2,6),
                               ModificationDate = new DateTime(2016,2,6),
                               ParentDirectoryId = 1,
                               Files = new []
                               {
                                   new FileInfo
                                   {
                                       FileId = 1,
                                       Name = "adminId2-FileId1",
                                       CreationTime = new DateTime(2010, 2, 2),
                                       ModificationDate = new DateTime(2013, 1, 1),
                                       DownloadsNumber = 3,
                                       FileSize = 40,
                                       FileType = new FileType
                                       {
                                            TypeId = 1,
                                            Extension = ".exe",
                                            RelatedProgram = "",
                                       }
                                   },
                                   new FileInfo
                                   {
                                       FileId = 2,
                                       Name = "adminId2-FileId2",
                                       CreationTime = new DateTime(2011, 2, 2),
                                       ModificationDate = new DateTime(2013, 1, 1),
                                       DownloadsNumber = 1,
                                       FileSize = 1024,
                                       FileType = new FileType
                                       {
                                            TypeId =2,
                                            Extension = ".doc",
                                            RelatedProgram = "MicrosofWord",
                                       }
                                   }

                               },
                               ChildrenDirectories = new []
                               {
                                   new DirectoryInfo
                                   {
                                       DirectoryId = 6,
                                       Name = "adminId6-Lvl2",
                                       CreationDate = new DateTime(2016,2,6),
                                       ModificationDate = new DateTime(2016,2,6),
                                       ParentDirectoryId = 2,
                                       ChildrenDirectories = new []
                                       {
                                           new DirectoryInfo
                                           {
                                            DirectoryId = 8,
                                               Name = "adminId6-Lvl3",
                                               CreationDate = new DateTime(2016,2,6),
                                               ModificationDate = new DateTime(2016,2,6),
                                               ParentDirectoryId = 6,
                                               Files = new []
                                               {
                                                   new FileInfo
                                                   {
                                                       FileId = 1,
                                                       Name = "adminId2-FileId1",
                                                       CreationTime = new DateTime(2010, 2, 2),
                                                       ModificationDate = new DateTime(2013, 1, 1),
                                                       DownloadsNumber = 3,
                                                       FileSize = 40,
                                                       FileType = new FileType
                                                       {
                                                           TypeId = 3,
                                                           Extension = ".html",
                                                           RelatedProgram = "MozilaFireFox",
                                                       }
                                                   },
                                               }
                                           }
                                       },

                                   },
                                   new DirectoryInfo
                                   {
                                       DirectoryId = 7,
                                       Name = "adminId7-Lvl2",
                                       CreationDate = new DateTime(2016,2,6),
                                       ModificationDate = new DateTime(2016,2,6),
                                       ParentDirectoryId = 1
                                   }
                               },
                           },
                           new DirectoryInfo
                           {
                               DirectoryId = 3,
                               Name = "adminId3-Lvl1",
                               CreationDate = new DateTime(2014,3,11),
                               ModificationDate = new DateTime(2016,4,3),
                               ParentDirectoryId = 1,
                           }
                       }

                   }

               },

               new Account
               {
                   Login = "Pasha",
                   Password = "PashaTheBest",
                   CreationDate = new DateTime(2015,01,18),
                   Directory = new DirectoryInfo
                   {
                      DirectoryId = 9,
                      Name = "PashaRoot",
                      CreationDate = new DateTime(2000,2,6),
                      ModificationDate = new DateTime(2015,4,23),
                   }

               }
           });


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
