using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NetMastery.InventoryManager.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IUserService _userService;
        public ErrorController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        // GET: Error
        public async Task<ActionResult> AccessDenied()
        {
            var admin = await _userService.FindUserByRole((int)Session["accountId"], "admin");
            ViewData["Name"] = admin.UserName;
            ViewData["Email"] = admin.Email;
            ViewData["Phone"] = admin.PhoneNumber;
            return View();
        }
    }
}