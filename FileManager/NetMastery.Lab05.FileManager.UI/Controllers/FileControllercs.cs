using NetMastery.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.ViewModel;
using System;
using System.Collections.Generic;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService, IUserContext context) : base(context)
        {
            _fileService = fileService;
        }

        public void Upload(string path, string externFilePath)
        {
            if (path != null && externFilePath != null)
            {
                _fileService.Upload(CreatePath(path), CreatePath(externFilePath));
            }
            else
            {
                throw new NullReferenceException("The path mustn't be empty string");
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
                _fileService.Remove(CreatePath(path));
            }
            else
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }

        

        

        public FileStructureDto GetFileByPath(string path)
        {
            if (path != null)
            {
                return _fileService.GetFileByPath(path);
            }
            {
                throw new NullReferenceException("The path of the directory couldn't be found");
            }
        }




    }
}
