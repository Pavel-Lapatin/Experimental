using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using NetMastery.Lab05.FileManager.UI.ViewModels.Directory;
using System.Linq;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly IDirectoryService _directoryService;
        

        public DirectoryController(IDirectoryService directoryService, 
                                   IUserContext context) : base(context)
        {
            _directoryService = directoryService;
        }

        public ActionResult Add(string destinationPath, string name)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new DirectoryAddViewModel(_userContext.CurrentPath, destinationPath, name);
                if(model.IsValid)
                {
                    _directoryService.Add(model.Path, model.Name);
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult ShowContent(string destinationPath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new DirectoryShowContentViewModel(_userContext.CurrentPath, destinationPath);
                if (model.IsValid)
                {
                    model.Data = _directoryService.ShowContent(model.Path);
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult Move(string destinationPath, string sourcePath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new DirectoryMoveViewModel(_userContext.CurrentPath, destinationPath, sourcePath);
                if (model.IsValid)
                {
                    _directoryService.Move(model.Path, model.SourcePath);
                    _userContext.CurrentPath = _userContext.RootDirectory;
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);      
        }

        public ActionResult Remove(string destinationPath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new DirectoryRemoveViewModel(_userContext.CurrentPath, destinationPath);
                if (model.IsValid)
                {
                    _directoryService.Remove(model.Path);
                    _userContext.CurrentPath = _userContext.RootDirectory;
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult ChangeWorkingDirectory(string destinationPath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new DirectoryViewModel(_userContext.CurrentPath, destinationPath);
                if (model.IsValid)
                {
                    _userContext.CurrentPath = _directoryService.GetInfoByPath(model.Path).FullPath; 
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult GetDirectoryInfo(string destinationPath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new DirectoryInfoViewModel(_userContext.CurrentPath, destinationPath);
                if (model.IsValid)
                {
                    model.Directory = _directoryService.GetInfoByPath(model.Path);
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult Search(string destinationPath, string pattern)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new DirectorySearchVIewModel(_userContext.CurrentPath, destinationPath, pattern);
                if(model.IsValid)
                {
                    model.Data = _directoryService.Search(model.Path, model.Pattern).ToList();
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }             
    }
}
