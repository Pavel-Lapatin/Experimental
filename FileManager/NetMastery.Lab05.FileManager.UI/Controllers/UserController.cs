using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.ViewModel;
using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class UserController : AuthenticateController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, AppViewModel model ) : base(model)
        {
            _userService = userService;
        }

        public void GetUserInfo()
        {
            var userInfo = _userService.GetInfoByLogin(Model.AuthenticatedLogin);
            if (userInfo == null)
                throw new NullReferenceException($"There is no user with login: {Model.AuthenticatedLogin}");
            else
            {
                Console.WriteLine();
                Console.WriteLine("Login: " + userInfo.Login);
                Console.WriteLine("Creation date: " + userInfo.CreationDate.ToString("yy-MM-dd"));
                Console.WriteLine("Used disk space: " + userInfo.CurentStorageSize + " kB");
                Console.WriteLine("Max disk space: " + userInfo.MaxStorageSize +" kB");
                Console.WriteLine("Root directory path: " + userInfo.RootDirectory.FullPath);
                Console.WriteLine();
            }
        }
    }
}
