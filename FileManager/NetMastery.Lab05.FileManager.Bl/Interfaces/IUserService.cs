using NetMastery.Lab05.FileManager.Dto;

namespace NetMastery.FileManager.Bl.Interfaces
{
    public interface IUserService
    {
        AccountDto GetInfoByLogin(string login);
    }
}
