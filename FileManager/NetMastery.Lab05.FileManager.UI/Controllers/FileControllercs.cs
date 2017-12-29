﻿using AutoMapper;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
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

        public void Upload(string destinationPath, string sourcePath)
        {
            if (IsAthenticated())
            {
                var form = new TwoPathForm(_userContext.CurrentPath, destinationPath, sourcePath);
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

        public void Download(string destinationPath, string sourcePath)
        {
            if (IsAthenticated())
            {
                var form = new TwoPathForm(_userContext.CurrentPath, destinationPath, sourcePath);
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
    

        public void Move(string destinationPath, string sourcePath)
        {
            if (IsAthenticated())
            {
                var form = new TwoPathForm(_userContext.CurrentPath, destinationPath, sourcePath);
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

        public void Remove(string destinationPath)
        {
            if (IsAthenticated())
            {
                var form = new OnePathForm(_userContext.CurrentPath, destinationPath);
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

        public void GetFleInfo(string destinationPath)
        {
            if (IsAthenticated())
            {
                var form = new OnePathForm(_userContext.CurrentPath, destinationPath);
                if (form.IsValid)
                {
                    var model = Mapper.Instance.Map<FileInfoVieModel>(_fileService.GetFileByPath(form.DestinationPath));
                    if (model == null) throw new ArgumentNullException();
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
