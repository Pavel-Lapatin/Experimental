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
using System;
using NetMastery.Lab05.FileManager.UI;
using System.IO;

namespace NetMastery.Lab05.FileManager.Bl.Servicies
{
    public class FileService : BusinessService, IFileService
    {
        private readonly string _currentDirectory;

        #region Constructors
        public FileService(IUnitOfWork unitOfWork, 
                           IMapper mapper, 
                           IUserContext userContext) : base(unitOfWork, mapper, userContext)
        {
            _currentDirectory = _unitOfWork.GetFileSystemManager<IDirectoryManager>().GetCurrentPath();
        }
        #endregion

        #region FileServiceAPI

        public void Upload(string pathToFile, string pathToStorage)
        {
            if (pathToStorage == null || pathToFile == null)
            {
                Log.Logger.Debug("FileService-->Upload-->Input is null");
                throw new ArgumentNullException();
            }
            if(pathToFile.IsInTheVirtualStorage(_currentDirectory) 
                || !pathToStorage.HasAccessToVirtualStorage(_userContext.RootDirectory))
            {
                throw new BusinessException("Access is denied");
            }

            var newFile = _unitOfWork
                .GetFileSystemManager<IFileManager>()
                .GetFileInfo(pathToFile) 
                ?? throw new BusinessException($"File with path\"{pathToFile}\" doesn't exist");

            var directory = _unitOfWork
                .Get<IDirectoryRepository>()
                .FindByPath(pathToStorage)
                ?? throw new BusinessException($"Directory with path\"{pathToStorage}\" doesn't exist in virtual storage");

            newFile.Directory = directory;

            if (!HasEnoughFreeSpace(pathToStorage, newFile.FileSize))
            {
                throw new BusinessException("Free space is not enough");
            }
            var fullPathToNewFile = pathToStorage.TransformToFullPath(_currentDirectory) + '\\' + newFile.Name;
            try
            {
                _unitOfWork.Get<IFileRepository>().Add(newFile);
                _unitOfWork.GetFileSystemManager<IFileManager>().Copy(fullPathToNewFile, pathToFile);
                _unitOfWork.Commit();
            }
            catch (DataException e)
            {
                Log.Logger.Debug(e.Message);
                _unitOfWork.GetFileSystemManager<IFileManager>().Remove(fullPathToNewFile);
                throw;
            }
        }

        public void Download(string pathToFile, string pathToStorage)
        {
            if(pathToFile == null || pathToStorage == null)
            {
                Log.Logger.Debug("FileService-->Download-->Input is null");
                throw new ArgumentNullException();
            }
            if (pathToStorage.IsInTheVirtualStorage(_currentDirectory)
                || !pathToFile.HasAccessToVirtualStorage(_userContext.RootDirectory))
            {
                throw new BusinessException("Access is denied");
            }

            var fileName = pathToFile.GetFileName();
            var fileDirectoryPath = pathToFile.GetDirectoryPath();

            var fileForDownload = (_unitOfWork
                .GetFileSystemManager<IFileManager>())
                .GetFileInfo(pathToFile.TransformToFullPath(_currentDirectory))
                ?? throw new BusinessException($"File with path\"{pathToFile}\" doesn't exist");
           
            var fileInfo = _unitOfWork
            .Get<IDirectoryRepository>()
            .FindByPathEagerLoadingFiles(fileDirectoryPath)?
            .Files.FirstOrDefault(x => x.Name == fileName)
            ?? throw new BusinessException($"File with path\"{pathToFile}\" doesn't exist");

            if (!IsFileValid(fileForDownload, fileInfo))
            {
                throw new BusinessException("File is damaged");
            }
            var pathToNewFile = pathToStorage + '\\' + fileName;

            var pathToTheDownloadFile = pathToFile.TransformToFullPath(_currentDirectory);
            try
            {
                fileInfo.DownloadsNumber++;
                _unitOfWork.GetFileSystemManager<IFileManager>().Copy(pathToNewFile, pathToFile.TransformToFullPath(_currentDirectory));
                _unitOfWork.Commit();
            }
            catch (DataException e)
            {
                Log.Logger.Debug(e.Message);
                _unitOfWork.GetFileSystemManager<IFileManager>().Remove(pathToNewFile);
                throw;
            }
        }

