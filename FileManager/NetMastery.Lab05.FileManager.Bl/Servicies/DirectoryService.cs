using System;
using NetMastery.Lab05.FileManager.DAL.Repository;
using System.Linq;
using AutoMapper;
using NetMastery.Lab05.FileManager.DAL.Entities;
using System.Resources;
using System.Collections.Generic;
using System.Text;
using NetMastery.Lab05.FileManager.BL.Dto;
using System.IO;
using NetMastery.Lab05.FileManager.DAL.Interfacies;

namespace NetMastery.Lab05.FileManager.BL.Servicies
{
    public class DirectoryService : IDisposable
    {
        IUnitOfWork _unitOfWork;

        #region Constructors

        public DirectoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }        
        #endregion

        #region DirectoryServiceAPI

        public void Add(string path, string name, string currentPath)
        {
            var fullPath = CreatePath(currentPath, path);
            var currentDirectory = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x=>x.FullPath == fullPath)
                .FirstOrDefault());

            if (currentDirectory != null)
            {
                var newDirectory = new DirectoryStructureDto
                {
                    Name = name,
                    CreationDate = DateTime.Now,
                    ModificationDate = DateTime.Now,
                    ParentDirectory = currentDirectory,
                };
                var newDirInfo = Directory.CreateDirectory(fullPath.Replace("~", Directory.GetCurrentDirectory())+"\\"+"name");
                try
                {
                    _unitOfWork.Repository<DirectoryStructure>()
                        .Add(Mapper.Map<DirectoryStructure>(newDirectory));

                    _unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    Directory.Delete(fullPath);
                    throw;
                }
            }
            else
                throw new ArgumentException();
        }

        public void MoveDirectory(string pathFrom, string pathTo, string currentPath)
        {
            var fullPathFrom = CreatePath(currentPath, pathFrom);
            var fullPathTo = CreatePath(currentPath, pathTo);

            var currentDirectoryFrom = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x => x.FullPath == fullPathFrom)
                .FirstOrDefault());

            var currentDirectoryTo = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x => x.FullPath == fullPathTo)
                .FirstOrDefault());

            if (currentDirectoryFrom != null && currentDirectoryTo != null)
            {
                currentDirectoryFrom.ParentDirectory = currentDirectoryTo;
                currentDirectoryTo.ChildrenDirectories.Add(currentDirectoryFrom);
                currentDirectoryFrom.FullPath = currentDirectoryTo.FullPath + "\\" + currentDirectoryFrom.Name;
                currentDirectoryFrom.ModificationDate = DateTime.Now;
            }
            Directory.Move(fullPathFrom.Replace("~", Directory.GetCurrentDirectory()),
                            fullPathTo.Replace("~", Directory.GetCurrentDirectory()));
            try
            {
                _unitOfWork.Repository<DirectoryStructure>().AddRange(new[] { Mapper.Instance.Map<DirectoryStructure>(currentDirectoryFrom), Mapper.Instance.Map<DirectoryStructure>(currentDirectoryTo) });
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Directory.Move(fullPathTo.Replace("~", Directory.GetCurrentDirectory()),
                            fullPathFrom.Replace("~", Directory.GetCurrentDirectory()));
                throw;
            }
        }
    

        public void RemoveDirectory(string path, string currentPath)
        {
            var fullPath = CreatePath(currentPath, path);
            var currentDirectory = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork.Repository<DirectoryStructure>().Find(x => x.FullPath == fullPath).FirstOrDefault());
            if (currentDirectory != null)
            {
                RecursiveRemove(currentDirectory);
                Directory.Delete(fullPath.Replace("~", Directory.GetCurrentDirectory()), true);
                _unitOfWork.Commit();
            }
            else
                throw new ArgumentException("Directory doesn't exist");
        }

        public IEnumerable<string> Search(string currentPath, string pattern)
        {
            var currentDirectory = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x => x.FullPath == currentPath)
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

        public string ChangeWorkDirectory(string path,string currentPath)
        {
            var createdPath = CreatePath(currentPath, path);
            var currentDirectory = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
               .Repository<DirectoryStructure>()
               .Find(x => x.FullPath == currentPath)
               .FirstOrDefault());

            if (currentDirectory != null)
            {
                return createdPath;
            }
            else
            {
                throw new ArgumentException("Directory doesn't exists");
            }
            
        }

        #endregion

        private string CreatePath(string existingPath, string newPath)
        {
            string[] pathParts = newPath.Split('\\');
            var path = new StringBuilder();
            foreach (var partName in pathParts)
            {
                switch(partName)
                {
                    case "..":
                        if (path.Length == 0) path.Append(existingPath);
                        var index = path.ToString().LastIndexOf("\\");
                        path = path.Remove(index, path.Length - index);
                        break;
                    case "." :
                        if (path.Length == 0) path.Append(existingPath);
                        break;
                    default:
                        if (partName.Any(x => x == '/' 
                        || x == ':' 
                        || x == '*' 
                        || x== '?' 
                        || x=='<' 
                        || x=='>' 
                        || x == '\"' 
                        || x=='|' 
                        || x == '~'))
                        {
                            throw new ArgumentException("The characters: /,|,:,*,<,>,\\,~\" are not allowed");
                        }
                        if(!string.IsNullOrEmpty(partName))
                        {
                            path.Append(partName);
                        }
                        break;
                }
            }
            return path.ToString();
        }

        public DirectoryStructureDto GetInfoByCurrentPath(string path)
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

        private void RecursiveRemove(DirectoryStructureDto rootDirectory)
        {
           if (rootDirectory.ChildrenDirectories != null || rootDirectory.ChildrenDirectories.Count != 0)
            {
                foreach (var child in rootDirectory.ChildrenDirectories)
                {
                    RecursiveRemove(child);
                }
            }
            if (rootDirectory.Files != null)
            {
                _unitOfWork.Repository<FileStructure>().RemoveRange(rootDirectory.Files.Select(x => Mapper.Instance.Map<FileStructure>(x)));
            }
            _unitOfWork.Repository<DirectoryStructure>().Remove(Mapper.Instance.Map<DirectoryStructure>(rootDirectory));
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}
