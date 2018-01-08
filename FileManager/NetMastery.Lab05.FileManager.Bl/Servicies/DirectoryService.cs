
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
using NetMastery.Lab05.FileManager.DAL.Exceptions;
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
                 .GetDbRepository<IDbDirectoryRepository>()
                 .FindByPath(path)
                 ?? throw new DirectoryDoesNotExistException();
            newDirectory.ParentDirectory = currentDirectory;

            try
            {
                (_unitOfWork.GetFileSystemManager<IFSDirectoryManager>()).AddFolder(path, name);
                _unitOfWork.GetDbRepository<IDbDirectoryRepository>().Add(newDirectory);
                _unitOfWork.Commit();
            }
            catch (FSDirectoryManagerArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                throw new ServiceArgumentException(e.Message);
            }
            catch (DbRepositoryArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                _unitOfWork.GetFileSystemManager<IFSDirectoryManager>().Remove(path + '\\' + name);
                throw new ServiceArgumentException(e.Message);
            }
        }

        public void Move(string pathFrom, string pathTo)
        {
            if (pathFrom == null || pathTo == null)
            {
                throw new ServiceArgumentNullException();
            }

            var currentDirectoryFrom = _unitOfWork
                .GetDbRepository<IDbDirectoryRepository>()
                .FindByPathEagerLoadingParentDirectory(pathFrom) 
                ?? throw new DirectoryDoesNotExistException();

            var currentDirectoryTo = _unitOfWork
                 .GetDbRepository<IDbDirectoryRepository>()
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
                _unitOfWork.GetFileSystemManager<IFSDirectoryManager>().Move(pathTo, pathFrom);
                _unitOfWork.Commit();
            }
            catch (FSDirectoryManagerArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                throw new ServiceArgumentException(e.Message);
            }
            catch (DbRepositoryArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                _unitOfWork.GetFileSystemManager<IFSDirectoryManager>().MoveRollback(pathTo, pathFrom);
                throw new ServiceArgumentException(e.Message);
            }
        } 

        public void Remove(string path)
        {
            if (path == null)
            {
                throw new ServiceArgumentNullException();
            }

            var currentDirectory = _unitOfWork
                .GetDbRepository<IDbDirectoryRepository>()
                .FindByPathEagerLoadingFiles(path) 
                ?? throw new DirectoryDoesNotExistException();

            RecursiveRemove(currentDirectory);
            try
            {
                _unitOfWork.GetFileSystemManager<IFSDirectoryManager>().Remove(path);
                _unitOfWork.Commit();
            }
            catch (FSDirectoryManagerArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                throw new ServiceArgumentException(e.Message);
            }
            catch (DbRepositoryArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                throw new ServiceArgumentException(e.Message);
            }      
        }


        public IEnumerable<string> Search(string path, string pattern)
        {
            if (path == null || pattern == null)
            {
                throw new ServiceArgumentNullException();
            }
               
            var currentDirectory = _unitOfWork
                .GetDbRepository<IDbDirectoryRepository>()
                .FindByPathEagerLoadingFull(path) 
                ?? throw new DirectoryDoesNotExistException(); 

            IList<string> results = new List<string>();
            RecursiveSearch(pattern, results, currentDirectory);
            return results;
        }


        public IEnumerable<string> ShowContent(string path)
        {
            if (path == null)
            {
                throw new ServiceArgumentNullException();
            }
            var directory = _unitOfWork
                .GetDbRepository<IDbDirectoryRepository>()
                .FindByPathEagerLoadingFull(path) 
                ?? throw new DirectoryDoesNotExistException();
   
            var result = new List<string>();
            var files = directory.Files.Select(x => x.Name + x.Extension);
            var directories = directory.ChildrenDirectories.Select(x => x.Name);
            result.AddRange(directories);
            result.AddRange(files);
            return result;
        }

        public DirectoryStructureDto GetInfoByPath(string path)
        {
            if (path == null)
            {
                throw new ServiceArgumentNullException();
            }
            return _mapper.Map<DirectoryStructureDto>(_unitOfWork
                 .GetDbRepository<IDbDirectoryRepository>()
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
                 .GetDbRepository<IDbDirectoryRepository>()
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
            _unitOfWork.GetDbRepository<IDbFileRepository>().RemoveRange(rootDirectory.Files);  
            _unitOfWork.GetDbRepository<IDbDirectoryRepository>().Remove(rootDirectory);

        }

        private void ChangeFullPath(string pathFrom, string pattern, string pathTo)
        {
            var dir = _unitOfWork
                .GetDbRepository<IDbDirectoryRepository>()
                .FindDirectoriesWhichContainsPath(pathFrom).ToArray();
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
