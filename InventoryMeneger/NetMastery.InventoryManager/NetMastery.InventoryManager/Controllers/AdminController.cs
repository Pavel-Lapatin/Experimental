using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMastery.InventoryManager.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult CreateOrganization()
        {
            return View();
        }

        public ActionResult FindOrganization()
        {
            return View();
        }
    }
}