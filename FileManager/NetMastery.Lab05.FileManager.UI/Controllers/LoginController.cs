using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.ViewModel;
using System;


namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public LoginController(IAuthenticationService authenticationService, AppViewModel model) : base(model)
        {
            _authenticationService = authenticationService;
        }

        public void Singin(string login, string password)
        {
            if (login == null || password == null)
            {
                throw new NullReferenceException("Login and password are required");
            }
            var newUser = _authenticationService.Signin(login, password);
            if(Model.AuthenticatedLogin == newUser.Login)
            {
                Console.WriteLine("You are user of the system already");
            }
            else
            {
                Model.AuthenticatedLogin = newUser.Login;
                Model.CurrentPath = newUser.RootDirectory.FullPath;
                Console.WriteLine("Hi, " + Model.AuthenticatedLogin);
            }
        }

        public void Signoff()
        {
            if (Model.AuthenticatedLogin != null)
            {
                Model.AuthenticatedLogin = null;
                Model.CurrentPath = "~\\";
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
