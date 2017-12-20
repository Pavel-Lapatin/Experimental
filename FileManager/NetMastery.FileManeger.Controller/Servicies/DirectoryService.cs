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

namespace NetMastery.Lab05.FileManager.BL.Servicies
{
    public class DirectoryService : IDisposable
    {
        IUnitOfWork _unitOfWOrk;

        #region Constructors
        public DirectoryService(IUnitOfWork unitOfWOrk)
        {
            _untOfWOrk = unitOfWOrk;
        }        
        #endregion

        #region DirectoryServiceAPI

        public void AddDirectory(string path, string name, string currentPath)
        {
            var fullPath = CreatePath(currentPath, path);
            var currentDirectory = _unitOfWOrk.Repository<DirectoryStructure>().Find(x=>x.FullPathh == fullPath).FirstOrDefault();
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
                    unitOfWork.Directories.Add(Mapper.Map<DirectoryStructure>(newDirectory));
                    unitOfWork.Complete();
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
            var currentDirectoryFrom = _unitOfWOrk.Repository<DirectoryStructure>().Find(x => x.FullPathh == fullPathFrom).FirstOrDefault();
            var currentDirectoryTo = _unitOfWOrk.Repository<DirectoryStructure>().Find(x => x.FullPathh == fullPathTo).FirstOrDefault();
            if (currentDirectoryFrom != null && currentDirectoryTo != null)
            {
                currentDirectoryFrom.ParentDirectory = currentDirectoryTo;
                currentDirectoryTo.ChildDirectories.Add(currentDirectoryFrom);
                currentDirectoryFrom.FullPath = currentDirectoryTo.FullPath + "\\" + currentDirectoryFrom.Name;
                currentDirectoryFrom.ModificationDate = DateTime.Now;
            }
            Directory.Move(fullPathFrom.Replace("~", Directory.GetCurrentDirectory()),
                            fullPathTo.Replace("~", Directory.GetCurrentDirectory()));
            try
            {
                _unitOfWork.Repository<DirectoryStructure>.AddRange(new[] { Mapper.Instance.Map<DirectoryStructure>(currentDirectoryFrom), Mapper.Instance.Map<DirectoryStructure>(currentDirectoryTo) });
                unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Directory.Move(fullPathTo.Replace("~", Directory.GetCurrentDirectory()),
                            fullPathFrom.Replace("~", Directory.GetCurrentDirectory()));
                throw;
            }
        }
    

        public void RemoveDirectory(string path, string CurrentPath)
        {
            var fullPath = CreatePath(currentPath, path);
            var currentDirectory = _unitOfWOrk.Repository<DirectoryStructure>().Find(x => x.FullPathh == fullPath).FirstOrDefault();
            if (currentDirectory != null)
            {
                RecursiveRemove(currentDirectory);
                Directory.Delete(fullPath.Replace("~", Directory.GetCurrentDirectory()), true);
                unitOfWork.Complete();
            }
            else
                throw new ArgumentException(rm.GetString("DirectoryNotExistArgumenException"));
        }

        public IEnumerable<string> Search(string path, string pattern, int rootDirectoryId)
        {
            var currentDirectory = GetDirectoryByPath(path, rootDirectoryId);
            var rootFullPath = new StringBuilder();
            IList<string> results = new List<string>();
            if (currentDirectory != null)
            {
                RecursiveSearch(pattern, results, rootFullPath);
                return results;
            }
            else
                throw new ArgumentException(rm.GetString("DirectoryNotExistArgumenException"));
        }

        public void ChangeWorkDirectory(string path, FileManagerModel model)
        {
            var createdPath = CreatePath(model.currentFullPath, path);
            var directory = GetDirectoryByFullPath(createdPath);
            if (directory != null)
            {
                model.currentFullPath = createdPath;
                model.RootDirectoryId = directory.DirectoryId;
            }
            else
            {
                throw new ArgumentException("Directory doesn't exists");
            }
            
        }

        #endregion

        #region FileServiceAPI

        public FileStructureDto GetFileInfo(string path, int rootDirectoryId)
        {
            var directoryPath = path.Substring(0, path.LastIndexOf("\\"));
            var fileName = path.Substring(path.LastIndexOf("\\") + 1, path.Length - 1);
            var currentDirectory = GetDirectoryByPath(directoryPath, rootDirectoryId);
            if (currentDirectory != null)
            {
                var file = currentDirectory.Files.FirstOrDefault(x => x.Name == fileName);
                if (file == null) throw new ArgumentException("Such file doesn't exist");
                return Mapper.Instance.Map<FileStructureDto>(file);
            }
            else throw new ArgumentException(rm.GetString("DirectoryNotExistArgumenException"));
        }

        public void UploadFile(string uploadFilePath, string newFilePath, int rootDirectoryId)
        {
            var newDirectory = GetDirectoryByPath(newFilePath, rootDirectoryId);
            if (newDirectory != null)
            {
                var oldFile = new FileInfo(uploadFilePath);
                var fileInfo = new FileStructureDto
                {
                    Name = oldFile.Name,
                    CreationTime = oldFile.CreationTime,
                    ModificationDate = oldFile.LastWriteTime,
                    DownloadsNumber = 0,
                    FileSize = oldFile.Length,
                    Extension = oldFile.Extension,
                    Directory = newDirectory
                };
                File.Copy(uploadFilePath, Path.Combine(newFilePath, fileInfo.Name)); 

                unitOfWork.Files.Add(Mapper.Map<FileStructure>(fileInfo));
                unitOfWork.Complete();
            }
            else
                throw new ArgumentException(rm.GetString("DirectoryNotExistArgumenException"));
        }

        public void DownLoadFile(string fromPath, string toPath, int rootDirectoryId)
        {
            if (!Directory.Exists(toPath)) throw new ArgumentException("Destination directory doesn't Exists");
            var directoryPath = fromPath.Substring(0, fromPath.LastIndexOf("\\"));
            var fileName = fromPath.Substring(fromPath.LastIndexOf("\\") + 1, fromPath.Length - 1);
            var directory = GetDirectoryByPath(directoryPath, rootDirectoryId);
            if (directory != null)
            {

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
                        if (partName.Any(x => x == '/' || x == ':' || x == '*' || x== '?' || x=='<' || x=='>' || x=='\"' || x=='|'))
                        {
                            throw new ArgumentException("The characters: /,|,:,*,<,>,\" are not allowed");
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

        private void RecursiveSearch(DirectoryStructureDto rootDirectory, string pattern,  IList<string> results)
        {          
            if (rootDirectory.ChildrenDirectories != null && rootDirectory.ChildrenDirectories.Count != 0)
            {
                foreach (var child in rootDirectory.ChildrenDirectories)
                {
                    RecursiveSearch(child, pattern, results);
                }
            }
            if(rootDirectory.Name.Contains(pattern))
            {
                results.Add(rootDirectory.FullPath);
            }
            if(rootDirectory.Files != null && rootDirectory.Files.Count != 0)
            {
                foreach (var file in rootDirectory.Files)
                {
                    if(file.Name.Contains(pattern))
                    {
                        results.Add(rootDirectory.FullPath + "\\" + file.Name);
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
                _unitOfWork.Repository<FileStructure>.RemoveRange(rootDirectory.Files.Select(x => Mapper.Instance.Map<FileStructure>(x)));
            }
            _unitOfWork.Repository<DirectoryStructure>.Remove(Mapper.Instance.Map<DirectoryStructure>(rootDirectory));
        }

        public void Dispose()
        {
            unitOfWork.Dispose();

        }
    }
}
