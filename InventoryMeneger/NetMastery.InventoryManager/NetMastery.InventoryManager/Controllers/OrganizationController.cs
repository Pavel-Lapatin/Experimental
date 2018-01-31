using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Bl.Exceptions;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using NetMastery.InventoryManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NetMastery.InventoryManager.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;
        int pageSize = 3;
        public OrganizationController(IOrganizationService organizationService, IMapper mapper)
        {
            _organizationService = organizationService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListOrganizations(int page = 1, string pattern = null)
        {
            var model = new OrganizationListViewModel();
            var organisations = (pattern == null
                ? _organizationService.GetAll((int)Session["Account"])
                : _organizationService.Search((int)Session["Account"], pattern));
            model.Organizations = organisations.OrderBy(x => x.Name)
                                               .Skip((page - 1) * pageSize)
                                               .Take(pageSize)
                                               .Select(item => _mapper.Map<OrganizationViewModel>(item)).ToArray();
            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = organisations.Count()
            };
            model.Pattern = pattern;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Organizations", model);
            }
            else
            {
                return View(model);
            }
        }

        [Authorize(Roles ="admin, accountant")]
        public ActionResult Edit()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("EditPartial");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin, accountant")]
        [ValidateAntiForgeryToken]
        public ActionResult Add(OrganizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                _organizationService.Add(_mapper.Map<OrganizationDto>(model));
                return RedirectToAction("Index");
            }
            catch (InventoryServiceException)
            {
                ModelState.AddModelError("", "Organization wasn't added");
                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin, accountant")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OrganizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                _organizationService.Update(_mapper.Map<OrganizationDto>(model));
                return RedirectToAction("Index");
            }
            catch (InventoryServiceException)
            {
                ModelState.AddModelError("", "Organization wasn't updated");
                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin, accountant")]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(IEnumerable<OrganizationViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var deletedItems = model.Where(x => x.IsSelected == true).ToArray();
            try
            {
                _organizationService.DeleteRange(deletedItems.Select(item => _mapper.Map<OrganizationDto>(item)));
                return RedirectToAction("Index");
            }
            catch (InventoryServiceException)
            {
                ModelState.AddModelError("", "Organizations wasn't deleted");
                return View(model);
            }
        }

        public ActionResult Search(OrganizationListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction("ListOrganisation", new {pattern = model.Pattern });
        }

        protected override void Dispose(bool disposing)
        {
            _organizationService.Dispose();
            base.Dispose(disposing);
        }
    }
}