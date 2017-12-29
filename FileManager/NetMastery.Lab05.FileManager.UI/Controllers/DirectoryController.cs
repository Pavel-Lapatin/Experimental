﻿using AutoMapper;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;
using System.Linq;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly IDirectoryService _directoryService;
        

        public DirectoryController(IDirectoryService directoryService, IUserContext context, RedirectEvent redirect) : base(context, redirect)
        {
            _directoryService = directoryService;
            
        }

        public void Add(string destinationPath, string name)
        {
            if (IsAthenticated())
            {
                var form = new AddDirectoryForm(_userContext.CurrentPath, destinationPath, name);
                if(form.IsValid)
                {
                    _directoryService.Add(form.DestinationPath, form.Name);
                    Console.WriteLine();
                    Console.WriteLine("Directorry created successfully");
                    Console.WriteLine();
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            }
        }

        public void List(string destinationPath)
        {
            if (IsAthenticated())
            {

                var form = new OnePathForm(_userContext.CurrentPath, destinationPath);
                if (form.IsValid)
                {
                    var result = _directoryService.List(form.DestinationPath).ToArray();
                    if (result == null) throw new ArgumentNullException();
                    var model = new DirectoryListViewModel(result, _userContext.CurrentPath);
                    model.RenderViewModel();
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
                    _directoryService.Move(form.DestinationPath, form.SourcePath);
                    Console.WriteLine();
                    Console.WriteLine("Directorry moved successfully");
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
                    _directoryService.Remove(form.DestinationPath);
                    Console.WriteLine();
                    Console.WriteLine("Directorry removed successfully");
                    Console.WriteLine();
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            }         
        }


        
        public void ChangeWorkingDirectory(string destinationPath)
        {
            if (IsAthenticated())
            {
                var form = new OnePathForm(_userContext.CurrentPath, destinationPath);
                if (form.IsValid)
                {
                    _userContext.CurrentPath = _directoryService.ChangeWorkDirectory(form.DestinationPath); 
                    Console.WriteLine();
                    Console.WriteLine("Work directorry changed successfully");
                    Console.WriteLine();
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            } 
        }

        public void GetDirectoryInfo(string destinationPath)
        {
            if (IsAthenticated())
            {
                var form = new OnePathForm(_userContext.CurrentPath, destinationPath);
                if (form.IsValid)
                {
                    var model = Mapper.Instance.Map<DirectoryInfoViewModel>(_directoryService.GetInfoByPath(form.DestinationPath));
                    model.RenderViewModel();
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            }
        }

        public void Search(string destinationPath, string sourcePath)
        {
            if (IsAthenticated())
            {
                var form = new SearchDirectoryForm(_userContext.CurrentPath, destinationPath, sourcePath);
                if (form.IsValid)
                {
                    
                    var results =_directoryService.Search(form.DestinationPath, form.Pattern);
                    if (results == null) throw new ArgumentNullException();
                    var model = new DirectorySearchVIewModel(results.ToArray());
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
