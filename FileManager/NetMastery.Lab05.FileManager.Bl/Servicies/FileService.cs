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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #region Constructors
        public FileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region FileServiceAPI

        public void Upload(string pathToStorage, string pathToFile)
        {
            try
            {
                var currentDirectory = (_unitOfWork
                    .GetFsRepository<IFSDirectoryManager>())
                    .GetCurrentPath();

                var fullPathToFile = pathToFile.Replace("~", currentDirectory);
                if (!_unitOfWork.GetFsRepository<IFSFileManager>().IsExist(fullPathToFile))
                {
                    throw new FileDoesNotExistException();
                }

                var directory = _unitOfWork
                    .GetDbRepository<IDbDirectoryRepository>()
                    .Find(x => x.FullPath == pathToStorage)
                    .FirstOrDefault() ?? throw new DirectoryDoesNotExistException();

                var userRootDirectoryName = pathToStorage.Split('\\')[1];

                var account = _unitOfWork
                    .GetDbRepository<IDbAccountRepository>()
                    .Find(x => x.RootDirectory.Name == userRootDirectoryName)
                    .FirstOrDefault() ?? throw new ServiceUnathorizedtAccessException();

                var freeSpace = GetFreeSpace(account);

                var fileInfo = (_unitOfWork
                    .GetFsRepository<IFSFileManager>())
                    .GetFileInfo(pathToFile) ?? throw new FileDoesNotExistException();

                if (freeSpace < fileInfo.Length)
                {
                    throw new ServiceArgumentException("Free space is not enough");
                }

                var newFile = _mapper.Map<FileStructure>(fileInfo);
                newFile.Directory = directory;
                account.CurentStorageSize += newFile.FileSize;

                _unitOfWork.GetDbRepository<IDbFileRepository>().Add(newFile);
                var fullPathToNewFile = pathToStorage.Replace("~", currentDirectory) + '\\' + fileInfo.Name;
                (_unitOfWork.GetFsRepository<IFSFileManager>()).Copy(fullPathToNewFile, fullPathToFile);
                _unitOfWork.Commit();
            }
            catch (FSRepositoryArgumentException e)
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

        public void Download(string pathToFile, string pathToStorage)
        {
            try
            {
                var currentDirectory = (_unitOfWork
                    .GetFsRepository<IFSDirectoryManager>())
                    .GetCurrentPath();

                pathToFile = pathToFile.Replace("~", currentDirectory);

                if (!_unitOfWork.GetFsRepository<IFSFileManager>().IsExist(pathToFile))
                {
                    throw new FileDoesNotExistException();
                }
                if (pathToStorage.Contains(currentDirectory))
                {
                    throw new ServiceArgumentException(
                        "For downloading file into storage use upload or move commands, " +
                        "please");
                }

                var fileName = GetFileName(pathToFile);
                var virtualDirectoryPath = GetVirtualDirectoryPath(pathToFile, currentDirectory);

                var fileInfo = (_unitOfWork
                    .GetFsRepository<IFSFileManager>())
                    .GetFileInfo(pathToFile) ?? throw new FileDoesNotExistException();

                var fileStructure = _unitOfWork
                .GetDbRepository<IDbDirectoryRepository>()
                .FindByPathEagerLoadingFiles(virtualDirectoryPath)?.Files.FirstOrDefault(x => x.Name + x.Extension == fileName)
                ?? throw new FileDoesNotExistException();

                if (!IsFileValid(fileInfo, fileStructure))
                {
                    throw new ServiceArgumentException("File is damaged");
                }

                var pathToNewFile = pathToStorage + '\\' + fileName;

                fileStructure.DownloadsNumber++;
                (_unitOfWork.GetFsRepository<IFSFileManager>()).Copy(pathToNewFile, pathToFile);
                _unitOfWork.Commit();
            }
            catch (FSRepositoryArgumentException e)
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
                var currentDirectory = (_unitOfWork
                    .GetFsRepository<IFSDirectoryManager>())
                    .GetCurrentPath();

                var fileName = GetFileName(pathToFile);
                var virtualDirectoryPath = GetVirtualDirectoryPath(pathToFile, currentDirectory);
                var fullFilePath = pathToFile.Replace("~", currentDirectory);
                var virtualStoragePath = pathToStorage.Replace(currentDirectory, "~");
                var newFileFullPath = virtualStoragePath.Replace("~", currentDirectory) + '\\' + fileName;
            try
            {
                var fileStructure = _unitOfWork
                .GetDbRepository<IDbDirectoryRepository>()
                .EagerFind(x => x.FullPath == virtualDirectoryPath, x => x.Files)
                .FirstOrDefault()?.Files.FirstOrDefault(x => x.Name + x.Extension == fileName)
                ?? throw new FileDoesNotExistException();

                var storage = _unitOfWork
                .GetDbRepository<IDbDirectoryRepository>()
                .Find(x => x.FullPath == virtualStoragePath).FirstOrDefault()
                ?? throw new DirectoryDoesNotExistException();

                fileStructure.Directory = storage;
                _unitOfWork.GetFsRepository<IFSFileManager>().Move(newFileFullPath, fullFilePath);
                _unitOfWork.Commit();
            }
            catch (FSRepositoryArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                throw new ServiceArgumentException(e.Message);
            }
            catch (DbRepositoryArgumentException e)
            {
                Log.Logger.Debug(e.Message);
                _unitOfWork.GetFsRepository<IFSFileManager>().MoveRollback(newFileFullPath, fullFilePath);
                throw new ServiceArgumentException(e.Message);
            }
        }

        public void Remove(string path)
        {
            try
            {
                var currentDirectory = (_unitOfWork
                    .GetFsRepository<IFSDirectoryManager>())
                    .GetCurrentPath();

                var fileName = GetFileName(path);
                var virtualDirectoryPath = GetVirtualDirectoryPath(path, currentDirectory);
                var fullFilePath = path.Replace("~", currentDirectory);

                var fileStructure = _unitOfWork
                .GetDbRepository<IDbDirectoryRepository>()
                .EagerFind(x => x.FullPath == virtualDirectoryPath, x => x.Files)
                .FirstOrDefault()?.Files.FirstOrDefault(x => x.Name + x.Extension == fileName)
                ?? throw new FileDoesNotExistException();

                _unitOfWork.GetDbRepository <IDbFileRepository>().Remove(fileStructure);
                _unitOfWork.GetFsRepository<IFSFileManager>().Remove(fullFilePath);
                _unitOfWork.Commit();
            }
            catch (FSRepositoryArgumentException e)
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

        public FileStructureDto GetFileByPath(string path)
        {
            var index = path.LastIndexOf('\\')+1;
            
            var fileName = path.Substring(index, path.Length-index);
            var storageDirectoryPath = path.Substring(0, path.LastIndexOf('\\'));
            return _mapper.Map<FileStructureDto>(_unitOfWork
                .GetDbRepository<IDbDirectoryRepository> ()
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
