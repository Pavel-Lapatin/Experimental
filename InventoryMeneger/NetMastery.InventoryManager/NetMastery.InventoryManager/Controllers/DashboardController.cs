using System.Web.Mvc;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using AutoMapper;

namespace NetMastery.InventoryManager.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;
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
    }
}