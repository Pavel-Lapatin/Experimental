using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using NetMastery.Lab05.FileManager.UI.Views;
using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService,
                              IUserContext context) : base(context)
        {
            _userService = userService;
        }

        public ActionResult GetUserInfo()
        {
            if (_userContext.IsAuthenticated)
            {
                var result = _userService.GetInfoByLogin(_userContext.Login);
                return new ViewResult(new UserInfoView(result));
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }
    }
}
