using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using NetMastery.Lab05.FileManager.UI.Views;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService,
                              IUserContext context) : base(context)
        {
            _authenticationService = authenticationService;
        }

        public ActionResult SinginPost(string login, string password)
        {
            var model = new SigninPostViewModel(login, password);
            if (model.IsValid)
            {
                var result = _authenticationService.Signin(model.Login, model.Password);
                if (!IsCurrentUser(model))
                {
                    _userContext.Login = result.Login;
                    _userContext.CurrentPath = result.RootDirectory;
                    _userContext.RootDirectory =result.RootDirectory;
                    return new ViewResult(new InfoView($"Welcome, {result.Login}"));
                }
                return new ViewResult(new InfoView($"{result.Login} has already signed in th the system"));
            }
            return new ViewResult(new ErrorView(model.Errors));
        }

        public ActionResult SigninGet()
        {
            return new ViewResult(new SigninGetView());
        }


        public ActionResult Signoff()
        {
            if (_userContext.Login != null)
            {
                _userContext.Clear();
                return new ViewResult(new InfoView($"Goodby!"));
            }
            return new ViewResult(new InfoView($"There is no any registered user"));
        }

        public bool IsCurrentUser(SigninPostViewModel model)
        {
            if (model.Login == _userContext.Login)
            { 
                return true;
            }
            return false;
        }
    }
}
