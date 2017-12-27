using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Helpers;
using System;


namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly IDirectoryService _directoryService;

        public DirectoryController(IDirectoryService directoryService, IUserContext context) : base(context)
        {
            _directoryService = directoryService;
            
        }

        public void Add(string path, string name)
        {
            if(string.IsNullOrEmpty(path) || string.IsNullOrEmpty(name))
            {    
                _directoryService.Add(UIHelpers.CreatePath(path, _userContext.CurrentPath), name);
                Console.WriteLine();
                Console.WriteLine("Directorry created successfully");
                Console.WriteLine();
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't empty string");
            }
        }

        public void List(string path)
        {
            if (path != null)
            {
                Console.WriteLine();
                var directory = _directoryService.GetInfoByPath(UIHelpers.CreatePath(path, _userContext.CurrentPath));

                Console.WriteLine("<---Directories:--->");
                foreach (var item in directory.ChildrenDirectories)
                {
                    Console.WriteLine(item.Name);
                }
                Console.WriteLine();
                Console.WriteLine("<---Files:--->");
                foreach (var item in directory.Files)
                {
                    Console.WriteLine(item.Name+item.Extension);
                }
                Console.WriteLine();
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't empty string");
            }
        }

        public void Move(string pathFrom, string pathTo)
        {
            if (string.IsNullOrEmpty(pathFrom) || string.IsNullOrEmpty(pathTo))
            {
                _directoryService.Move(UIHelpers.CreatePath(pathFrom, _userContext.CurrentPath), UIHelpers.CreatePath(pathTo, _userContext.CurrentPath));
                Console.WriteLine();
                Console.WriteLine("Directorry moved successfully");
                Console.WriteLine();
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't empty string");
            }
        }

        public void Remove(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                _directoryService.Remove(UIHelpers.CreatePath(path, _userContext.CurrentPath));
                Console.WriteLine();
                Console.WriteLine("Directorry removed successfully");
                Console.WriteLine();
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't empty string");
            }
        }

        
        public void ChangeWorkingDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                _userContext.CurrentPath = _directoryService.ChangeWorkDirectory(UIHelpers.CreatePath(path, _userContext.CurrentPath));
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't empty string");
            }
        }

        public void GetDirectoryInfo(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                var directoryInfo = _directoryService.GetInfoByPath(UIHelpers.CreatePath(path, _userContext.CurrentPath));
                if (directoryInfo != null)
                {

                    Console.WriteLine();
                    Console.WriteLine("Login: " + _userContext.Login);
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
            else
            {
                throw new NullReferenceException("The path of the directory couldn't empty string");
            }
        }

        public void Search(string path, string pattern)
        {
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine();
                Console.WriteLine($"There are following results for pattern {pattern}:");
                foreach (var item in _directoryService.Search(UIHelpers.CreatePath(path, _userContext.CurrentPath), pattern))
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't empty string");
            }
        }
    }
}
