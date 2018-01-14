using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class StartController : Controller
    {

        public StartController(IUserContext context) : base(context)
        {
        }

        public ActionResult Start()
        {
            return new ViewResult(new StartView());
        }

    }
}
