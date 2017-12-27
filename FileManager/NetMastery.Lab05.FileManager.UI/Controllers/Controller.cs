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

    }
}
