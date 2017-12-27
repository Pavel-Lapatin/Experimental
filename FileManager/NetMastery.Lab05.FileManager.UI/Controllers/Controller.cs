using NetMastery.Lab05.FileManager.UI.events;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    
    
    public abstract class Controller
    {
        public RedirectEvent Redirect = new RedirectEvent();

        protected IUserContext _userContext;

        public Controller(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public bool IsAthenticated()
        {
            if(_userContext.Login == null )
            {
                _userContext.RenderError();
                
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
                Parameters = null
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
    }
}
