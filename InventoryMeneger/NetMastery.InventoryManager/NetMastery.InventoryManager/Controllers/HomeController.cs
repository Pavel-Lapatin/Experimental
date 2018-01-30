﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMastery.InventoryManager.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Body()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("BodyPartial");
            }
            else
            {
                return View();
            }
        }
    }
}