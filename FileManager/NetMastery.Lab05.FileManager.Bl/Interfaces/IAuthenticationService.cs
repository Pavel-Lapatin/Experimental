using NetMastery.Lab05.FileManager.Dto;
using System;

namespace NetMastery.Lab05.FileManager.Bl.Interfaces
{
    public interface IAuthenticationService : IDisposable
    {
        AccountDto Signin(string login, string password);
        void Singup(string login, string password);
    }
}
