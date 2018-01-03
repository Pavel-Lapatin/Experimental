using AutoMapper;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;
using System.Diagnostics;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public FileController(IFileService fileService,
                              IUserContext context,
                              IMapper mapper,
                              RedirectEvent redirect) : base(context, redirect)
        {
            _fileService = fileService;
            _mapper = mapper;
        }

        public void Upload(string destinationPath, string sourcePath)
        {
            if (IsAthenticated())
            {
                var form = new TwoPathForm(_userContext.CurrentPath, destinationPath, sourcePath);
                if (form.IsValid)
                {
                    _fileService.Upload(form.SourcePath, form.DestinationPath);
                    Debug.WriteLine("File uploaded successfully");
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            } 
        }

        public void Download(string destinationPath, string sourcePath)
        {
            if (IsAthenticated())
            {
                var form = new TwoPathForm(_userContext.CurrentPath, destinationPath, sourcePath);
                if (form.IsValid)
                {
                    _fileService.Download(form.DestinationPath, form.SourcePath);
                    Debug.WriteLine("File downloaded successfully");
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            }
        }
    

        public void Move(string destinationPath, string sourcePath)
        {
            if (IsAthenticated())
            {
                var form = new TwoPathForm(_userContext.CurrentPath, destinationPath, sourcePath);
                if (form.IsValid)
                {
                    _fileService.Move(form.DestinationPath, form.SourcePath);
                    Debug.WriteLine("File downloaded successfully");
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            }
        }

        public void Remove(string destinationPath)
        {
            if (IsAthenticated())
            {
                var form = new OnePathForm(_userContext.CurrentPath, destinationPath);
                if (form.IsValid)
                {
                    _fileService.Remove(form.DestinationPath);
                    Debug.WriteLine("File removed successfully");
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            }
        }

        public void GetFleInfo(string destinationPath)
        {
            if (IsAthenticated())
            {
                var form = new OnePathForm(_userContext.CurrentPath, destinationPath);
                if (form.IsValid)
                {
                    var model =_mapper.Map<FileInfoVieModel>(_fileService.GetFileByPath(form.DestinationPath));
                    model.RenderViewModel();
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            }
        }
    }
}
