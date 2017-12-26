using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.ViewModel;
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
            if(_loginVM.Login == _userContext.Login)
            {
                _loginVM.Errors.Add("This user is in the system now");
            }
        }

        public void Singin()
        {
            if (_loginVM.IsValid)
            {
                var newUser = _authenticationService.Signin(_loginVM.Login, _loginVM.Password);
                _userContext.Login = newUser.Login;
                _userContext.CurrentPath = newUser.RootDirectory.FullPath;
            }
            else
            {
                _loginVM.RenderErrors();
            }
        }

        public string SigninGet
        {
            
        }

        public void Signoff()
        {
            if (_userContext.Login != null)
            {
                _userContext.Login = null;
                _userContext.CurrentPath = "~\\";
                Console.WriteLine("Goodbye!!!");
                Console.WriteLine();
                Console.WriteLine("Press any button for continue");
                Console.ReadKey();
            } 
            else
            {
                Console.WriteLine("Nobady is registered in the system");
            }
        }
    }
}
