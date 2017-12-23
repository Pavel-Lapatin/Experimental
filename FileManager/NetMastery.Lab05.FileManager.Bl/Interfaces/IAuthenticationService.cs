using NetMastery.Lab05.FileManager.Dto;

namespace NetMastery.FileManager.Bl.Interfaces
{
    public interface IAuthenticationService
    {
        UserInfo Signin(string login, string password);
        void Singup(string login, string password);
    }
}
