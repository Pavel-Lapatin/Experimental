using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using System.IO;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.FileManager.Bl.Interfaces;

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
            var currentDirectory = _unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x=>x.FullPath == path)
                .FirstOrDefault();

            if (currentDirectory != null)
            {
                var newDirectory = new DirectoryStructureDto
                {
                    Name = name,
                    CreationDate = DateTime.Now,
                    ModificationDate = DateTime.Now,
                    FullPath = path + "\\" + name,
                };
                if (_unitOfWork.Repository<DirectoryStructure>()
                        .Find(x => x.FullPath == newDirectory.FullPath)
                        .FirstOrDefault() != null)
                {
                    throw new ArgumentException("This directory already exists");
                }   
                var newDirInfo = Directory.CreateDirectory(newDirectory.FullPath.Replace("~", Directory.GetCurrentDirectory()));
                try
                {
                    var newDir = Mapper.Instance.Map<DirectoryStructure>(newDirectory);
                    newDir.ParentDirectory = currentDirectory;
                    _unitOfWork.Repository<DirectoryStructure>().Add(newDir);
                    _unitOfWork.Commit();
                }
                catch (Exception)
                {
                    Directory.Delete(path);
                    throw;
                }
            }
            else
            {
                throw new ArgumentException("Directory doesn't exist");
            }
                
        }

        public void Move(string pathFrom, string pathTo)
        {
            var currentDirectoryFrom = _unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x => x.FullPath == pathFrom)
                .FirstOrDefault();

            var currentDirectoryTo = _unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x => x.FullPath == pathTo)
                .FirstOrDefault();

            if (currentDirectoryFrom == null || currentDirectoryTo == null)
            {
                throw new NullReferenceException("Directory doesn't exist");
            } 
            if(currentDirectoryTo.FullPath.Contains(currentDirectoryFrom.FullPath))
            {
                throw new NullReferenceException("Distanation directory is subfolder of source directory");
            }
            currentDirectoryFrom.ParentDirectory = currentDirectoryTo;
            currentDirectoryFrom.FullPath = currentDirectoryTo.FullPath + "\\" + currentDirectoryFrom.Name;
            currentDirectoryFrom.ModificationDate = DateTime.Now;
            var source = pathFrom.Replace("~", Directory.GetCurrentDirectory());
            var destination = pathTo.Replace("~", Directory.GetCurrentDirectory()) + '\\' + currentDirectoryFrom.Name;
            if (Directory.Exists(source))
            { 
                if(Directory.Exists(destination))
                {
                    throw new ArgumentException("There is already folder with the exact name as soource folder");
                }
                Directory.Move(source, destination);
                try
                {
                    _unitOfWork.Repository<DirectoryStructure>().Add(currentDirectoryFrom);
                    _unitOfWork.Commit();
                }
                catch (Exception)
                {
                    Directory.Move(destination, source);
                    throw;
                }
            }
        }
    

        public void Remove(string path)
        {
            var currentDirectory = _unitOfWork
                .Repository<DirectoryStructure>()
                .EagerFind((x => x.FullPath == path), x=>x.ChildrenDirectories).FirstOrDefault(); 

            if (currentDirectory != null)
            {
                RecursiveRemove(currentDirectory);
                Directory.Delete(path.Replace("~", Directory.GetCurrentDirectory()), true);
                _unitOfWork.Commit();
            }
            else
                throw new ArgumentException("Directory doesn't exist");
        }


        public IEnumerable<string> Search(string path, string pattern)
        {
            var currentDirectory = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
                .Repository<DirectoryStructure>()
                .EagerFind((x => x.FullPath == path), x=>x.ChildrenDirectories)
                .FirstOrDefault());

            IList<string> results = new List<string>();

            if (currentDirectory != null)
            {
                RecursiveSearch(pattern, results, currentDirectory);
                return results;
            }
            else
                throw new ArgumentException("Directory doesn't exist");
        }

        public string ChangeWorkDirectory(string path)
        {
            var currentDirectory = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
               .Repository<DirectoryStructure>()
               .Find(x => x.FullPath == path)
               .FirstOrDefault());

            if (currentDirectory != null)
            {
                return path;
            }
            else
            {
                throw new ArgumentException("Directory doesn't exists");
            }
            
        }

        #endregion

        public DirectoryStructureDto GetInfoByPath(string path)
        {
            return Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
                .Repository<DirectoryStructure>().Find(x => x.FullPath == path).FirstOrDefault());
        }


        private void RecursiveSearch(string pattern, IList<string> results, DirectoryStructureDto directory)
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
            if(directory.Files != null && directory.Files.Count != 0)
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
            if (rootDirectory.ChildrenDirectories != null || rootDirectory.ChildrenDirectories.Count != 0)
            {
                var children = rootDirectory.ChildrenDirectories.ToList();
                for (int i = 0; i < children.Count; i++)
                {
                    RecursiveRemove(children[i]);
                }
            }
            if (rootDirectory.Files != null || rootDirectory.Files.Count != 0)
            {
                _unitOfWork.Repository<FileStructure>().RemoveRange(rootDirectory.Files);
                _unitOfWork.Repository<DirectoryStructure>().Remove(rootDirectory);

            }

        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

       
    }
}
