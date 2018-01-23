using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMastery.InventoryManager.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}