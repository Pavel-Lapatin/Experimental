using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using System.Threading.Tasks;
using NetMastery.InventoryManager.Models;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using AutoMapper;
using Microsoft.Owin.Security;
using System.Web;

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
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _userService.PasswordSignIn(model.UserName, model.Password, model.RememberMe);
            switch (result)
            {
                case SignInStatus.Success:

                    var user = _userService.FindByName(model.UserName);
                    if(user == null)
                    {
                        ModelState.AddModelError("", "Invallid login attempt");
                        return View(model);
                    }
                    Session["Account"] = user.AccountId;
                    return RedirectToAction("Index", "Dashboard");
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAccount(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.RegisterNewAccount(model.UserName,
                    model.Email, model.PhoneNumber, model.Password);
                if (res.Succeeded)
                {
                    Session["Account"] = _userService.GetAccountId(User.Identity.GetUserId());
                    return RedirectToAction("Index", "Dashboard");
                }
                AddErrors(res);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            _userService.Dispose();
            _roleService.Dispose();
            base.Dispose(disposing);
        }
    }
}