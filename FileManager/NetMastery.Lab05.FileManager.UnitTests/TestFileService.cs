using Moq;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.Bl.Servicies;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.Domain;
using NUnit.Framework;

namespace NetMastery.Lab05.FileManager.UnitTests
{
    [TestFixture]
    public class TestFileService
    {
        [Test]
        [TestCase(null, "parameter")]
        [TestCase("parameter", null)]
        public void When_InputParamentrsIsNull_Expected_ServiceArgumentNullException(string pathToStorage, string pathToFile)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns("E\\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            //Act
            var fileService = new FileService(unitOfWork.Object, null);
            //Assert
            Assert.That(() => fileService.Upload(pathToStorage, pathToFile),
               Throws.TypeOf<ServiceArgumentNullException>());
            Assert.That(() => fileService.Download(pathToFile, pathToStorage),
              Throws.TypeOf<ServiceArgumentNullException>());
        }

        [Test]
        [TestCase("pathToStoreage", "pathToFile")]
        public void When_UploadedFileDoesNotExist_Expected_FileDoesNotExistException(string pathToStorage, string pathToFile)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var fileManager = new Mock<IFileManager>();
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns("E\\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IFileManager>()).Returns(fileManager.Object);
            fileManager.Setup(x => x.GetFileInfo(It.IsAny<string>()))
                .Returns((FileStructure)null);
            //Act
            var fileService = new FileService(unitOfWork.Object, null);
            //Assert
            Assert.That(() => fileService.Upload(pathToStorage, pathToFile),
               Throws.TypeOf<FileDoesNotExistException>());
        }

        [Test]
        [TestCase("pathToStoreage", "pathToFile")]
        public void When_StorageForUploadedingDoesNotExist_Expected_DirectoryDoesNotExistException(string pathToStorage, string pathToFile)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var fileManager = new Mock<IFileManager>();
            var directoryManager = new Mock<IDirectoryManager>();
            var directoryRepository = new Mock<IDirectoryRepository>();
            fileManager.Setup(x => x.GetFileInfo(It.IsAny<string>())).Returns(new FileStructure());
            directoryManager.Setup(x => x.GetCurrentPath()).Returns("E\\CommonStorage");
            directoryRepository.Setup(x => x.FindByPath(It.IsAny<string>())).Returns((DirectoryStructure)null);
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IFileManager>()).Returns(fileManager.Object);
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(directoryRepository.Object);
            //Act
            var fileService = new FileService(unitOfWork.Object, null);
            //Assert
            Assert.That(() => fileService.Upload(pathToStorage, pathToFile),
               Throws.TypeOf<DirectoryDoesNotExistException>());
        }

        [Test]
        [TestCase("pathToStoreage", "pathToFile")]
        public void When_IsNotEnoughFreeSpace_Expected_ServiceArgumentException(string pathToStorage, string pathToFile)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var fileManager = new Mock<IFileManager>();
            var directoryManager = new Mock<IDirectoryManager>();
            var directoryRepository = new Mock<IDirectoryRepository>();
            var accountRepository = new Mock<IAccountRepository>();
            fileManager.Setup(x => x.GetFileInfo(It.IsAny<string>())).Returns(new FileStructure {FileSize = 1024 });
            directoryManager.Setup(x => x.GetCurrentPath()).Returns("E\\CommonStorage");
            directoryRepository.Setup(x => x.FindByPath(It.IsAny<string>())).Returns(new DirectoryStructure());
            accountRepository.Setup(x => x.HasEnoughFreeSpace(It.IsAny<string>(), It.IsAny<long>())).Returns(false);
            unitOfWork.Setup(x => x.Get<IAccountRepository>()).Returns(accountRepository.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IFileManager>()).Returns(fileManager.Object);
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(directoryRepository.Object);
            //Act
            var fileService = new FileService(unitOfWork.Object, null);
            //Assert
            Assert.That(() => fileService.Upload(pathToStorage, pathToFile),
               Throws.TypeOf<ServiceArgumentException>());
        }

        [Test]
        [TestCase("pathToStoreage", "pathToFile")]
        public void When_UploadSeccessfullyExecuted_Expected_ExecutingCommitMethod(string pathToStorage, string pathToFile)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Commit());
            var fileManager = new Mock<IFileManager>();
            fileManager.Setup(x => x.GetFileInfo(It.IsAny<string>())).Returns(new FileStructure { FileSize = 1024 });
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns("E\\CommonStorage");
            var directoryRepository = new Mock<IDirectoryRepository>();
            directoryRepository.Setup(x => x.FindByPath(It.IsAny<string>())).Returns(new DirectoryStructure());
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(x => x.HasEnoughFreeSpace(It.IsAny<string>(), It.IsAny<long>())).Returns(true);
            var fileRepository = new Mock<IFileRepository>();
            fileRepository.Setup(x => x.Add(It.IsAny<FileStructure>()));

            unitOfWork.Setup(x => x.Get<IAccountRepository>()).Returns(accountRepository.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IFileManager>()).Returns(fileManager.Object);
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(directoryRepository.Object);
            unitOfWork.Setup(x => x.Get<IFileRepository>()).Returns(fileRepository.Object);
            //Act
            var fileService = new FileService(unitOfWork.Object, null);
            fileService.Upload(pathToStorage, pathToFile);
            //Assert
            fileRepository.Verify(x=>x.Add(It.IsAny<FileStructure>()), Times.Once);
            fileManager.Verify(x => x.Copy(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            unitOfWork.Verify(x => x.Commit());
        }

        [Test]
        [TestCase("pathToStoreage", "pathToFile")]
        public void When_DownloadFileDoesNotExist_Expected_FileDoesNotExistException(string pathToStorage, string pathToFile)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var fileManager = new Mock<IFileManager>();
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns("E\\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IFileManager>()).Returns(fileManager.Object);
            fileManager.Setup(x => x.GetFileInfo(It.IsAny<string>()))
                .Returns((FileStructure)null);
            //Act
            var fileService = new FileService(unitOfWork.Object, null);
            //Assert
            Assert.That(() => fileService.Upload(pathToStorage, pathToFile),
               Throws.TypeOf<FileDoesNotExistException>());
        }

    }
}
