using NetMastery.Lab05.FileManager.Dto;
using System;

namespace NetMastery.Lab05.FileManager.Bl.Interfaces
{ 
    public interface IUserService : IDisposable
    {
        AccountDto GetInfoByLogin(string login);
    }
}
