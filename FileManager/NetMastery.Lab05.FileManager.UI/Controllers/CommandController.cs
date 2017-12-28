using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class CommandController : Controller
    {
        public CommandController(IUserContext userContext, RedirectEvent redirect) : base(userContext, redirect)
        {
        }

        public string GetCommand()
        {
            if(_userContext.IsAuthenticated)
            {
                var form = new CommandForm { CurrentPath = _userContext.CurrentPath};
                form.RenderForm();
                return Console.ReadLine();
            }
            else
            {
                LoginGetRedirect();
            }
            return null;
        }
    }
}
