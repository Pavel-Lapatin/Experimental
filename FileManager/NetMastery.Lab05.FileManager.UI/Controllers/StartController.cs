using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class StartController : Controller
    {

        public StartController(IUserContext userContext, RedirectEvent redirect) : base(userContext, redirect)
        {
        }

        public void Start(StartForm form)
        {
            _userContext.CurrentPath = "test2";
            form.Render();
            LoginGetRedirect();
        }

    }
}
