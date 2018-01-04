using AutoMapper;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.Forms;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly IDirectoryService _directoryService;
        private readonly IMapper _mapper;
        

        public DirectoryController(IDirectoryService directoryService, 
                                   IUserContext context, 
                                   IMapper mapper, 
                                   RedirectEvent redirect) : base(context, redirect)
        {
            _directoryService = directoryService;
            _mapper = mapper;
        }

        public void Add(string destinationPath, string name)
        {
            if (IsAthenticated())
            {
                var form = new AddDirectoryForm(_userContext.CurrentPath, destinationPath, name);
                if(form.IsValid)
                {
                    _directoryService.Add(form.DestinationPath, form.Name);
                    Debug.WriteLine("Directorry created successfully");
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
                    var result = _directoryService.ShowContent(form.DestinationPath).ToArray();
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
                    _userContext.CurrentPath = _userContext.RootDirectory;
                    Debug.WriteLine("Directorry moved successfully");
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
                    _userContext.CurrentPath = _userContext.RootDirectory;
                    Debug.WriteLine("Directorry removed successfully");
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
                    _userContext.CurrentPath = _directoryService.GetInfoByPath(form.DestinationPath).FullPath; 
                    Debug.WriteLine("Work directorry changed successfully");
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
                    var model = _mapper.Map<DirectoryInfoViewModel>(_directoryService.GetInfoByPath(form.DestinationPath));
                    model.RenderViewModel();
                }
                else
                {
                    form.RenderErrors();
                }
                GetCommandRedirect();
            }
        }

        public void Search(string destinationPath, string pattern)
        {
            if (IsAthenticated())
            {
                var form = new SearchDirectoryForm(_userContext.CurrentPath, destinationPath, pattern);
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
