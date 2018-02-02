using AutoMapper;
using NetMastery.InventoryManager.Bl.DtoEntities;
using NetMastery.InventoryManager.Bl.Exceptions;
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
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private int pageSize= 2;
        public UserController(IUserService userService,
                              IRoleService roleService,
                              IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }

        // GET: User
        [Authorize(Roles = "admin")]
        public ActionResult Index(UserListViewModel model, int page = 1)
        {
            model = model ?? new UserListViewModel();
            var users = (model.SearchParametrs == null
                ? _userService.GetAll((int)Session["Account"])
                : _userService.SearchByPattern((int)Session["Account"], model.SearchParametrs.Name,
                                                                        model.SearchParametrs.Email,
                                                                        model.SearchParametrs.Phone,
                                                                        model.SearchParametrs.Role));

            model.Users = users.OrderBy(x => x.UserName)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .Select(item => _mapper.Map<UserViewModel>(item))
                               .ToArray();
            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = users.Count()
            };
            model.PagingInfo.CurrentPage = page;
            ViewData["Roles"] = _roleService.GetAll().Select(item => _mapper.Map<SelectListItem>(item));
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserListViewModel model)
        {
            ViewData["Roles"] = _roleService.GetAll().Select(item => _mapper.Map<SelectListItem>(item));
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
                _userService.Register(_mapper.Map<UserDto>(model),model.Password);
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
        public ActionResult Update(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                _userService.Update(_mapper.Map<UserDto>(model));
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
        public ActionResult Remove(UserViewModel model)
        {
            try
            {
                _userService.Delete(_mapper.Map<UserDto>(model));

                return RedirectToAction("Index");
            }
            catch (InventoryServiceException)
            {
                ModelState.AddModelError("", "Organizations wasn't deleted");
                return View("ListOrganizations", model);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _userService.Dispose();
            base.Dispose(disposing);
        }
    }
}