using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using System.IO;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using Serilog;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.DAL.Exceptions;

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public class DirectoryService : IDirectoryService, IDisposable
    {
        IUnitOfWork _unitOfWork;

        #region Constructors

        public DirectoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region DirectoryServiceAPI

        public void Add(string path, string name)
        {
            if (path == null || name == null)
            {
                throw new NullInputParametrException();
            }

            var newDirectory = CreateNewDirectory(path, name);

            var currentDirectory = _unitOfWork
                .DBRepository<DirectoryStructure>()
                .Find(x => x.FullPath == path)
                .FirstOrDefault() ?? throw new DirectoryDoesNotExistException();

            newDirectory.ParentDirectory = currentDirectory;
            try
            {
                ((IDirectoryRepository)_unitOfWork.FSRepository<DirectoryRepository>()).AddFolder(path, name);
                _unitOfWork.DBRepository<DirectoryStructure>().Add(newDirectory);
                _unitOfWork.Commit();
            }
            catch (FSRepositoryArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                throw new FileManagerBlArgumentException(e.Message);
            }
            catch (DBRepositoryArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                _unitOfWork.FSRepository<DirectoryRepository>().Remove(path + '\\' + name);
                throw new FileManagerBlArgumentException(e.Message);
            }
        }

        public void Move(string pathFrom, string pathTo)
        {
            if (pathFrom == null || pathTo == null)
            {
                throw new NullInputParametrException();
            }

            var currentDirectoryFrom = _unitOfWork
                .DBRepository<DirectoryStructure>()
                .EagerFind(x => x.FullPath == pathFrom, x => x.ParentDirectory)
                .FirstOrDefault() ?? throw new DirectoryDoesNotExistException();

            var currentDirectoryTo = _unitOfWork
                .DBRepository<DirectoryStructure>()
                .Find(x => x.FullPath == pathTo)
                .FirstOrDefault() ?? throw new DirectoryDoesNotExistException();

            if (currentDirectoryTo.FullPath.Contains(currentDirectoryFrom.FullPath))
            {
                throw new FileManagerBlArgumentException("Distanation directory is subfolder of source directory");
            }

            ChangeFullPath(currentDirectoryFrom.FullPath, 
                currentDirectoryFrom.ParentDirectory.FullPath,
                pathTo);

            currentDirectoryFrom.ParentDirectory = currentDirectoryTo;
            currentDirectoryFrom.ModificationDate = DateTime.Now;
            try
            {
                _unitOfWork.FSRepository<DirectoryRepository>().Move(pathTo, pathFrom);
                _unitOfWork.Commit();
            }
            catch (FSRepositoryArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                throw new FileManagerBlArgumentException(e.Message);
            }
            catch (DBRepositoryArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                _unitOfWork.FSRepository<DirectoryRepository>().MoveRollback(pathTo, pathFrom);
                throw new FileManagerBlArgumentException(e.Message);
            }
        } 

        public void Remove(string path)
        {
            if (path == null)
            {
                throw new NullInputParametrException();
            }

            var currentDirectory = (_unitOfWork
                .DBRepository<DirectoryStructure>()
                .EagerFind((x => x.FullPath == path), x => x.ChildrenDirectories)
                .FirstOrDefault()) ?? throw new DirectoryDoesNotExistException();
            RecursiveRemove(currentDirectory);
            try
            {
                _unitOfWork.FSRepository<DirectoryRepository>().Remove(path);
                _unitOfWork.Commit();
            }
            catch (FSRepositoryArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                throw new FileManagerBlArgumentException(e.Message);
            }
            catch (DBRepositoryArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                throw new FileManagerBlArgumentException(e.Message);
            }      
        }


        public IEnumerable<string> Search(string path, string pattern)
        {
            if (path == null || pattern == null)
            {
            throw new NullInputParametrException();
            }
               
            var currentDirectory = _unitOfWork
                .DBRepository<DirectoryStructure>()
                .EagerFind(x => x.FullPath == path, x => x.ChildrenDirectories, x => x.Files)
                .FirstOrDefault() ?? throw new DirectoryDoesNotExistException(); 

            IList<string> results = new List<string>();
            RecursiveSearch(pattern, results, currentDirectory);
            return results;
        }


        public IEnumerable<string> ShowContent(string path)
        {
            if (path == null)
            {
                throw new NullInputParametrException();
            }
            var directory = _unitOfWork
                .DBRepository<DirectoryStructure>()
                .EagerFind(x => x.FullPath == path, x => x.Files)
                .FirstOrDefault() ?? throw new DirectoryDoesNotExistException();
   
            var result = new List<string>();
            var files = directory.Files.Select(x => x.Name + x.Extension).ToList();
            var directories = directory.ChildrenDirectories.Select(x => x.Name).ToList();
            result.AddRange(directories);
            result.AddRange(files);
            return result;
        }

        public string ChangeWorkDirectory(string path)
        {
            if (path == null)
            {
                throw new NullInputParametrException();
            }

            var currentDirectory = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
               .DBRepository<DirectoryStructure>()
               .Find(x => x.FullPath == path)
               .FirstOrDefault()) ?? throw new DirectoryDoesNotExistException();
            return path;
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
            if (_unitOfWork.DBRepository<DirectoryStructure>()
                    .Find(x => x.FullPath == newDirectory.FullPath)
                    .FirstOrDefault() != null)
            {
                throw new DirectoryExistsException();
            }
            return Mapper.Instance.Map<DirectoryStructure>(newDirectory);
        }

        public DirectoryStructureDto GetInfoByPath(string path)
        {
            if (path == null)
            {
            throw new NullInputParametrException();
            }   
            return Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
                .DBRepository<DirectoryStructure>().Find(x => x.FullPath == path).FirstOrDefault());
        }

        private void RecursiveSearch(string pattern, IList<string> results, DirectoryStructure directory)
        {          
            if (directory.ChildrenDirectories != null && directory.ChildrenDirectories.Count != 0)
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
            if(directory.Files != null && directory.Files.Count > 0)
            {
                foreach (var file in directory.Files)
                {
                    if(file.Name.Contains(pattern))
                    {
                        results.Add(directory.FullPath + "\\" + file.Name);
                    }
                }
            }
        }

        private void RecursiveRemove(DirectoryStructure rootDirectory)
        {
            if (rootDirectory.ChildrenDirectories != null || rootDirectory.ChildrenDirectories.Count > 0)
            {
                var children = rootDirectory.ChildrenDirectories.ToList();
                for (int i = 0; i < children.Count; i++)
                {
                    RecursiveRemove(children[i]);
                }
            }
            if (rootDirectory.Files != null || rootDirectory.Files.Count > 0)
            {
                _unitOfWork.DBRepository<FileStructure>().RemoveRange(rootDirectory.Files);  
            }
            _unitOfWork.DBRepository<DirectoryStructure>().Remove(rootDirectory);

        }

        private void ChangeFullPath(string pathFrom, string pattern, string pathTo)
        {
            var dir = _unitOfWork
                .DBRepository<DirectoryStructure>()
                .Find(x => x.FullPath.Contains(pathFrom))
                .ToList();
            for (int i = 0; i < dir.Count; i++)
            {
                dir[i].FullPath = dir[i].FullPath.Replace(pattern, pathTo);
            }
        }


        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
