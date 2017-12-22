using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.Domain;

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

        public void Add(string path, string name)
        {
            var currentDirectory = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x=>x.FullPath == path)
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
                var newDirInfo = Directory.CreateDirectory(path.Replace("~", Directory.GetCurrentDirectory())+"\\"+"name");
                try
                {
                    _unitOfWork.Repository<DirectoryStructure>()
                        .Add(Mapper.Map<DirectoryStructure>(newDirectory));

                    _unitOfWork.Commit();
                }
                catch (Exception)
                {
                    Directory.Delete(path);
                    throw;
                }
            }
            else
                throw new ArgumentException();
        }

        public void Move(string pathFrom, string pathTo)
        {
            var currentDirectoryFrom = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x => x.FullPath == pathFrom)
                .FirstOrDefault());

            var currentDirectoryTo = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x => x.FullPath == pathTo)
                .FirstOrDefault());

            if (currentDirectoryFrom != null && currentDirectoryTo != null)
            {
                currentDirectoryFrom.ParentDirectory = currentDirectoryTo;
                currentDirectoryTo.ChildrenDirectories.Add(currentDirectoryFrom);
                currentDirectoryFrom.FullPath = currentDirectoryTo.FullPath + "\\" + currentDirectoryFrom.Name;
                currentDirectoryFrom.ModificationDate = DateTime.Now;
            }
            Directory.Move(pathFrom.Replace("~", Directory.GetCurrentDirectory()),
                            pathTo.Replace("~", Directory.GetCurrentDirectory()));
            try
            {
                _unitOfWork.Repository<DirectoryStructure>().AddRange(new[] { Mapper.Instance.Map<DirectoryStructure>(currentDirectoryFrom), Mapper.Instance.Map<DirectoryStructure>(currentDirectoryTo) });
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                Directory.Move(pathTo.Replace("~", Directory.GetCurrentDirectory()),
                            pathFrom.Replace("~", Directory.GetCurrentDirectory()));
                throw;
            }
        }
    

        public void RemoveDirectory(string path)
        {
            var currentDirectory = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork.Repository<DirectoryStructure>().Find(x => x.FullPath == fullPath).FirstOrDefault());
            if (currentDirectory != null)
            {
                RecursiveRemove(currentDirectory);
                Directory.Delete(path.Replace("~", Directory.GetCurrentDirectory()), true);
                _unitOfWork.Commit();
            }
            else
                throw new ArgumentException("Directory doesn't exist");
        }

        public IEnumerable<string> Search(string pattern, string path)
        {
            var currentDirectory = Mapper.Instance.Map<DirectoryStructureDto>(_unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x => x.FullPath == path)
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
