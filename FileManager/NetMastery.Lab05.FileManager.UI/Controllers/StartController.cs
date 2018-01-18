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

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
    }
}
