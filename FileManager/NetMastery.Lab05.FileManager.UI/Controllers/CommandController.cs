using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class CommandController : Controller
    {
        CommandViewModel _commandVM;

        public CommandController(IUserContext userContext, CommandViewModel model) : base(userContext)
        {
            _userContext = userContext;
            _commandVM = model;

            if (_userContext.Login == null)
            {
                Redirect.OnRedirect(this, new RedirectEventArgs
                {
                    ControllerType = typeof(LoginController),
                    Method = "SignInGet",
                    Parameters = null
                });
            }
        }

        public string GetCommand()
        {
            _commandVM.Render(_userContext.CurrentPath);
           return Console.ReadLine();
        }
    }
}
