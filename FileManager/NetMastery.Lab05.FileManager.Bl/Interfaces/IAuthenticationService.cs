using NetMastery.Lab05.FileManager.Dto;

namespace NetMastery.FileManeger.Bl.Interfaces
{
    public interface IAuthenticationService
    {
        UserInfo Signin(string login, string password);
        UserInfo Singup(string login, string password);
    }
}
