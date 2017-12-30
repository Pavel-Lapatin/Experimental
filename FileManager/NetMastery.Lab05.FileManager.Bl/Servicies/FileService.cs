using AutoMapper;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Dto;
using System;
using System.Linq;
using System.IO;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.DAL.Repository;
using Serilog;
using NetMastery.Lab05.FileManager.DAL.Exceptions;

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

        public void Upload(string pathToStorage, string pathToFile)
        {
            try
            {
                var currentDirectory = ((IDirectoryRepository)_unitOfWork
                    .FSRepository<DirectoryRepository>())
                    .GetCurrentPath();

                var fullPathToFile = pathToFile.Replace("~", currentDirectory);
                if (!_unitOfWork.FSRepository<FileRepository>().IsExist(fullPathToFile))
                {
                    throw new FileDoesNotExistException();
                }

                var directory = _unitOfWork
                    .DBRepository<DirectoryStructure>()
                    .Find(x => x.FullPath == pathToStorage)
                    .FirstOrDefault() ?? throw new DirectoryDoesNotExistException();

                var userRootDirectoryName = pathToStorage.Split('\\')[1];

                var account = _unitOfWork
                    .DBRepository<Account>()
                    .Find(x => x.RootDirectory.Name == userRootDirectoryName)
                    .FirstOrDefault() ?? throw new UnathorizeStorageAccessException();

                var freeSpace = GetFreeSpace(account);

                var fileInfo = ((IFileRepository)_unitOfWork
                    .FSRepository<FileRepository>())
                    .GetFileInfo(pathToFile) ?? throw new FileDoesNotExistException();

                if (freeSpace < fileInfo.Length)
                {
                    throw new FileManagerBlArgumentException("Free space is not enough");
                }

                var newFile = Mapper.Instance.Map<FileStructure>(fileInfo);
                newFile.Directory = directory;
                account.CurentStorageSize += newFile.FileSize;

                _unitOfWork.DBRepository<FileStructure>().Add(newFile);
                var fullPathToNewFile = pathToStorage.Replace("~", currentDirectory) + '\\' + fileInfo.Name;
                ((IFileRepository)_unitOfWork.FSRepository<FileRepository>()).Copy(fullPathToNewFile, fullPathToFile);
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

        public void Download(string pathToFile, string pathToStorage)
        {
            try
            {
                var currentDirectory = ((IDirectoryRepository)_unitOfWork
                    .FSRepository<DirectoryRepository>())
                    .GetCurrentPath();

                pathToFile = pathToFile.Replace("~", currentDirectory);

                if (!_unitOfWork.FSRepository<FileRepository>().IsExist(pathToFile))
                {
                    throw new FileDoesNotExistException();
                }
                if (pathToStorage.Contains(currentDirectory))
                {
                    throw new FileManagerBlArgumentException(
                        "For downloading file into storage use upload or move commands, " +
                        "please");
                }

                var fileName = GetFileName(pathToFile);
                var virtualDirectoryPath = GetVirtualDirectoryPath(pathToFile, currentDirectory);

                var fileInfo = ((IFileRepository)_unitOfWork
                    .FSRepository<FileRepository>())
                    .GetFileInfo(pathToFile) ?? throw new FileDoesNotExistException();

                var fileStructure = _unitOfWork
                .DBRepository<DirectoryStructure>()
                .EagerFind(x => x.FullPath == virtualDirectoryPath, x => x.Files)
                .FirstOrDefault()?.Files.FirstOrDefault(x => x.Name + x.Extension == fileName)
                ?? throw new FileDoesNotExistException();

                if (!IsFileValid(fileInfo, fileStructure))
                {
                    throw new FileManagerBlArgumentException("File is damaged");
                }

                var pathToNewFile = pathToStorage + '\\' + fileName;

                fileStructure.DownloadsNumber++;
                ((IFileRepository)_unitOfWork.FSRepository<FileRepository>()).Copy(pathToNewFile, pathToFile);
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

        private bool IsFileValid(FileInfo fileInfo, FileStructure fileStructure)
        {
            if (fileStructure.FileSize * 1024 == fileInfo.Length)
            {
                return true;
            }
            return false;
        }

        private string GetVirtualDirectoryPath(string pathToFile, string currentPath)
        {
            var virtuaPath = pathToFile.Replace(currentPath, "~");
            return virtuaPath.Substring(0, virtuaPath.LastIndexOf('\\'));
        }

        private string GetFileName(string pathToFile)
        {
            return pathToFile.Substring(pathToFile.LastIndexOf('\\') + 1);
        }


        public void Move(string pathToFile, string pathToStorage)
        {
                var currentDirectory = ((IDirectoryRepository)_unitOfWork
                    .FSRepository<DirectoryRepository>())
                    .GetCurrentPath();

                var fileName = GetFileName(pathToFile);
                var virtualDirectoryPath = GetVirtualDirectoryPath(pathToFile, currentDirectory);
                var fullFilePath = pathToFile.Replace("~", currentDirectory);
                var virtualStoragePath = pathToStorage.Replace(currentDirectory, "~");
                var newFileFullPath = virtualStoragePath.Replace("~", currentDirectory) + '\\' + fileName;
            try
            {
                var fileStructure = _unitOfWork
                .DBRepository<DirectoryStructure>()
                .EagerFind(x => x.FullPath == virtualDirectoryPath, x => x.Files)
                .FirstOrDefault()?.Files.FirstOrDefault(x => x.Name + x.Extension == fileName)
                ?? throw new FileDoesNotExistException();

                var storage = _unitOfWork
                .DBRepository<DirectoryStructure>()
                .Find(x => x.FullPath == virtualStoragePath).FirstOrDefault()
                ?? throw new DirectoryDoesNotExistException();

                fileStructure.Directory = storage;
                _unitOfWork.FSRepository<FileRepository>().Move(newFileFullPath, fullFilePath);
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
                _unitOfWork.FSRepository<FileRepository>().MoveRollback(newFileFullPath, fullFilePath);
                throw new FileManagerBlArgumentException(e.Message);
            }
        }

        public void Remove(string path)
        {
            try
            {
                var currentDirectory = ((IDirectoryRepository)_unitOfWork
                    .FSRepository<DirectoryRepository>())
                    .GetCurrentPath();

                var fileName = GetFileName(path);
                var virtualDirectoryPath = GetVirtualDirectoryPath(path, currentDirectory);
                var fullFilePath = path.Replace("~", currentDirectory);

                var fileStructure = _unitOfWork
                .DBRepository<DirectoryStructure>()
                .EagerFind(x => x.FullPath == virtualDirectoryPath, x => x.Files)
                .FirstOrDefault()?.Files.FirstOrDefault(x => x.Name + x.Extension == fileName)
                ?? throw new FileDoesNotExistException();

                _unitOfWork.DBRepository<FileStructure>().Remove(fileStructure);
                _unitOfWork.FSRepository<FileRepository>().Remove(fullFilePath);
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

        public FileStructureDto GetFileByPath(string path)
        {
            var index = path.LastIndexOf('\\')+1;
            
            var fileName = path.Substring(index, path.Length-index);
            var storageDirectoryPath = path.Substring(0, path.LastIndexOf('\\'));
            return Mapper.Instance.Map<FileStructureDto>(_unitOfWork
                .DBRepository<DirectoryStructure>()
                .EagerFind(x => x.FullPath == storageDirectoryPath, x => x.Files)
                .FirstOrDefault()?.Files.FirstOrDefault(x => x.Name+x.Extension == fileName));
        }
        

        private long GetFreeSpace(Account account)
        {
            return account.MaxStorageSize - account.CurentStorageSize;
        }
        #endregion
    }
}
