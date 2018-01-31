using AutoMapper;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using System.Web.Mvc;

namespace NetMastery.InventoryManager.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;
        int pageSize = 6;
        public CardController(ICardService cardService, IMapper mapper)
        {
            _cardService = cardService;
            _mapper = mapper;
        }
        // GET: Card
        public ActionResult Index()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            _cardService.Dispose();
            base.Dispose(disposing);
        }
    }
}