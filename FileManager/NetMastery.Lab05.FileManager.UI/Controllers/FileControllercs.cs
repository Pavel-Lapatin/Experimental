using AutoMapper;
using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService, IUserContext context, RedirectEvent redirect) : base(context, redirect)
        {
            _fileService = fileService;
        }

        public void Upload(TwoPathForm form)
        {
            if (IsAthenticated())
            {
                form.Currentpath = GetCurrentPath();
                if (form.IsValid)
                {
                    _fileService.Upload(form.SourcePath, form.DestinationPath);
                    Console.WriteLine();
                    Console.WriteLine("File uploaded successfully");
                    Console.WriteLine();
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            } 
        }

        public void Download(TwoPathForm form)
        {
            if (IsAthenticated())
            {
                form.Currentpath = GetCurrentPath();
                if (form.IsValid)
                {
                    _fileService.Download(form.DestinationPath, form.SourcePath);
                    Console.WriteLine();
                    Console.WriteLine("File uploaded successfully");
                    Console.WriteLine();
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            }
        }
    

        public void Move(TwoPathForm form)
        {
            if (IsAthenticated())
            {
                form.Currentpath = GetCurrentPath();
                if (form.IsValid)
                {
                    _fileService.Move(form.DestinationPath, form.SourcePath);
                    Console.WriteLine();
                    Console.WriteLine("File downloaded successfully");
                    Console.WriteLine();
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            }
        }

        public void Remove(OnePathForm form)
        {
            if (IsAthenticated())
            {
                form.Currentpath = GetCurrentPath();
                if (form.IsValid)
                {
                    _fileService.Remove(form.DestinationPath);
                    Console.WriteLine();
                    Console.WriteLine("File removed successfully");
                    Console.WriteLine();
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            }
        }

        public void GetFleInfo(OnePathForm form)
        {
            if (IsAthenticated())
            {
                form.Currentpath = GetCurrentPath();
                if (form.IsValid)
                {
                    var model = Mapper.Instance.Map<FileInfoVieModel>(_fileService.GetFileByPath(form.DestinationPath));
                    if (model == null) throw new NullReferenceException();
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
