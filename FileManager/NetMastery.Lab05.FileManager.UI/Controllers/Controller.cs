using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{   
    public class Controller
    {
        protected readonly IUserContext _userContext;

        protected Controller(IUserContext userContext)
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
