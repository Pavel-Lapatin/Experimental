using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.ViewModel;
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

        public void List(string path)
        {
            if (path != null)
            {
                Console.WriteLine();
                var directory = _directoryService.GetInfoByPath(CreatePath(path));

                Console.WriteLine("Directories:");
                foreach (var item in directory.ChildrenDirectories)
                {
                    Console.WriteLine(item.Name);
                }
                Console.WriteLine();
                Console.WriteLine("Files");
                foreach (var item in directory.Files)
                {
                    Console.WriteLine(item.Name);
                }
                Console.WriteLine();
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

        public void Move(string pathFrom, string pathTo)
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

        
        public void ChangeWorkingDirectory(string path)
        {
            if (path != null)
            {
                Model.CurrentPath = _directoryService.ChangeWorkDirectory(CreatePath(path));
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

        public void GetDirectoryInfo(string path)
        {
            var directoryInfo = _directoryService.GetInfoByPath(CreatePath(path));
            if (directoryInfo != null)
            {

                Console.WriteLine();
                Console.WriteLine("Login: " + Model.AuthenticatedLogin);
                Console.WriteLine("Path: " + directoryInfo.FullPath);
                Console.WriteLine("CreationDate: " + directoryInfo.CreationDate.ToString("yy-MM-dd"));
                Console.WriteLine("ModificationDate: " + directoryInfo.ModificationDate.ToString("yy-MM-dd"));
                Console.WriteLine("Size: " + directoryInfo.DirectorySize + " kB");
                Console.WriteLine();
            }
            else
            {
                throw new NullReferenceException($"There is no such folder");
            }
        }

        public void Search(string path, string pattern)
        {
            if (path != null)
            {
                Console.WriteLine();
                foreach (var item in _directoryService.Search(CreatePath(path), pattern))
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }
    }
}
