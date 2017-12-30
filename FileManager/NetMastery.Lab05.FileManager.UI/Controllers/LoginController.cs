using AutoMapper;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;


namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService,  
            IUserContext userContext, RedirectEvent redirect) : base(userContext, redirect)
        {
            _authenticationService = authenticationService;
        }

        public void Singin(string login, string password)
        {
            var form = new LoginForm(_userContext.CurrentPath, login, password);
            if (form.IsValid)
            {
                var accountViewModel = Mapper.Instance.Map<AccountViewModel>(_authenticationService.Signin(form.Login, form.Password));

                if (!IsUser(accountViewModel))
                {
                    _userContext.Login = accountViewModel.Login;
                    _userContext.CurrentPath = accountViewModel.RootDirectory;
                    _userContext.RootDirectory = accountViewModel.RootDirectory;
                    accountViewModel.Messages.Add($"Welcome to the system, {accountViewModel.Login}");
                }
                accountViewModel.RenderMessages();
                accountViewModel.RenderViewModel();
                GetCommandRedirect();
            }
            else
            {
                form.RenderErrors();
            }
        }

        public string SigninGet(LoginForm form)
        {
            form.RenderForm();
            var command = Console.ReadLine();
            return "login -l " + command;
        }


        public void Signoff()
        {
            var accountViewModel = Mapper.Instance.Map<AccountViewModel>(_userContext);
            
            if (accountViewModel.Login != null)
            {
                accountViewModel.Messages.Add($"Goodbay {accountViewModel.Login}");
                _userContext.Clear();
            }
            else
            {
                accountViewModel.Messages.Add($"There is no any registered user");
            }
            LoginGetRedirect();
            accountViewModel.RenderMessages();
        }

        public bool IsUser(AccountViewModel model)
        {
            if (model.Login == _userContext.Login)
            {
                model.Messages.Add($"{model.Login} is a current user of the system");
                return true;
            }
            return false;
        }
    }
}
