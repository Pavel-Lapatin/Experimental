using NetMastery.Lab05.FileManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Cryptography;


namespace NetMastery.Lab05.FileManager.DAL
{
    public class FileManegerInitializer : CreateDatabaseIfNotExists<FileManagerDbContext>
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        private const int Pbkdf2Iterations = 5000;

        public FileManegerInitializer()
        {
            
        }
        protected override void Seed(FileManagerDbContext context)
        {
            context.FileTypes.AddRange(new[]
            {
                new FileType
                {
                    TypeId = 1,
                    Extension = ".exe",
                    RelatedProgram = "Explorer"
                },
                 new FileType
                {
                    TypeId = 2,
                    Extension = ".doc",
                    RelatedProgram = "MicrosoftWord"
                },
                  new FileType
                {
                    TypeId = 1,
                    Extension = ".html",
                    RelatedProgram = "MozilaFireFox"
                }
            });
            /*
           context.Accounts.AddRange(new[]
           {

               new Account
               {
                   Login = "admin",
                   Password = "admin",
                   CreationDate = DateTime.Now,
                   Storage = new DirectoryInfo
                   {
                       StorageId = 1,
                       Name = "adminRoot",
                       CreationDate = new DateTime(2000,2,6),
                       ModificationDate = new DateTime(2015,4,23),
                       ChildrenStoragies = new []
                       {
                           new DirectoryInfo
                           {
                               StorageId = 2,
                               Name = "adminId2-Lvl1",
                               CreationDate = new DateTime(2016,2,6),
                               ModificationDate = new DateTime(2016,2,6),
                               ParentStorageId = 1,
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
                               ChildrenStoragies = new []
                               {
                                   new DirectoryInfo
                                   {
                                       StorageId = 6,
                                       Name = "adminId6-Lvl2",
                                       CreationDate = new DateTime(2016,2,6),
                                       ModificationDate = new DateTime(2016,2,6),
                                       ParentStorageId = 2,
                                       ChildrenStoragies = new []
                                       {
                                           new DirectoryInfo
                                           {
                                            StorageId = 8,
                                               Name = "adminId6-Lvl3",
                                               CreationDate = new DateTime(2016,2,6),
                                               ModificationDate = new DateTime(2016,2,6),
                                               ParentStorageId = 6,
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
                                       StorageId = 7,
                                       Name = "adminId7-Lvl2",
                                       CreationDate = new DateTime(2016,2,6),
                                       ModificationDate = new DateTime(2016,2,6),
                                       ParentStorageId = 1
                                   }
                               },
                           },
                           new DirectoryInfo
                           {
                               StorageId = 3,
                               Name = "adminId3-Lvl1",
                               CreationDate = new DateTime(2014,3,11),
                               ModificationDate = new DateTime(2016,4,3),
                               ParentStorageId = 1,
                           }
                       }

                   }

               },

               new Account
               {
                   Login = "Pasha",
                   Password = "PashaTheBest",
                   CreationDate = new DateTime(2015,01,18),
                   Storage = new DirectoryInfo
                   {
                      StorageId = 9,
                      Name = "PashaRoot",
                      CreationDate = new DateTime(2000,2,6),
                      ModificationDate = new DateTime(2015,4,23),
                   }

               }
           });
            */
            base.Seed(context);
            context.SaveChanges();
        }

        private static string HashPassword(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            var salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);

            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
            return Pbkdf2Iterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) {IterationCount = iterations};
            return pbkdf2.GetBytes(outputBytes);
        }
    }    
}

