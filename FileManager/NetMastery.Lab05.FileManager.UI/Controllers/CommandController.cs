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
        }

        public string GetCommand()
        {
            if(IsAthenticated())
            {
                _commandVM.Render(_userContext.CurrentPath);
                return Console.ReadLine();
            }
            return null;
        }
    }
}
