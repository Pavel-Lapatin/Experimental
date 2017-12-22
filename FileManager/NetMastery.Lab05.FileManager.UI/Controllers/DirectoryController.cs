﻿using NetMastery.FileManeger.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.ViewModels;
using System;
using System.Collections.Generic;


namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class DirectoryController : AuthenticateController
    {
        private readonly IDirectoryService _directoryService;

        public DirectoryController(IDirectoryService directoryService, AppViewModel model) : base(model)
        {
            _directoryService = directoryService;
            
        }

        public void Add(string path, string name)
        {
            if(path != null && name != null )
            {
                
                _directoryService.Add(CreatePath(path), name);
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

        public void Move(string pathFrom, string pathTo, string currentPath)
        {
            if (pathFrom != null && pathTo != null)
            {
                _directoryService.Move(CreatePath(pathFrom), CreatePath(pathTo));
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

        public void Remove(string path)
        {
            if (path != null)
            {
                _directoryService.Remove(CreatePath(path));
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

        public IEnumerable<string> Search(string pattern, string path)
        {
            if (path != null)
            {
                return _directoryService.Search(pattern, CreatePath(path));
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

        public void ChangeWorkingDirectory(string path)
        {
            if (path != null)
            {
                _directoryService.ChangeWorkDirectory(CreatePath(path));
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

        public DirectoryStructureDto GetDirectoryByPath(string path)
        {
            if (path != null)
            {
                return _directoryService.GetInfoByPath(CreatePath(path));
            }
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

    }
}
