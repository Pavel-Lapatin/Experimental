using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{   
    public abstract class Controller
    {

        public IUserContext _userContext;


        public Controller(IUserContext userContext)
        {
            _userContext = userContext;
        }
 

        public string GetCurrentPath()
        {
            if (_userContext.CurrentPath != null)
            {
                return _userContext.CurrentPath;
            }
            throw new ArgumentNullException();
        }
    }
}
