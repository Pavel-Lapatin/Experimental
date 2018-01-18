
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using Serilog;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.DAL.Repository;
using System;
using NetMastery.Lab05.FileManager.UI;
using NetMastery.Lab05.FileManager.Bl.Helpers;

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public class DirectoryService : BusinessService, IDirectoryService
    {
        #region Constructors

        public DirectoryService(IUnitOfWork unitOfWork, 
                                IMapper mapper, 
                                IUserContext userContext) : base(unitOfWork, mapper, userContext)
        {
        }
        #endregion

        #region DirectoryServiceAPI

        public void Add(string path, string name)
        {
            if (path == null || name == null)
            {
                Log.Logger.Debug("DirectoryService-->Add-->Input is null");
                throw new ArgumentNullException();
            }
            if(!path.HasAccessToVirtualStorage(_userContext.RootDirectory))
            {
                throw new BusinessException("Access is denied");
            }

            var currentDirectory = _unitOfWork
                 .Get<IDirectoryRepository>()
                 .FindByPath(path)
                 ?? throw new BusinessException($"Directory with \"{path}\" doesn't exist");

            var newDirectory = CreateNewDirectory(path, name);
            newDirectory.ParentDirectory = currentDirectory;

            try
            {
                (_unitOfWork.GetFileSystemManager<IDirectoryManager>()).AddFolder(path, name);
                _unitOfWork.Get<IDirectoryRepository>().Add(newDirectory);
                _unitOfWork.Commit();
            }
            catch (System.Data.DataException e)
            {
                Log.Logger.Debug(e.Message);
                _unitOfWork.GetFileSystemManager<IDirectoryManager>().Remove(path + '\\' + name);
                throw;
            }
        }

        public void Move(string pathFrom, string pathTo)
        {
            if (pathFrom == null || pathTo == null)
            {
                Log.Logger.Debug("DirectoryService-->Move-->Input is null");
                throw new ArgumentNullException();
            }

            if (!pathFrom.HasAccessToVirtualStorage(_userContext.RootDirectory)
                || !pathTo.HasAccessToVirtualStorage(_userContext.RootDirectory))
            {
                throw new BusinessException("Access is denied");
            }

            var currentDirectoryFrom = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindByPathEagerLoadingParentDirectory(pathFrom)
                ?? throw new BusinessException($"Directory with \"{pathFrom}\" doesn't exist");

            var currentDirectoryTo = _unitOfWork
                 .Get<IDirectoryRepository>()
                 .FindByPath(pathTo)
                 ?? throw new BusinessException($"Directory with \"{pathTo}\" doesn't exist");

            if (currentDirectoryTo.FullPath.Contains(currentDirectoryFrom.FullPath))
            {
                throw new BusinessException("Distanation directory is subfolder of source directory");
            }

            ChangeFullPath(currentDirectoryFrom.FullPath, 
                currentDirectoryFrom.ParentDirectory.FullPath,
                pathTo);

            currentDirectoryFrom.ParentDirectory = currentDirectoryTo;
            currentDirectoryFrom.ModificationDate = DateTime.Now;
            try
            {
                _unitOfWork.GetFileSystemManager<IDirectoryManager>().Move(pathTo, pathFrom);
                _unitOfWork.Commit();
            }
            catch (System.Data.DataException e)
            {
                Log.Logger.Debug(e.Message);
                _unitOfWork.GetFileSystemManager<IDirectoryManager>().Move(pathFrom, pathTo);
                throw;
            }
        } 

        public void Remove(string path)
        {
            if (path == null)
            {
                Log.Logger.Debug("DirectoryService-->Remove-->Input is null");
                throw new ArgumentNullException();
            }
            if (!path.HasAccessToVirtualStorage(_userContext.RootDirectory))
            {
                throw new BusinessException("Access is denied");
            }

            var currentDirectory = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindByPathEagerLoadingFiles(path)
                ?? throw new BusinessException($"Directory with \"{path}\" doesn't exist");

            RecursiveRemove(currentDirectory);
            try
            {
                _unitOfWork.GetFileSystemManager<IDirectoryManager>().Remove(path);
                _unitOfWork.Commit();
            }
            
            catch (System.Data.DataException e)
            {
                Log.Logger.Debug(e.Message);
                throw;
            }      
        }

        public IEnumerable<string> Search(string path, string pattern)
        {
            if (path == null || pattern == null)
            {
                Log.Logger.Debug("DirectoryService-->Search-->Input is null");
                throw new ArgumentNullException();
            }
            if (!path.HasAccessToVirtualStorage(_userContext.RootDirectory))
            {
                throw new BusinessException("Access is denied");
            }

            var currentDirectory = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindByPathEagerLoadingFull(path)
                ?? throw new BusinessException($"Directory with \"{path}\" doesn't exist");

            IList<string> results = new List<string>();
            RecursiveSearch(pattern, results, currentDirectory);
            return results;
        }

        public IDictionary<string, IList<string>> ShowContent(string path)
        {
            if (path == null)
            {
                Log.Logger.Debug("DirectoryService-->IDictionary-->Input is null");
                throw new ArgumentNullException();
            }
            if (!path.HasAccessToVirtualStorage(_userContext.RootDirectory))
            {
                throw new BusinessException("Access is denied");
            }

            var directory = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindByPathEagerLoadingFull(path)
                ?? throw new BusinessException($"Directory with \"{path}\" doesn't exist");

            var result = new Dictionary<string, IList<string>>();
            IList<string> files = directory.Files.Select(x => x.Name).ToList();
            IList<string> directories = directory.ChildrenDirectories.Select(x => x.Name).ToList();
            result.Add("Directories", directories);
            result.Add("Files", files);
            return result;
        }

        public DirectoryStructureDto GetInfoByPath(string path)
        {
            if (path == null)
            {
                Log.Logger.Debug("DirectoryService-->GetInfoByPath-->Input is null");
                throw new ArgumentNullException();
            }
            if (!path.HasAccessToVirtualStorage(_userContext.RootDirectory))
            {
                throw new BusinessException("Access is denied");
            }

            return _mapper.Map<DirectoryStructureDto>(_unitOfWork
                 .Get<IDirectoryRepository>()
                 .FindByPath(path)
                 ?? throw new BusinessException($"Directory with \"{path}\" doesn't exist"));
        }

        #endregion

        private DirectoryStructure CreateNewDirectory(string path, string name)
        {
            var newDirectory = new DirectoryStructureDto
            {
                Name = name,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                FullPath = path + "\\" + name,
            };
            
            if (_unitOfWork
                 .Get<IDirectoryRepository>()
                 .FindByPath(newDirectory.FullPath) != null)
            {
                throw new BusinessException($"Directory with \"{path}\" has already existed");
            }
            return _mapper.Map<DirectoryStructure>(newDirectory);
        }

        private void RecursiveSearch(string pattern, IList<string> results, DirectoryStructure directory)
        {          
            if (directory.ChildrenDirectories != null)
            {
                foreach (var child in directory.ChildrenDirectories)
                {
                    RecursiveSearch(pattern, results, child);
                }
            }
            if(directory.Name.Contains(pattern))
            {
                results.Add(directory.FullPath);
            }
            foreach (var file in directory.Files)
            {
                if (file.Name.Contains(pattern))
                {
                    results.Add(directory.FullPath + "\\" + file.Name);
                }
            }
        }

        private void RecursiveRemove(DirectoryStructure rootDirectory)
        {
            if (rootDirectory.ChildrenDirectories != null)
            {
                var children = rootDirectory.ChildrenDirectories.ToList();
                for (int i = 0; i < children.Count; i++)
                {
                    RecursiveRemove(children[i]);
                }
            }
            _unitOfWork.Get<IFileRepository>().RemoveRange(rootDirectory.Files);  
            _unitOfWork.Get<IDirectoryRepository>().Remove(rootDirectory);
        }

        private void ChangeFullPath(string pathFrom, string pattern, string pathTo)
        {
            var dir = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindDirectoriesWhichContainPath(pathFrom).ToArray();
            for (int i = 0; i < dir.Length; i++)
            {
                dir[i].FullPath = dir[i].FullPath.Replace(pattern, pathTo);
            }
        }
    }
}
