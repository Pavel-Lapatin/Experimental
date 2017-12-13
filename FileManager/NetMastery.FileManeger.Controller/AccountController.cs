﻿using System;
using System.Linq;
using System.Security.Cryptography;
using AutoMapper;
using NetMastery.Lab05.FileManager.DAL.Entities;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DTO;


namespace NetMastery.FileManeger.Controller
{
    public class AccountController
    {
        public AccountDto CurrentUser { get; set; }
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        private const int Pbkdf2Iterations = 5000;
        private const int IterationIndex = 0;
        private const int SaltIndex = 1;
        private const int Pbkdf2Index = 2;

        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository repository)
        {
            _accountRepository = repository;
        }

        #region Autentication

        public bool VerifyPassword(string login, string password)
        {
            var account = _accountRepository.Find(x => x.Login == login).FirstOrDefault();
            if (account == null) throw new NullReferenceException();
            if (ValidatePassword(password, _accountRepository.GetPasswordByLogin(login)))
            {
                CurrentUser =  Mapper.Instance.Map<AccountDto>(account);
                return true;
            }
            return false;
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

        public bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var iterations = Int32.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);

            var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
            return Equals(hash, testHash);
        }

        private byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
        #endregion

        #region Registration

        public void RegisterUser(string login, string password)
        {
            var hashPassword = HashPassword(password);
            var newAccount = new AccountDto
            {
                Login = login,
                Password = hashPassword,
                CreationDate = DateTime.Now
            };
            _accountRepository.Add(Mapper.Map<Account>(newAccount));
        }
        #endregion
    }
}
