using AutoMapper;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Dto;
using System;
using System.Linq;
using System.IO;
using NetMastery.Lab05.FileManager.Domain;

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public class FileService : IFileService
    {
        IUnitOfWork _unitOfWork;
        #region Constructors
        public FileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region FileServiceAPI
  
        public void Upload(string pathToFile, string pathToStorage)
        {
            var fullPathToFile = pathToFile.Replace("~", Directory.GetCurrentDirectory());

            var directory = _unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x => x.FullPath == pathToStorage)
                .FirstOrDefault();

            if (File.Exists(pathToFile) && directory != null)
            {
                var userRootDirectoryName = pathToStorage.Split('\\')[1];
                var account = _unitOfWork
                    .Repository<Account>()
                    .Find(x => x.RootDirectory.Name == userRootDirectoryName)
                    .FirstOrDefault();
                if(account != null)
                {
                    var freeSpace = account.MaxStorageSize - account.CurentStorageSize;
                    var newFile = Mapper.Instance.Map<FileStructure>(new FileInfo(pathToFile));
                    var fullPathToNewFile = pathToStorage.Replace("~", Directory.GetCurrentDirectory())+'\\'+newFile.Name;
                    if (freeSpace < (newFile.FileSize))
                    {
                        throw new ArgumentException("There is no enouugh space in the storage");
                    }
                    File.Copy(pathToFile, fullPathToNewFile);
                    account.CurentStorageSize += newFile.FileSize;

                    try
                    {
                        newFile.Directory = directory;
                        _unitOfWork.Repository<FileStructure>().Add(newFile);
                        _unitOfWork.Commit();
                    }
                    catch (Exception e)
                    {
                        File.Delete(fullPathToNewFile);
                        Console.WriteLine(e.Message);
                    }    
                }
                else
                {
                    throw new ArgumentException("Storage account has invalid rootFolder");
                }
                
            }
            else
            {
                throw new ArgumentException("Uploading file or storage directory don't exist");
            }
        }

        public void Download(string pathFromStorage, string pathToFile)
        {
            var fullPathToFile = pathToFile.Replace("~", Directory.GetCurrentDirectory());
            if (pathToFile.Contains(Directory.GetCurrentDirectory()))
            {
                throw new ArgumentException("For downloading file into storage use upload or move commands, please");
            }
            var fileName = pathFromStorage.Substring(pathFromStorage.LastIndexOf('\\') + 1, pathFromStorage.Length - 1);
            var storageDirectoryPath = pathFromStorage.Substring(0, pathFromStorage.LastIndexOf('\\'));
            var file = _unitOfWork
                .Repository<DirectoryStructure>()
                .EagerFind(x => x.FullPath == storageDirectoryPath, x=>x.Files)
                .FirstOrDefault()?.Files.FirstOrDefault(x => x.Name == fileName);

            if(file == null)
            {
                throw new ArgumentException("Directory or file are not exist");
            }
            var pathToNewFile = pathToFile + '\\' + fileName;
            File.Copy(pathFromStorage, pathToNewFile);
            file.DownloadsNumber++;
            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                File.Delete(pathToNewFile);
                Console.WriteLine(e.Message);
            }

        }

        public void Move(string pathFrom, string pathTo)
        {
            var fileName = pathFrom.Substring(pathFrom.LastIndexOf('\\') + 1, pathFrom.Length - 1);
            var storageDirectoryPath = pathFrom.Substring(0, pathFrom.LastIndexOf('\\'));
            var file = _unitOfWork
                .Repository<DirectoryStructure>()
                .EagerFind(x => x.FullPath == storageDirectoryPath, x => x.Files)
                .FirstOrDefault()?.Files.FirstOrDefault(x => x.Name == fileName);
            var directoryTo = _unitOfWork
                .Repository<DirectoryStructure>()
                .Find(x => x.FullPath == storageDirectoryPath)
                .FirstOrDefault(); 
            if (file == null || directoryTo == null)
            {
                throw new ArgumentException("Directory or file are not exist");
            }
            var pathToNewFile = pathTo.Replace("~", Directory.GetCurrentDirectory()) + '\\' + fileName;
            var fullPathFromStorage = pathFrom.Replace("~", Directory.GetCurrentDirectory());
            File.Copy(fullPathFromStorage, pathToNewFile);
            file.Directory = directoryTo;
            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                File.Delete(pathToNewFile);
                Console.WriteLine(e.Message);
            }
        }

        public void Remove(string path)
        {
            var fileName = path.Substring(path.LastIndexOf('\\') + 1, path.Length - 1);
            var storageDirectoryPath = path.Substring(0, path.LastIndexOf('\\'));
            var file = _unitOfWork
                .Repository<DirectoryStructure>()
                .EagerFind(x => x.FullPath == storageDirectoryPath, x => x.Files)
                .FirstOrDefault()?.Files.FirstOrDefault(x => x.Name == fileName);
            if(file == null)
            {
                throw new ArgumentException("Directory or file are not exist");
            }
            var fullPath = path.Replace("~", Directory.GetCurrentDirectory());
            File.Delete(fullPath);
            try
            {
                _unitOfWork.Repository<FileStructure>().Remove(file);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public FileStructureDto GetFileByPath(string path)
        {
            var fileName = path.Substring(path.LastIndexOf('\\') + 1, path.Length - 1);
            var storageDirectoryPath = path.Substring(0, path.LastIndexOf('\\'));
            return Mapper.Instance.Map<FileStructureDto>(_unitOfWork
                .Repository<DirectoryStructure>()
                .EagerFind(x => x.FullPath == storageDirectoryPath, x => x.Files)
                .FirstOrDefault()?.Files.FirstOrDefault(x => x.Name == fileName));
        }
        #endregion
    }
}
