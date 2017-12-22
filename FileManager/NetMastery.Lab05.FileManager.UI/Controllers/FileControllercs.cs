using NetMastery.FileManeger.Bl.Interfaces;
using NetMastery.Lab05.FileManager.ViewModels;
using System;
using System.Collections.Generic;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class FileController : AuthenticateController
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService, AppViewModel model) : base(model)
        {
            _fileService = fileService;
        }

        public void Upload(string path, string externFilePath)
        {
            if (path != null && externFilePath != null)
            {

                _fileService.Upload(CreatePath(path), externFilePath);
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

        public void Download(string externFilePath, string path)
        {
            if (path != null && externFilePath != null)
            {
                _fileService.Upload(CreatePath(path), CreatePath(externFilePath));
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
                _fileService.Move(CreatePath(pathFrom), CreatePath(pathTo));
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
