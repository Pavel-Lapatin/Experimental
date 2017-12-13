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

        protected override void Seed(FileManagerDbContext context)
        {
            context.Accounts.AddRange(new[]
            {
                new Account
                {
                    Login = "admin",
                    Password = "admin",
                    CreationDate = DateTime.Now
                },
                 new Account
                {
                    Login = "Pasha",
                    Password = "PashaTheBest",
                    CreationDate = new DateTime(2015,01,18)
                }
            });

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

