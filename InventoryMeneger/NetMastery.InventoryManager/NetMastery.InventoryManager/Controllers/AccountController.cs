using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using System.Threading.Tasks;
using NetMastery.InventoryManager.Models.AccountViewModels;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using AutoMapper;

namespace NetMastery.InventoryManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public AccountController(IUserService userService, IRoleService roleService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (Request.IsAjaxRequest())
            {
                return PartialView("LoginPartial");
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult CreateAccount(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (Request.IsAjaxRequest())
            {
                return PartialView("CreateAccountPartial");
            }
            else
            {
                return View();
            }
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await _userService.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe);
            switch (result)
            {
                case SignInStatus.Success:
                    var str = User.Identity.GetUserId();
                    var accountId = await _userService.GetAccountIdAsync(User.Identity.GetUserId());
                    Session["Account"] = accountId;
                    return RedirectToAction("Index", "Dashboard");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewAccount(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.RegisterNewAccount(model.UserName,
                    model.Email, model.PhoneNumber, model.Password);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                AddErrors(res);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

       
        #region Helpers
        

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        #endregion

    }
}