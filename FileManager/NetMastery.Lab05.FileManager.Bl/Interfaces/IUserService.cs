using NetMastery.Lab05.FileManager.Dto;

namespace NetMastery.FileManeger.Bl.Interfaces
{
    public interface IUserService
    {
        UserInfo GetInfoByLogin(string login);
    }
}
