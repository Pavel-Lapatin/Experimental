using Moq;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.Bl.Servicies;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.UI;
using NUnit.Framework;
using System;

namespace NetMastery.Lab05.FileManager.UnitTests
{
    [TestFixture]
    public class TestFileService
    {
        #region Upload method
        [Test]
        [TestCase(null, "parameter")]
        [TestCase("parameter", null)]
        public void When_InputParamentrsIsNullUpload_Expected_BusinessException(string pathToFile, string pathToStorage)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns(@"E:\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            var userContext = new Mock<IUserContext>();
            //Act
            var fileService = new FileService(unitOfWork.Object, null, userContext.Object);
            //Assert
            Assert.That(() => fileService.Upload(pathToFile, pathToStorage),
               Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCase(@"E:\CommonStorage\file1", @"~\adminRoot\folder1")]
        [TestCase(@"~\adminRoot\folder2\file1", @"~\adminRoot\folder1")]
        [TestCase(@"E:\file1", @"E:\CommonStorage\adminRoot\folder1")]
        [TestCase(@"E:\file1", @"~\pashaRoot\folder1")]
        [TestCase(@"E:\file1", @"E:")]
        public void When_AccessIsDeniedUpload_Expected_BusinessException(string pathToFile, string pathToStorage)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns(@"E:\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns(@"~\adminRoot");
            //Act
            var fileService = new FileService(unitOfWork.Object, null, userContext.Object);
            //Assert
            Assert.That(() => fileService.Upload(pathToFile, pathToStorage),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase(@"E:\file2.html", @"~\adminRoot\Folder1")]
        public void When_UploadedFileDoesNotExist_Expected_BusinessException(string pathToFile, string pathToStorage)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var fileManager = new Mock<IFileManager>();
            var directoryManager = new Mock<IDirectoryManager>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns(@"~\adminRoot");
            directoryManager.Setup(x => x.GetCurrentPath()).Returns(@"E:\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IFileManager>()).Returns(fileManager.Object);
            fileManager.Setup(x => x.GetFileInfo(It.IsAny<string>())).Returns((FileStructure)null);
            //Act
            var fileService = new FileService(unitOfWork.Object, null, userContext.Object);
            //Assert
            Assert.That(() => fileService.Upload(pathToFile, pathToStorage),
               Throws.TypeOf<BusinessException>());
        }


        [Test]
        [TestCase(@"E:\file1", @"~\adminRoot\folder1")]
        public void When_IsNotEnoughFreeSpace_Expected_ServiceArgumentException(string pathToStorage, string pathToFile)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var fileManager = new Mock<IFileManager>();
            var directoryManager = new Mock<IDirectoryManager>();
            var directoryRepository = new Mock<IDirectoryRepository>();
            var accountRepository = new Mock<IAccountRepository>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns(@"~\adminRoot");
            fileManager.Setup(x => x.GetFileInfo(It.IsAny<string>())).Returns(new FileStructure { FileSize = 1024 });
            directoryManager.Setup(x => x.GetCurrentPath()).Returns(@"E:\CommonStorage");
            directoryRepository.Setup(x => x.FindByPath(It.IsAny<string>())).Returns(new DirectoryStructure());
            unitOfWork.Setup(x => x.Get<IAccountRepository>()).Returns(accountRepository.Object);
            accountRepository.Setup(x => x.FindByLogin(It.IsAny<string>())).Returns(new Account
            {
                MaxStorageSize = 1000,
                CurentStorageSize = 0
            });
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IFileManager>()).Returns(fileManager.Object);
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(directoryRepository.Object);
            //Act
            var fileService = new FileService(unitOfWork.Object, null, userContext.Object);
            //Assert
            Assert.That(() => fileService.Upload(pathToStorage, pathToFile),
               Throws.TypeOf<BusinessException>());
        }



        [Test]
        [TestCase(@"E:\file1", @"~\adminRoot\folder1")]
        public void When_UploadSeccessfullyExecuted_Expected_ExecutingCommitMethod(string pathToStorage, string pathToFile)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns(@"~\adminRoot");
            unitOfWork.Setup(x => x.Commit());
            var fileManager = new Mock<IFileManager>();
            fileManager.Setup(x => x.GetFileInfo(It.IsAny<string>())).Returns(new FileStructure { FileSize = 500 });
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns(@"E:\CommonStorage");
            var directoryRepository = new Mock<IDirectoryRepository>();
            directoryRepository.Setup(x => x.FindByPath(It.IsAny<string>())).Returns(new DirectoryStructure());
            var accountRepository = new Mock<IAccountRepository>();
            var fileRepository = new Mock<IFileRepository>();
            fileRepository.Setup(x => x.Add(It.IsAny<FileStructure>()));
            accountRepository.Setup(x => x.FindByLogin(It.IsAny<string>())).Returns(new Account
            {
                MaxStorageSize = 1000,
                CurentStorageSize = 0
            });

