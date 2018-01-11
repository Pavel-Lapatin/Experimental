using AutoMapper;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.Bl.Helpers;
using System.Linq;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.DAL.Repository;
using Serilog;
using System.Data;

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string _currentDirectory;

        #region Constructors
        public FileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentDirectory = _unitOfWork.GetFileSystemManager<IDirectoryManager>().GetCurrentPath();
        }
        #endregion

        #region FileServiceAPI

        public void Upload(string pathToStorage, string pathToFile)
        {
            if (pathToStorage == null || pathToFile == null)
            {
                throw new ServiceArgumentNullException();
            }
            var fullPathToFile = pathToFile.TransformToFullPath(_currentDirectory);
            var virtualPathToStoorage = pathToStorage.TransformToVirtualPath(_currentDirectory);
            var newFile = _unitOfWork
                .GetFileSystemManager<IFileManager>()
                .GetFileInfo(pathToFile) ?? throw new FileDoesNotExistException();
            var directory = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindByPath(virtualPathToStoorage) ?? throw new DirectoryDoesNotExistException();
            newFile.Directory = directory;
            if (!HasEnoughFreeSpace(virtualPathToStoorage, newFile.FileSize))
            {
                throw new ServiceArgumentException("Free space is not enough");
            }
            try
            {
                _unitOfWork.Get<IFileRepository>().Add(newFile);
                var fullPathToNewFile = pathToStorage.TransformToFullPath(_currentDirectory) + '\\' + newFile.Name;
                (_unitOfWork.GetFileSystemManager<IFileManager>()).Copy(fullPathToNewFile, fullPathToFile);
                _unitOfWork.Commit();
            }
            catch (DataException e)
            {
                Log.Logger.Debug(e.Message);
                throw;
            }
        }

        public void Download(string pathToFile, string pathToStorage)
        {
            if(pathToFile == null || pathToStorage == null)
            {
                throw new ServiceArgumentNullException();
            }

            var virtualPathToFile = pathToFile.TransformToVirtualPath(_currentDirectory);
            var fileName = virtualPathToFile.GetFileName();
            var virtualPathToFileDirectory = virtualPathToFile.GetDirectoryPath();

            var fileForDownload = (_unitOfWork
                .GetFileSystemManager<IFileManager>())
                .GetFileInfo(pathToFile) ?? throw new FileDoesNotExistException();

            if (pathToStorage.Contains(_currentDirectory))
            {
                throw new ServiceArgumentException(
                    "For downloading file into storage use upload or move commands, please");
            }
           
            var fileInfo = _unitOfWork
            .Get<IDirectoryRepository>()
            .FindByPathEagerLoadingFiles(virtualPathToFileDirectory)?
            .Files.FirstOrDefault(x => x.Name + x.Extension == fileName)
            ?? throw new FileDoesNotExistException();

            if (!IsFileValid(fileForDownload, fileInfo))
            {
                throw new ServiceArgumentException("File is damaged");
            }

            var pathToNewFile = pathToStorage + '\\' + fileName;
            try
            {
                fileInfo.DownloadsNumber++;
                (_unitOfWork.GetFileSystemManager<IFileManager>()).Copy(pathToNewFile, pathToFile);
                _unitOfWork.Commit();
            }
            catch (DataException e)
            {
                Log.Logger.Debug(e.Message);
                throw;
            }
        }

        private bool IsFileValid(FileStructure fileInfo, FileStructure fileStructure)
        {
            if (fileStructure.FileSize * 1024 == fileInfo.FileSize)
            {
                return true;
            }
            return false;
        }



        public void Move(string pathToFile, string pathToStorage)
        {
                var currentDirectory = (_unitOfWork
                    .GetFileSystemManager<IDirectoryManager>())
                    .GetCurrentPath();

                var fileName = pathToFile.GetFileName();
                var virtualDirectoryPath = pathToFile.GetDirectoryPath().TransformToVirtualPath(_currentDirectory);
                var fullFilePath = pathToFile.Replace("~", currentDirectory);
                var virtualStoragePath = pathToStorage.Replace(currentDirectory, "~");
                var newFileFullPath = virtualStoragePath.Replace("~", currentDirectory) + '\\' + fileName;
            try
            {
                var fileStructure = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindByPathEagerLoadingFiles(virtualDirectoryPath)?
                .Files.FirstOrDefault(x => x.Name + x.Extension == fileName)
                ?? throw new FileDoesNotExistException();

                var storage = _unitOfWork
                .Get<IDirectoryRepository>()
                .Find(x => x.FullPath == virtualStoragePath).FirstOrDefault()
                ?? throw new DirectoryDoesNotExistException();

                fileStructure.Directory = storage;
                _unitOfWork.GetFileSystemManager<IFileManager>().Move(newFileFullPath, fullFilePath);
                _unitOfWork.Commit();
            }
            catch (DataException e)
            {
                Log.Logger.Debug(e.Message);
                _unitOfWork.GetFileSystemManager<IFileManager>().Move(fullFilePath, newFileFullPath);
                throw;
            }
        }

        public void Remove(string path)
        {
            try
            {
                var currentDirectory = (_unitOfWork
                    .GetFileSystemManager<IDirectoryManager>())
                    .GetCurrentPath();

                var fileName = path.GetFileName();
                var virtualDirectoryPath =path.GetDirectoryPath().TransformToVirtualPath(_currentDirectory);
                var fullFilePath = path.Replace("~", currentDirectory);

                var fileStructure = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindByPathEagerLoadingFiles(virtualDirectoryPath)?
                .Files.FirstOrDefault(x => x.Name + x.Extension == fileName)
                ?? throw new FileDoesNotExistException();

                _unitOfWork.Get <IFileRepository>().Remove(fileStructure);
                _unitOfWork.GetFileSystemManager<IFileManager>().Remove(fullFilePath);
                _unitOfWork.Commit();
            }
            catch (DataException e)
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
                .Get<IDirectoryRepository> ()
                .FindByPathEagerLoadingFiles(path)?
                .Files.FirstOrDefault(x => x.Name+x.Extension == fileName));
        }

        private bool HasEnoughFreeSpace(string path, long fileSize)
        {
            var rootFolderName = path.Trim().Split('\\')[1];
            var account = _unitOfWork.Get<IAccountRepository>().FindByRootName(rootFolderName) 
                ?? throw new ServiceArgumentNullException("Account doesn't exist") ;
            if (account.MaxStorageSize - account.CurentStorageSize > fileSize)
            {
                account.CurentStorageSize += fileSize;
                return true;
            }
            return false;
        }
        #endregion
    }
}
