using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Helpers;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using NetMastery.Lab05.FileManager.UI.ViewModels.Directory;
using NetMastery.Lab05.FileManager.UI.Views;
using System.Linq;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly IDirectoryService _directoryService;
        
        public DirectoryController(IDirectoryService directoryService, 
                                   IUserContext context
                                   ) : base(context)
        {
            _directoryService = directoryService;
        }

        public ActionResult Add(string path, string name)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new DirectoryAddViewModel(path, name);
                if(model.IsValid)
                {
                    _directoryService.Add(model.Path.CreatePath(_userContext.CurrentPath), model.Name);
                    return new ViewResult(new InfoView("Directory successfully added"));
                }
                return new ViewResult(new ErrorView(model.Errors));
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult ShowContent(string path)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new PathViewModel(path);
                if (model.IsValid)
                {
                    var virtualPath = model.Path.CreatePath(_userContext.CurrentPath);
                    var result = _directoryService.ShowContent(virtualPath);
                    return new ViewResult(new ContentView(result));
                }
                return new ViewResult(new ErrorView(model.Errors));
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult Move(string pathFrom, string pathTo)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new TwoPathViewModel(pathFrom, pathTo);
                if (model.IsValid)
                {
                    var virtualPathFrom = model.Path.CreatePath(_userContext.CurrentPath);
                    var virtualPathTo = model.SecondPath.CreatePath(_userContext.CurrentPath);
                    _directoryService.Move(virtualPathFrom, virtualPathTo);
                    _userContext.CurrentPath = _userContext.RootDirectory;
                    return new ViewResult(new InfoView("Directory successfully moved"));
                }
                return new ViewResult(new ErrorView(model.Errors));
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);      
        }

        public ActionResult Remove(string path)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new PathViewModel(path);
                if (model.IsValid)
                {
                    var virtualPath = model.Path.CreatePath(_userContext.CurrentPath);
                    _directoryService.Remove(virtualPath);
                    _userContext.CurrentPath = _userContext.RootDirectory;
                    return new ViewResult(new InfoView("Directory successfully removed"));
                }
                return new ViewResult(new ErrorView(model.Errors));
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult ChangeWorkingDirectory(string path)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new PathViewModel(path);
                if (model.IsValid)
                {
                    var virtualPath = model.Path.CreatePath(_userContext.CurrentPath);
                    _userContext.CurrentPath = _directoryService.GetInfoByPath(virtualPath).FullPath;
                    return null;
                }
                return new ViewResult(new ErrorView(model.Errors));
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult GetDirectoryInfo(string destinationPath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new PathViewModel(destinationPath);
                if (model.IsValid)
                {
                    var virtualPath = model.Path.CreatePath(_userContext.CurrentPath);
                    var result = _directoryService.GetInfoByPath(virtualPath);
                    return new ViewResult(new DirectoryInfoView(result));
                }
                return new ViewResult(new ErrorView(model.Errors));
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult Search(string path, string pattern)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new DirectorySearchVIewModel(path, pattern);
                if(model.IsValid)
                {
                    var virtualPath = model.Path.CreatePath(_userContext.CurrentPath);
                    var result = _directoryService.Search(virtualPath, model.Pattern).ToList();
                    return new ViewResult(new SearchView(result));
                }
                return new ViewResult(new ErrorView(model.Errors));
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }             
    }
}
