
using NetMastery.Lab05.FileManager.BL.Dto;
using System;
using System.Collections.Generic;


namespace UI.Controllers
{
    public class DirectoryController
    {
        private readonly IDirectoryService _directoryService;

        public DirectoryController(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }

        public void Add(string path, string name, string currentPath)
        {
            if(path != null)
            {
                _directoryService.Add(path, name, currentPath);
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
                _directoryService.Add(pathFrom, pathTo, currentPath);
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

        public void Remove(string path, string currentPath)
        {
            if (path != null)
            {
                _directoryService.Remove(path, currentPath);
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
                return _directoryService.Search(pattern, path);
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

        public void ChangeWorkingDirectory(string path, string currentPath)
        {
            if (path != null)
            {
                _directoryService.ChangeWorkDirectory(currentPath, path);
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
                return _directoryService.GetInfoByCurrentPath(path);
            }
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

    }
}
