using NetMastery.InventoryManager.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace NetMastery.InventoryManager.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboar
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var organisation

            return View();
        }
        public ActionResult OrganizationCard()
        {
            var card = new OrganizationCard { Name = "ООО \"Тоннельновация\"",
                Address = "Республика Беларусь, г. Минск, ул. Бельского, офис 121",
                Email = "zavod@gmail.com",
                Image = null,
                Phone = "+375292615084"};

            return View(card);
        }
    }
}