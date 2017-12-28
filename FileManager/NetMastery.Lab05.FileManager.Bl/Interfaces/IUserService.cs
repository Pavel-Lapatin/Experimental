using NetMastery.Lab05.FileManager.Dto;

namespace NetMastery.Lab05.FileManager.Bl.Interfaces
{ 
    public interface IUserService
    {
        AccountDto GetInfoByLogin(string login);
    }
}
