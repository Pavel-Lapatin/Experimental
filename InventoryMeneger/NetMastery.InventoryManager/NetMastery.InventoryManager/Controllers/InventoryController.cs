using AutoMapper;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMastery.InventoryManager.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;
        public InventoryController(IInventoryService inventoryService, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }
        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            _inventoryService.Dispose();
            base.Dispose(disposing);
        }
    }
}