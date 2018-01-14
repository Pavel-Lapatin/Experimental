using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Helpers;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using NetMastery.Lab05.FileManager.UI.ViewModels.Directory;
using NetMastery.Lab05.FileManager.UI.Views;
using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;


        public FileController(IFileService fileService,
                              IUserContext context) : base(context)
        {
            _fileService = fileService;
        }

        public ActionResult Upload(string pathToFile, string pathToStorage)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new TwoPathViewModel(pathToFile, pathToStorage);
                if (model.IsValid)
                {
                    var absolutePathToFile = model.Path.CreatePath(_userContext.CurrentPath);
                    var virtualPathToStorage = model.SecondPath.CreatePath(_userContext.CurrentPath);
                    _fileService.Upload(absolutePathToFile, virtualPathToStorage);
                    return new ViewResult(new InfoView("File uploaded successfully"));
                }
                return new ViewResult(new ErrorView(model.Errors));
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult Download(string pathToFile, string pathToStorage)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new TwoPathViewModel(pathToFile, pathToStorage);
                if (model.IsValid)
                {
                    var absolutePathToFile = model.Path.CreatePath(_userContext.CurrentPath);
                    var virtualPathToStorage = model.SecondPath.CreatePath(_userContext.CurrentPath);
                    _fileService.Download(absolutePathToFile, virtualPathToStorage);
                    return new ViewResult(new InfoView("File downloaded successfully"));
                }
                return new ViewResult(new ErrorView(model.Errors)); ;
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
                    _fileService.Move(virtualPathFrom, virtualPathTo);
                    return new ViewResult(new InfoView("File moved successfully"));
                }
                return new ViewResult(new ErrorView(model.Errors));
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult Remove(string destinationPath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new PathViewModel(destinationPath);
                if (model.IsValid)
                {
                    var virtualPath = model.Path.CreatePath(_userContext.CurrentPath);
                    _fileService.Remove(virtualPath);
                    return new ViewResult(new InfoView("File removed successfully"));
                }
                return new ViewResult(new ErrorView(model.Errors));
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult GetFileInfo(string destinationPath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new PathViewModel(destinationPath);
                if (model.IsValid)
                {
                    var virtualPath = model.Path.CreatePath(_userContext.CurrentPath);
                    var result =_fileService.GetFileByPath(virtualPath);
                    return new ViewResult(new FileInfoView(result));
                }
                return new ViewResult(new ErrorView(model.Errors));
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }
    }
}