        public void Move(string pathFrom, string pathTo)
        {
            if (pathFrom == null || pathTo == null)
            {
                Log.Logger.Debug("FileService--Move-->Input is null");
                throw new ArgumentNullException();
            }

            if (!pathFrom.HasAccessToVirtualStorage(_userContext.RootDirectory)
                || !pathTo.HasAccessToVirtualStorage(_userContext.RootDirectory))
            {
                throw new BusinessException("Access is denied");
            }

            var currentDirectory = (_unitOfWork
                    .GetFileSystemManager<IDirectoryManager>())
                    .GetCurrentPath();

            var fileName = pathFrom.GetFileName();
            var fileDirectoryPath = pathFrom.GetDirectoryPath();

            var fileStructure = _unitOfWork
            .Get<IDirectoryRepository>()
            .FindByPathEagerLoadingFiles(fileDirectoryPath)?
            .Files.FirstOrDefault(x => x.Name == fileName)
            ?? throw new BusinessException($"File with path\"{pathFrom}\" doesn't exist");

            var storage = _unitOfWork
            .Get<IDirectoryRepository>()
            .Find(x => x.FullPath == pathTo).FirstOrDefault()
            ?? throw new BusinessException($"Directory with path\"{pathTo}\" doesn't exist in virtual storage");
            var newFilePath = (pathTo + '\\' + fileName).TransformToFullPath(_currentDirectory);
            var oldFilePath = pathFrom.TransformToFullPath(_currentDirectory);
            fileStructure.Directory = storage;
            try
            {
                _unitOfWork.GetFileSystemManager<IFileManager>()
                    .Move(newFilePath, oldFilePath);

                _unitOfWork.Commit();
            }
            catch (DataException e)
            {
                Log.Logger.Debug(e.Message);
                _unitOfWork.GetFileSystemManager<IFileManager>().Move(oldFilePath, newFilePath);
                throw;
            }
        }

        public void Remove(string path)
        {
            if (path == null)
            {
                Log.Logger.Debug("FileService--Remove-->Input is null");
                throw new ArgumentNullException();
            }

            if (!path.HasAccessToVirtualStorage(_userContext.RootDirectory))

            {
                throw new BusinessException("Access is denied");
            }

            var fileName = path.GetFileName();
            var fileDirectoryPath = path.GetDirectoryPath();
      
            var fileStructure = _unitOfWork
            .Get<IDirectoryRepository>()
            .FindByPathEagerLoadingFiles(fileDirectoryPath)?
            .Files.FirstOrDefault(x => x.Name == fileName)
            ?? throw new BusinessException($"File with path\"{path}\" doesn't exist");
            try
            {
                _unitOfWork.Get<IFileRepository>().Remove(fileStructure);
                _unitOfWork.GetFileSystemManager<IFileManager>().Remove(path.TransformToFullPath(_currentDirectory));
                _unitOfWork.Commit();
            }
            catch (DataException e)
            {
                Log.Logger.Debug(e.Message);
                throw;
            }
        }

        public FileStructureDto GetFileByPath(string path)
        {
            if (path == null)
            {
                Log.Logger.Debug("FileService--Remove-->Input is null");
                throw new ArgumentNullException();
            }

            if (!path.HasAccessToVirtualStorage(_userContext.RootDirectory))
            {
                throw new BusinessException("Access is denied");
            }

            var fileName = path.GetFileName();
            var fileDirectoryPath = path.GetDirectoryPath();

            return _mapper.Map<FileStructureDto>(_unitOfWork
                .Get<IDirectoryRepository> ()
                .FindByPathEagerLoadingFiles(fileDirectoryPath)?
                .Files.FirstOrDefault(x => x.Name == fileName)
                ?? throw new BusinessException($"File with path\"{path}\" doesn't exist"));
        }
        
        #endregion

        private bool HasEnoughFreeSpace(string virtualPath, long fileSize)
        {
            var account = _unitOfWork.Get<IAccountRepository>().FindByLogin(_userContext.Login) 
                ?? throw new ArgumentNullException();
            if (account.MaxStorageSize - account.CurentStorageSize > fileSize)
            {
                account.CurentStorageSize += fileSize;
                return true;
            }
            return false;
        }

        private bool IsFileValid(FileStructure fileInfo, FileStructure fileStructure)
        {
            if (fileStructure.FileSize == fileInfo.FileSize)
            {
                return true;
            }
            return false;
        }
    }
}
