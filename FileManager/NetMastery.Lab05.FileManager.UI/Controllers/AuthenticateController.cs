using NetMastery.Lab05.FileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
