
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

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public class DirectoryService : IDirectoryService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #region Constructors

        public DirectoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region DirectoryServiceAPI

        public void Add(string path, string name)
        {
            if (path == null || name == null)
            {
                throw new ServiceArgumentNullException();
            }

            var newDirectory = CreateNewDirectory(path, name);
            var currentDirectory = _unitOfWork
                 .Get<IDirectoryRepository>()
                 .FindByPath(path)
                 ?? throw new DirectoryDoesNotExistException();
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
                throw new ServiceArgumentNullException();
            }

            var currentDirectoryFrom = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindByPathEagerLoadingParentDirectory(pathFrom) 
                ?? throw new DirectoryDoesNotExistException();

            var currentDirectoryTo = _unitOfWork
                 .Get<IDirectoryRepository>()
                 .FindByPath(pathTo)
                 ?? throw new DirectoryDoesNotExistException();

            if (currentDirectoryTo.FullPath.Contains(currentDirectoryFrom.FullPath))
            {
                throw new ServiceArgumentException("Distanation directory is subfolder of source directory");
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
                throw new ServiceArgumentNullException();
            }
            var currentDirectory = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindByPathEagerLoadingFiles(path) 
                ?? throw new DirectoryDoesNotExistException();
             
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
                throw new ServiceArgumentNullException();
            }
               
            var currentDirectory = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindByPathEagerLoadingFull(path) 
                ?? throw new DirectoryDoesNotExistException(); 

            IList<string> results = new List<string>();
            RecursiveSearch(pattern, results, currentDirectory);
            return results;
        }


        public IDictionary<string, IList<string>> ShowContent(string path)
        {
            if (path == null)
            {
                throw new ServiceArgumentNullException();
            }
            var directory = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindByPathEagerLoadingFull(path) 
                ?? throw new DirectoryDoesNotExistException();
   
            var result = new Dictionary<string, IList<string>>();
            IList<string> files = directory.Files.Select(x => x.Name + x.Extension).ToList();
            IList<string> directories = directory.ChildrenDirectories.Select(x => x.Name).ToList();
            result.Add("Directories", directories);
            result.Add("Files", files);
            return result;
        }

        public DirectoryStructureDto GetInfoByPath(string path)
        {
            if (path == null)
            {
                throw new ServiceArgumentNullException();
            }
            return _mapper.Map<DirectoryStructureDto>(_unitOfWork
                 .Get<IDirectoryRepository>()
                 .FindByPath(path)
                 ?? throw new DirectoryDoesNotExistException());
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
                throw new DirectoryExistsException();
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
                    results.Add(directory.FullPath + "\\" + file.Name + file.Extension);
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

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
