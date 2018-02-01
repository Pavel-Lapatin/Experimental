using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Bl.Exceptions;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using NetMastery.InventoryManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMastery.InventoryManager.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly ISubdivisionService _subdivisionService;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        int pageSize = 3;
        public OrganizationController(IOrganizationService organizationService,
                                      ISubdivisionService subdivisionService,
                                      IPersonService personService,
                                      IMapper mapper)
        {
            _organizationService = organizationService;
            _subdivisionService = subdivisionService;
            _personService = personService;
            _mapper = mapper;
        }

        public ActionResult Index(OrganizationListViewModel model)
        {
            return View(model);
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
        public ActionResult Edit(EditOrganizationViewModel model)
        {
            model.Subdivisions = _subdivisionService.GetAll(model.Organization.OrganizationId);
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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var model = new OrganizationViewModel();
                //attach the uploaded image to the object before saving to Database
                model.Image = new byte[file.ContentLength];
                return View("Add", model);
            }
            return View("Add");
        }
        [Authorize(Roles = "admin")]
        public ActionResult Add()
        {
            return View();
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
        [HttpPost]
        public ActionResult Search(OrganizationListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction("Index", new {pattern = model.Pattern });
        }

        protected override void Dispose(bool disposing)
        {
            _organizationService.Dispose();
            base.Dispose(disposing);
        }
    }
}