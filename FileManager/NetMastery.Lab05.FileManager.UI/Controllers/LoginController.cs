using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using NetMastery.Lab05.FileManager.UI.ViewModels.Login;


namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService,  
            IUserContext userContext) : base(userContext)
        {
            _authenticationService = authenticationService;
        }

        public ActionResult SinginPost(string login, string password)
        {
            var model = new SigninPostViewModel(login, password);
            if (model.IsValid)
            {
                model.Account = _authenticationService.Signin(model.Login, model.Password);

                if (!IsCurrentUser(model))
                {
                    _userContext.Login = model.Account.Login;
                    _userContext.CurrentPath = model.Account.RootDirectory;
                    _userContext.RootDirectory = model.Account.RootDirectory;
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);

        }

        public ActionResult SigninGet()
        {
            return new ViewResult(new SigninGetViewModel());
        }


        public ActionResult Signoff()
        {
            var model =new SignoffViewModel();
            if (_userContext.Login != null)
            {
                _userContext.Clear();
            }
            else
            {
                model.AddError(nameof(SignoffViewModel), $"There is no any registered user");
            }
            return new ViewResult(model);
        }

        public bool IsCurrentUser(SigninPostViewModel model)
        {
            if (model.Login == _userContext.Login)
            { 
                return true;
            }
            model.AddError(nameof(SigninPostViewModel.Login), "You already signed in the system");
            return false;
        }
    }
}
