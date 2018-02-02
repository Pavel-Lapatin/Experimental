using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Bl.Servicies.Interfaces;
using NetMastery.InventoryManager.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMastery.InventoryManager.Controllers
{
    public class UserController : Controller
    {
        
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        int pageSize = 3;
        public UserController(IUserService userService,
                              IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        // GET: User
        [Authorize(Roles = "admin")]
        public ActionResult Index(UserListViewModel model)
        {
            if(model == null)
            {
                model.Users = _userService.GetAll((int)Session["Account"])
                    .Select(item => _mapper.Map<UserViewModel>(item));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]

        public ActionResult Add(UserViewModel model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                model.AccountId = (int)Session["Account"];
                if (file != null)
                {
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        model.Image = binaryReader.ReadBytes(file.ContentLength);
                    }
                }
                _organizationService.Add(_mapper.Map<OrganizationDto>(model));
                return RedirectToAction("Index");
            }
            catch (InventoryServiceException)
            {
                ModelState.AddModelError("", "Organization wasn't added");
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(OrganizationListViewModel model)
        {
            var deletedItems = model.Organizations.Where(x => x.IsSelected == true)?.ToArray();
            try
            {
                if (deletedItems != null)
                {
                    _organizationService.DeleteRange(deletedItems.Select(item => _mapper.Map<OrganizationDto>(item)));
                }
                return RedirectToAction("Index");
            }
            catch (InventoryServiceException)
            {
                ModelState.AddModelError("", "Organizations wasn't deleted");
                return View("ListOrganizations", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(OrganizationListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction("Index", new { pattern = model.Pattern });
        }

        protected override void Dispose(bool disposing)
        {
            _organizationService.Dispose();
            base.Dispose(disposing);
        }
    }
}