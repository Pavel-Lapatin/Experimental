using NetMastery.Lab05.FileManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL
{
    public class FileManegerInitializer : CreateDatabaseIfNotExists<FileManagerDBContext>
    {
        private const int SaltByteSize = 8;
        private const int HashByteSize = 8; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        private const int Pbkdf2Iterations = 5000;
        private const int IterationIndex = 0;
        private const int SaltIndex = 1;
        private const int Pbkdf2Index = 2;

        protected override void Seed(FileManagerDBContext context)
        {
            context.Accounts.AddRange(new[]
            {
                new Account
                {
                    Login = "admin",
                    Password = HashPassword("admin"),
                    CreationDate = DateTime.Now
                },
                 new Account
                {
                    Login = "Pasha",
                    Password = HashPassword("PashaTheBest"),
                    CreationDate = new DateTime(2015,01,18)
                }
            });

            base.Seed(context);
        }

        private string HashPassword(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);

            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
            return Pbkdf2Iterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);
        }

        private byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }    
}

