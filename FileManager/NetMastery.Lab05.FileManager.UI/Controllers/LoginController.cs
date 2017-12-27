using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;


namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private LoginViewModel _loginVM;

        public LoginController(IAuthenticationService authenticationService,  
            LoginViewModel model,
            IUserContext userContext) : base(userContext)
        {
            _authenticationService = authenticationService;
            _loginVM = model;
        }

        public void Singin()
        {
            if (_loginVM.IsValid)
            {
                var newUser = _authenticationService.Signin(_loginVM.Login, _loginVM.Password);
                if (IsExistingUser())
                {
                    Console.WriteLine($"{ _loginVM.Login} is a current user of the system");
                }
                else
                {
                    _userContext.Login = newUser.Login;
                    _userContext.CurrentPath = newUser.RootDirectory.FullPath;
                    Console.WriteLine($"Welcome to the system, { _loginVM.Login}");
                }
            }
            else
            {
                _loginVM.RenderErrors();
            }
            GetCommandRedirect();
        }

        public string SigninGet()
        {
            _loginVM.RenderGet();
            var command = Console.ReadLine();
            return "login -l " + command;
        }
        public void Signoff()
        {
            if (IsAthenticated())
            {
               _loginVM.RenderSignoff();
                _userContext.Clear();
                LoginGetRedirect();
            }
        }

        public bool IsExistingUser()
        {
            if (_loginVM.Login == _userContext.Login)
            {
                Console.WriteLine($"{_loginVM.Login} is a current user of the system");
                return true;
            }
            return false;
        }
    }
}
