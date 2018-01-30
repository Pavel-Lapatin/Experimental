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

        public ActionResult ListOrganizations(int page=1)
        {
            if(Session["Account"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var organisation = _organizationService.GetAll((int)Session["Account"]);
            var cards = organisation
                .Select(x => _mapper.Map<OrganizationCardViewModel>(x))
                .OrderBy(x => x.Name)
                .ToList(); ;
            var model = PageListViewModel<OrganizationCardViewModel>.CreatePage(cards, page, pageSize);
            if (Request.IsAjaxRequest())
            {
                return  PartialView("Organizations", model);
            }
            else
            {
                return  PartialView("Organizations", model);
            }
        }
    }
}