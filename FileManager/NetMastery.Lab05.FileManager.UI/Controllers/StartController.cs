using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class StartController : Controller
    {

        public StartController(IUserContext context,
                              Func<Type, string, object[], RedirectResult> redirect,
                              Func<ViewModel, ViewResult> viewResult
                              ) : base(context, redirect, viewResult)
        {
        }

        public ActionResult Start()
        {
            return _viewResult(new StartViewModel());
        }

    }
}
