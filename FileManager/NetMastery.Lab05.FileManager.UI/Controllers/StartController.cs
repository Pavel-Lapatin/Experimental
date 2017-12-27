using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class StartController : Controller
    {
        StartViewModel StartViewModel { get; set; }

        public StartController(StartViewModel model, IUserContext userContext) : base(userContext)
        {
            StartViewModel = model;
        }

        public void Start()
        {
            StartViewModel.Render();
            var e = new RedirectEventArgs
            {
                ControllerType = typeof(LoginController),
                Method = "SigninGet",
                Parameters = null
            };
            Redirect.OnRedirect(this, e);
        }

    }
}
