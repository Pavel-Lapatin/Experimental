using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{   
    public abstract class Controller
    {
        public RedirectEvent Redirect;

        public IUserContext _userContext;


        public Controller(IUserContext userContext, RedirectEvent redirect)
        {
            _userContext = userContext;
            Redirect = redirect;
        }
    
        public bool IsAthenticated()
        {
            if(!_userContext.IsAuthenticated)
            {
                LoginGetRedirect();
                return false;
            }
            return true;
        }

        public void LoginGetRedirect()
        {
            Redirect.OnRedirect(this, new RedirectEventArgs
            {
                ControllerType = typeof(LoginController),
                Method = "SigninGet",
                Parameters = new[] { new LoginForm() }
            });
        }

        public void GetCommandRedirect()
        {
            Redirect.OnRedirect(this, new RedirectEventArgs
            {
                ControllerType = typeof(CommandController),
                Method = "GetCommand",
                Parameters = null
            });
        }

        public string GetCurrentPath()
        {
            if (_userContext.CurrentPath != null)
            {
                return _userContext.CurrentPath;
            }
            throw new NullReferenceException();
        }
    }
}
