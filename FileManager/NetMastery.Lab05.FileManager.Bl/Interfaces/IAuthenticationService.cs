using NetMastery.Lab05.FileManager.Dto;

namespace NetMastery.Lab05.FileManager.Bl.Interfaces
{
    public interface IAuthenticationService
    {
        AccountDto Signin(string login, string password);
        void Singup(string login, string password);
    }
}
