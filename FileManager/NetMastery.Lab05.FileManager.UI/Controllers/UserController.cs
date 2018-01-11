using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService,
                              IUserContext context,
                              Func<Type, string, object[], RedirectResult> redirect,
                              Func<ViewModel, ViewResult> viewResult
                              ) : base(context, redirect, viewResult)
        {
            _userService = userService;
        }

        public ActionResult GetUserInfo()
        {
            if(_userContext.IsAuthenticated)
            {
                var model = new UserInfoViewModel
                {
                    Account = _userService.GetInfoByLogin(_userContext.Login)
                };
                return _viewResult(model);
            }
            return _redirect(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }
    }
}
