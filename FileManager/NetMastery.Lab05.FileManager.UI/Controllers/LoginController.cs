using NetMastery.FileManeger.Bl.Interfaces;
using NetMastery.Lab05.FileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public LoginController(IAuthenticationService authenticationService, AppViewModel model) : base(model)
        {
            _authenticationService = authenticationService;
        }

        public bool Singin(string login, string password)
        {
            if (login == null || password == null)
            {
                throw new NullReferenceException("Login and password are required");
            }
            Model.AuthenticatedLogin = _authenticationService.Signin(login, password)?.Login;
            if (Model.AuthenticatedLogin == null)
            {
                return false;
            }
            return true;
        }
    }
}
