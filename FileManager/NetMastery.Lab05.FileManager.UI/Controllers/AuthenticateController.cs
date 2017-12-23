using NetMastery.Lab05.FileManager.UI.ViewModel;
using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public abstract class AuthenticateController : Controller
    {
        public AuthenticateController(AppViewModel model) : base(model)
        {
            if(!IsAuthenticate())
            {
                throw new UnauthorizedAccessException("Signin first, please");
            }
        }
    }
}
