using AutoMapper;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMastery.PersonManager.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        public PersonController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }
        // GET: Person
        public ActionResult Index()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            _personService.Dispose();
            base.Dispose(disposing);
        }
    }
}