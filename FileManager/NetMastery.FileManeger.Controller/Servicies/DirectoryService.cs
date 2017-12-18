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
        private ResourceManager rm;
        private UnitOfWork unitOfWork;


        #region Constructors
        public DirectoryService(ResourceManager rm, UnitOfWork unitOfWork) 
        {
            this.rm = rm;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region DirectoryServiceAPI

        public DirectoryStructureDto GetDirectoryInfo(string path, int rootDirectoryId)
        {
            var currentDirectory = GetDirectoryByPath(path, rootDirectoryId);
            if (currentDirectory != null)
            {
                return Mapper.Instance.Map<DirectoryStructureDto>(currentDirectory);
            }
            else throw new ArgumentException(rm.GetString("DirectoryNotExistArgumenException"));
        }

        public void AddNewCatalog(string path, string name, int rootDirectoryId)
        {

            var currentDirectory = GetDirectoryByPath(path, rootDirectoryId);
            if (currentDirectory != null)
            {
                var newDirectory = new DirectoryStructureDto
                {
                    Name = name,
                    CreationDate = DateTime.Now,
                    ModificationDate = DateTime.Now,
                    ParentDirectory = currentDirectory,
                };
                unitOfWork.Directories.Add(Mapper.Map<DirectoryStructure>(newDirectory));
                unitOfWork.Complete();
            }
            else
                throw new ArgumentException(rm.GetString("DirectoryNotExistArgumenException"));
        }

        public void MoveCatalog(string path1, string path2, int rootDirectoryId)
        {
            var currentDirectory = GetDirectoryByPath(rootDirectoryId, new[] {path1, path2 });
            if (currentDirectory[0] != null && currentDirectory[1] != null)
            {
                currentDirectory[0].ParentDirectory = currentDirectory[1];
                currentDirectory[0].FullPath = currentDirectory[1].FullPath + "\\" + currentDirectory[1].Name;
                currentDirectory[0].ModificationDate = DateTime.Now;
            }
        }

        public void RemoveCatalog(string path, int rootDirectoryId)
        {
            var currentDirectory = GetDirectoryByPath(path, rootDirectoryId);
            if (currentDirectory != null)
            {
                RecursiveRemove(currentDirectory);
                unitOfWork.Complete();
            }
            else
                throw new ArgumentException(rm.GetString("DirectoryNotExistArgumenException"));
        }

        public DirectoryStructureDto GetRootDirectory(string path, int rootDirectoryId)
        {
            var currentDirectory = GetDirectoryByPath(path, rootDirectoryId);
            if (currentDirectory != null)
            {
                return currentDirectory;
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
                RecursiveSearch(currentDirectory, pattern, results, rootFullPath);
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

        private DirectoryStructureDto GetDirectoryByFullPath(string fullPath)
        {
            //method 1 using FullPath property
           var directory = unitOfWork.Directories.;


            return Mapper.Instance.Map<DirectoryStructureDto>(directory);

            
        }

        private void RecursiveSearch(DirectoryStructureDto rootDirectory, string pattern,  IList<string> results, StringBuilder fullPath)
        {          
            if (rootDirectory.ChildrenDirectories != null || rootDirectory.ChildrenDirectories.Count != 0)
            {
                foreach (var child in rootDirectory.ChildrenDirectories)
                {
                    fullPath.Append('\\');
                    fullPath.Append(child.Name);
                    RecursiveSearch(child, pattern, results, fullPath);
                }
            }
            if(fullPath.ToString().Contains(pattern))
            {
                results.Add(fullPath.ToString());
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
                unitOfWork.Files.RemoveRange(rootDirectory.Files.Select(x => Mapper.Instance.Map<FileStructure>(x)));
            }
            unitOfWork.Directories.Remove(Mapper.Instance.Map<DirectoryStructure>(rootDirectory));
        }

        private DirectoryStructureDto GetDirectoryByPath(string path, int directoryId)
        {
            var names = path.Split('\\');
            var rootTree = Mapper.Instance.Map<DirectoryStructureDto>(unitOfWork.Directories.Get(directoryId));
            if (rootTree == null || rootTree.Name != names[0]) return null;
            DirectoryStructureDto directory = rootTree;
            for (int i = 1; i < names.Length; i++)
            {
                foreach (var child in directory.ChildrenDirectories)
                {
                    if (child.Name == names[i])
                    {
                        directory = child;
                        break;
                    }
                    directory = null;
                }
            }
            return directory;  
        }

        private List<DirectoryStructureDto> GetDirectoryByPath(int directoryId, params string[] pathes)
        {
            var rootTree = Mapper.Instance.Map<DirectoryStructureDto>(unitOfWork.Directories.Get(directoryId));
            if (rootTree == null) return null;
            List<DirectoryStructureDto> result = new List<DirectoryStructureDto>();
            foreach (var path in pathes)
            {
                var names = path.Split('\\');
                if (rootTree.Name != names[0]) result.Add(null);
                DirectoryStructureDto directory = rootTree;
                for (int i = 1; i < names.Length; i++)
                {
                    foreach (var child in directory.ChildrenDirectories)
                    {
                        if (child.Name == names[i])
                        {
                            directory = child;
                            break;
                        }
                        directory = null;
                    }
                }
                result.Add(directory);
            }
            return result;
        }

        //private void CalculateSum(DirectoryStructureDto rootDirectory, ref long sum)
        //{
        //    if(rootDirectory.Files != null && rootDirectory.Files.Count >0)
        //    {
        //        foreach (var file in rootDirectory.Files)
        //        {
        //            sum += file.FileSize;
        //        }
        //    }
        //    foreach (var directory in rootDirectory.ChildrenDirectories)
        //    {
        //        CalculateSum(directory, ref sum);
        //    }
        //}
     
        private bool CheckName(DirectoryStructureDto directory, string name)
        {
            foreach (var child in directory.ChildrenDirectories)
            {
                if (child.Name == name) ;
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();

        }
    }
}
