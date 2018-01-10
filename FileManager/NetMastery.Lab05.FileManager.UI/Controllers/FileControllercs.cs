using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using NetMastery.Lab05.FileManager.UI.ViewModels.Directory;

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

        public ActionResult Upload(string destinationPath, string sourcePath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new FileUploadViewModel(_userContext.CurrentPath, destinationPath, sourcePath);
                if (model.IsValid)
                {
                    _fileService.Upload(model.SourcePath, model.Path);
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult Download(string destinationPath, string sourcePath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new FileDownloadViewModel(_userContext.CurrentPath, destinationPath, sourcePath);
                if (model.IsValid)
                {
                    _fileService.Download(model.Path, model.SourcePath);
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }
    

        public ActionResult Move(string destinationPath, string sourcePath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new FileMoveViewModel(_userContext.CurrentPath, destinationPath, sourcePath);
                if (model.IsValid)
                {
                    _fileService.Move(model.Path, model.SourcePath);
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult Remove(string destinationPath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new FileRemoveViewModel(_userContext.CurrentPath, destinationPath);
                if (model.IsValid)
                {
                    _fileService.Remove(model.Path);
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }

        public ActionResult GetFileInfo(string destinationPath)
        {
            if (_userContext.IsAuthenticated)
            {
                var model = new FileInfoVieModel(_userContext.CurrentPath, destinationPath);

                if (model.IsValid)
                {
                    model.File =_fileService.GetFileByPath(model.Path);
                }
                return new ViewResult(model);
            }
            return new RedirectResult(typeof(LoginController), nameof(LoginController.SigninGet), null);
        }
    }
}