            unitOfWork.Setup(x => x.Get<IAccountRepository>()).Returns(accountRepository.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IFileManager>()).Returns(fileManager.Object);
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(directoryRepository.Object);
            unitOfWork.Setup(x => x.Get<IFileRepository>()).Returns(fileRepository.Object);
            //Act
            var fileService = new FileService(unitOfWork.Object, null, userContext.Object);
            fileService.Upload(pathToStorage, pathToFile);
            //Assert
            fileRepository.Verify(x => x.Add(It.IsAny<FileStructure>()), Times.Once);
            fileManager.Verify(x => x.Copy(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            unitOfWork.Verify(x => x.Commit());
        }


        #endregion
        #region Download method
        [Test]
        [TestCase(null, "parameter")]
        [TestCase("parameter", null)]
        public void When_InputParamentrsIsDownlaod_Expected_BusinessException(string pathToFile, string pathToStorage)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns(@"E:\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            var userContext = new Mock<IUserContext>();
            //Act
            var fileService = new FileService(unitOfWork.Object, null, userContext.Object);
            //Assert
            Assert.That(() => fileService.Download(pathToFile, pathToStorage),
               Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCase(@"~\adminRoot\folder1\file1", @"E:\CommonStorage\adminRoot")]
        [TestCase(@"~\adminRoot\folder1\file1", @"~\adminRoot\folder2")]
        [TestCase(@"E:\CommonStorage\adminRoot\folder1\file1", @"E:\")]
        [TestCase(@"~\pashaRoot\folder1\file1", @"E:\")]
        [TestCase(@"E:\file1", @"E:")]
        public void When_AccessIsDeniedDownload_Expected_BusinessException(string pathToFile, string pathToStorage)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns(@"E:\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns(@"~\adminRoot");
            //Act
            var fileService = new FileService(unitOfWork.Object, null, userContext.Object);
            //Assert
            Assert.That(() => fileService.Download(pathToFile, pathToStorage),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase(@"~\adminRoot\Folder1", @"E:\file2.html")]
        public void When_FileDoesNotExistDownload_Expected_BusinessException(string pathToFile, string pathToStorage)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var fileManager = new Mock<IFileManager>();
            var directoryManager = new Mock<IDirectoryManager>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns(@"~\adminRoot");
            directoryManager.Setup(x => x.GetCurrentPath()).Returns(@"E:\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IFileManager>()).Returns(fileManager.Object);
            fileManager.Setup(x => x.GetFileInfo(It.IsAny<string>())).Returns((FileStructure)null);
            //Act
            var fileService = new FileService(unitOfWork.Object, null, userContext.Object);
            //Assert
            Assert.That(() => fileService.Download(pathToFile, pathToStorage),
               Throws.TypeOf<BusinessException>());
        }

        #endregion

        #region Move method
        [Test]
        [TestCase(null, "parameter")]
        [TestCase("parameter", null)]
        public void When_InputParamentrsIsMove_Expected_BusinessException(string pathFrom, string pathTo)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns(@"E:\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            var userContext = new Mock<IUserContext>();
            //Act
            var fileService = new FileService(unitOfWork.Object, null, userContext.Object);
            //Assert
            Assert.That(() => fileService.Move(pathFrom, pathTo),
               Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCase(@"~\adminRoot\folder1\file1", @"E:\CommonStorage\adminRoot")]
        [TestCase(@"~\adminRoot\folder1\file1", @"~\pashaRoot\folder2")]
        [TestCase(@"E:\CommonStorage\adminRoot\folder1\file1", @"E:\")]
        [TestCase(@"~\pashaRoot\folder1\file1", @"E:\")]
        [TestCase(@"E:\file1", @"E:")]
        public void When_AccessIsDeniedMove_Expected_BusinessException(string pathFrom, string pathTo)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns(@"E:\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns(@"~\adminRoot");
            //Act
            var fileService = new FileService(unitOfWork.Object, null, userContext.Object);
            //Assert
            Assert.That(() => fileService.Move(pathFrom, pathTo),
               Throws.TypeOf<BusinessException>());
        }
        #endregion

        #region Remove method
        [Test]
        [TestCase(null)]
        public void When_InputParamentrsIsRemove_Expected_BusinessException(string path)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns(@"E:\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            var userContext = new Mock<IUserContext>();
            //Act
            var fileService = new FileService(unitOfWork.Object, null, userContext.Object);
            //Assert
            Assert.That(() => fileService.Remove(path),
               Throws.TypeOf<ArgumentNullException>());
        }

        [Test]

        [TestCase(@"~\pashaRoot\folder1\file1")]
        [TestCase(@"E:\file1")]
        [TestCase(@"E:\CommonStorage\adminRoot\file1")]
        public void When_AccessIsDeniedRemove_Expected_BusinessException(string path)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(x => x.GetCurrentPath()).Returns(@"E:\CommonStorage");
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns(@"~\adminRoot");
            //Act
            var fileService = new FileService(unitOfWork.Object, null, userContext.Object);
            //Assert
            Assert.That(() => fileService.Remove(path),
               Throws.TypeOf<BusinessException>());
        }

        #endregion
    }
}
