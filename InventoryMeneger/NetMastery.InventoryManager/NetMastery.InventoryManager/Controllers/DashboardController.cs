using System;
﻿using NetMastery.InventoryManager.Models.Dashboard;
using System.Web.Mvc;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using System.Linq;
using NetMastery.InventoryManager.Models;
using NetMastery.InventoryManager.Bl.DtoEntities;
using System.Threading.Tasks;
using AutoMapper;

namespace NetMastery.InventoryManager.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;
        int pageSize = 6;
        public DashboardController(IOrganizationService organizationService, IMapper mapper)
        {
            _organizationService = organizationService;
            _mapper = mapper;
        }


        // GET: Dashboar
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Search(DashboardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction("ListOrganisation", new { page = 1, pattern = model.SearchString });
        }

        public ActionResult ListOrganizations(int page=1, string pattern = null)
        {
             var organisation = pattern == null 
                ? _organizationService.GetAll((int)Session["Account"]) 
                : _organizationService.Search((int)Session["Account"], pattern);
            var cards = organisation
                .Select(x => _mapper.Map<OrganizationCardViewModel>(x))
                .OrderBy(x => x.Name)
                .ToList();
            var model = PageListViewModel<OrganizationCardViewModel>.CreatePage(cards, page, pageSize);
            if (Request.IsAjaxRequest())
            {
                return  PartialView("Organizations", model);
            }
            else
            {
                return  View(model);
            }
        }
    }
}