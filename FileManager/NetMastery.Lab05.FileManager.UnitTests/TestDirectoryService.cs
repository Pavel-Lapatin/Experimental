using AutoMapper;
using Moq;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.Bl.Servicies;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI;
using NUnit.Framework;
using System;
using System.Collections.Generic;


namespace NetMastery.Lab05.FileManager.UnitTests
{
    [TestFixture]
    public class TestDirectoryService
    {
        #region Test input parameters
        [Test]
        [TestCase(null, "something")]
        [TestCase("something", null)]
        public void When_AddInputNull_Expected_ArgumentNullException(string parametr1, string parametr2)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            var autoMapper = new Mock<IMapper>();
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            Assert.That(() => directoryService.Add(parametr1, parametr2),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCase(null, "something")]
        [TestCase("something", null)]
        public void When_GetInfoInputNull_Expected_ArgumentNullException(string parametr1, string parametr2)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            var autoMapper = new Mock<IMapper>();
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            Assert.That(() => directoryService.GetInfoByPath(null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCase(null, "something")]
        [TestCase("something", null)]
        public void When_MoveInputNull_Expected_ArgumentNullException(string parametr1, string parametr2)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            var autoMapper = new Mock<IMapper>();
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            
            Assert.That(() => directoryService.Move(parametr1, parametr2),
                Throws.TypeOf<ArgumentNullException>());
           
        }

        [Test]
        [TestCase(null, "something")]
        [TestCase("something", null)]
        public void When_RemoveInputNull_Expected_ArgumentNullException(string parametr1, string parametr2)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            var autoMapper = new Mock<IMapper>();
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            
            Assert.That(() => directoryService.Remove(null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCase(null, "something")]
        [TestCase("something", null)]
        public void When_SearchInputNull_Expected_ArgumentNullException(string parametr1, string parametr2)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            var autoMapper = new Mock<IMapper>();
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            
            Assert.That(() => directoryService.Search(parametr1, parametr2),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCase(null, "something")]
        [TestCase("something", null)]
        public void When_ShowContextInputNull_Expected_ArgumentNullException(string parametr1, string parametr2)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            var autoMapper = new Mock<IMapper>();
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);

            Assert.That(() => directoryService.ShowContent(null),
                Throws.TypeOf<ArgumentNullException>());
        }
        #endregion

        #region Test AddDirectory method
        [Test]
        [TestCase("~adminRoot\\NotExistingPath", "name")]
        public void When_DirectoryDoseNotExistAdd_Expected_BusinessException(string path, string name)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var Repository = new Mock<IDirectoryRepository>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~adminRoot");
            Repository.Setup(u => u.FindByPath(It.IsAny<string>()))
                .Returns((DirectoryStructure)null);
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(Repository.Object);
            var autoMapper = new Mock<IMapper>();
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.Add(path, name),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~\\adminRoot", "newFolder")]
        public void When_AccessToPathDeniedAdd_Expected_BusinessException(string path, string name)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\pashaRoot");
            var autoMapper = new Mock<IMapper>();
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.Add(path, name),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~\\adminRoot", "newFolder")]
        public void When_DirectoryAlreadyExist_Expect_BusinessException(string path, string name)
        {
            //Arrange
            var data = new List<DirectoryStructure>(new[]
                {
                    new DirectoryStructure{FullPath = "~\\adminRoot\\newFolder"},
                    new DirectoryStructure{FullPath = "~\\adminRoot\\newFolder2"}
                });
            var unitOfWork = new Mock<IUnitOfWork>();
            var repository = new Mock<IDirectoryRepository>();
            var autoMapper = new Mock<IMapper>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\adminRoot");
            repository.Setup(u => u.FindByPath(It.Is<string>(s => 
                s == path + '\\' + name || s == path))).Returns(new DirectoryStructure());

            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(repository.Object);
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.Add(path, name),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~\\adminRoot", "newFolder")]
        public void When_AddDirectorySeccessfully_Expected_ExecuteAddMethod(string path, string name)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Commit());
            var repository = new Mock<IDirectoryRepository>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\adminRoot");
            repository.Setup(u => u.FindByPath(It.Is<string>(s => s == "~\\adminRoot")))
                .Returns(new DirectoryStructure { DirectoryId = 1, FullPath = "~\\adminRoot" });
            repository.Setup(u => u.FindByPath(It.Is<string>(s => s != "~\\adminRoot")))
               .Returns((DirectoryStructure)null);
            repository.Setup(u => u.Add(It.IsAny<DirectoryStructure>()));
            var directoryManager = new Mock<IDirectoryManager>();
            directoryManager.Setup(u => u.AddFolder(It.IsAny<string>(), It.IsAny<string>()));
            var autoMapper = new Mock<IMapper>();
            autoMapper.Setup(x => x.Map<DirectoryStructure>(It.IsAny<DirectoryStructureDto>()))
                .Returns(new DirectoryStructure { FullPath = path + '\\' + name });
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(repository.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            directoryService.Add(path, name);
            //Assert
            repository.Verify(u => u.Add(It.IsAny<DirectoryStructure>()), Times.Once);
            directoryManager.Verify(u => u.AddFolder(path, name), Times.Once);
            unitOfWork.Verify(u => u.Commit(), Times.Once);
        }


        #endregion

        #region MoveDirectory method

        [Test]
        [TestCase("~\\adminRoot\\Folder1", "~\\adminRoot\\Folder2")]
        [TestCase("~\\adminRoot\\Folder2", "~\\adminRoot\\Folder1")]
        public void When_DirectoryDoseNotExistMove_Expected_BusinessException(string path, string name)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var repository = new Mock<IDirectoryRepository>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\adminRoot");
            repository.Setup(u => u.FindByPath(It.Is<string>(s => s == "~adminRoot\\Folder1")))
                .Returns(new DirectoryStructure());
            repository.Setup(u => u.FindByPath(It.Is<string>(s => s != "~adminRoot\\Folder1")))
                .Returns((DirectoryStructure) null);
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(repository.Object);
            var autoMapper = new Mock<IMapper>();
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.Move(path, name),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~\\adminRoot\\Folder1", "~\\pashaRoot\\Folder2")]
        [TestCase("~\\pashaRoot\\Folder2", "~\\adminRoot\\Folder1")]
        public void When_AccessToPathDeniedMove_Expected_BusinessException(string path, string name)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\pashaRoot");
            var autoMapper = new Mock<IMapper>();
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.Move(path, name),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~\\adminRoot\\Folder1", "~\\adminRoot\\Folder1\\Folder2")]
        public void When_MovedToSubFolder_Expected_BusinessException(string path, string name)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\adminRoot");
            var autoMapper = new Mock<IMapper>();
            var repository = new Mock<IDirectoryRepository>();
            repository.Setup(u => u.FindByPathEagerLoadingParentDirectory(It.Is<string>(s => 
                s == "~\\adminRoot\\Folder1")))
                .Returns(new DirectoryStructure { FullPath = "~\\adminRoot\\Folder1" });
            repository.Setup(u => u.FindByPath(It.Is<string>(s => s == "~\\adminRoot\\Folder1\\Folder2")))
                .Returns(new DirectoryStructure { FullPath = "~\\adminRoot\\Folder1\\Folder2"});
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(repository.Object);
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.Move(path, name),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~\\adminRoot\\folder1", "~\\adminRoot\\folder2")]
        public void When_MoveDirectorySuccessfully_Expect_ExecutionOfMoveMethod(string pathFrom, string pathTo)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var repository = new Mock<IDirectoryRepository>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\adminRoot");
            var directoryManager = new Mock<IDirectoryManager>();
            var autoMapper = new Mock<IMapper>();
            repository.Setup(u => u.FindByPath(It.Is<string>(s => s == "~\\adminRoot\\folder2")))
                .Returns(new DirectoryStructure { FullPath = pathTo });
            repository.Setup(u => 
                u.FindByPathEagerLoadingParentDirectory(It.Is<string>(s => s == "~\\adminRoot\\folder1")))
                .Returns(new DirectoryStructure
                {
                    ParentDirectory = new DirectoryStructure
                    {
                        FullPath = "~\\adminRoot"
                    },
                    FullPath = pathFrom
                });
            directoryManager.Setup(u => u.Move(It.IsAny<string>(), It.IsAny<string>()));
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(repository.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            unitOfWork.Setup(x => x.Commit());
            //Act
            new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object).Move(pathFrom, pathTo);
            //Assert
            directoryManager.Verify(u => u.Move(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            unitOfWork.Verify(u => u.Commit());
        }

        [Test]
        [TestCase("~\\adminRoot\\folder1", "~\\adminRoot\\folder2")]
        public void When_DirectoryMoved_Expect_ChildrenDirectoriesChangeFullPath(string pathFrom, string pathTo)
        {
            var data = new[]
            {
                new DirectoryStructure{ FullPath = "~\\adminRoot\\folder1\\Test1"},
                new DirectoryStructure{ FullPath = "~\\adminRoot\\folder1\\Test3"},
                new DirectoryStructure{ FullPath = "~\\adminRoot\\folder1\\Test4\\Test5"}
            };
            var unitOfWork = new Mock<IUnitOfWork>();
            var repository = new Mock<IDirectoryRepository>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\adminRoot");
            var directoryManager = new Mock<IDirectoryManager>();
            var autoMapper = new Mock<IMapper>();
            repository.Setup(u => u.FindByPath(It.Is<string>(s => s == "~\\adminRoot\\folder2")))
                .Returns(new DirectoryStructure { FullPath = pathTo });
            repository.Setup(u =>
                u.FindByPathEagerLoadingParentDirectory(It.Is<string>(s => s == "~\\adminRoot\\folder1")))
                .Returns(new DirectoryStructure
                {
                    ParentDirectory = new DirectoryStructure
                    {
                        FullPath = "~\\adminRoot"
                    },
                    FullPath = pathFrom
                });
            repository.Setup(x => x.FindDirectoriesWhichContainPath(It.IsAny<string>())).Returns(data);

            directoryManager.Setup(u => u.Move(It.IsAny<string>(), It.IsAny<string>()));
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(repository.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(directoryManager.Object);
            unitOfWork.Setup(x => x.Commit());
            //Act
            new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object).Move(pathFrom, pathTo);
            //Assert
            Assert.AreEqual(data[0].FullPath, "~\\adminRoot\\folder2\\folder1\\Test1");
            Assert.AreEqual(data[1].FullPath, "~\\adminRoot\\folder2\\folder1\\Test3");
            Assert.AreEqual(data[2].FullPath, "~\\adminRoot\\folder2\\folder1\\Test4\\Test5");
        }

        #endregion

        #region RemoveDirectory method

        [Test]
        [TestCase("~adminRoot\\NotExistingPath")]
        public void When_DirectoryDoseNotExistRemove_Expected_BusinessException(string path)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var Repository = new Mock<IDirectoryRepository>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~adminRoot");
            Repository.Setup(u => u.FindByPath(It.IsAny<string>()))
                .Returns((DirectoryStructure)null);
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(Repository.Object);
            var autoMapper = new Mock<IMapper>();
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.Remove(path),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~\\adminRoot")]
        public void When_AccessToPathDeniedRemove_Expected_BusinessException(string path)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\pashaRoot");
            var autoMapper = new Mock<IMapper>();
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.Remove(path),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~adminRoot\\folder2")]
        public void When_DirectoryRemovedSeccessfully_Expect_RemoveMethodExucutes(string path)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var Repository = new Mock<IDirectoryRepository>();
            var DirectoryManager = new Mock<IDirectoryManager>();
            var fdFileRepository = new Mock<IFileRepository>();
            var autoMapper = new Mock<IMapper>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~adminRoot");
            Repository.Setup(u => u.FindByPathEagerLoadingFiles(It.Is<string>(s => s == path)))
                .Returns(new DirectoryStructure
                {
                    FullPath = path,
                    Files = new[] 
                    {
                        new FileStructure(),
                        new FileStructure()
                    }
                });

            Repository.Setup(u => u.Remove(It.IsAny<DirectoryStructure>()));
            DirectoryManager.Setup(u => u.Remove(It.IsAny<string>()));
            fdFileRepository.Setup(u => u.RemoveRange(It.IsAny<ICollection<FileStructure>>()));

            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(Repository.Object);
            unitOfWork.Setup(x => x.Get<IFileRepository>()).Returns(fdFileRepository.Object);
            unitOfWork.Setup(x => x.GetFileSystemManager<IDirectoryManager>()).Returns(DirectoryManager.Object);
            unitOfWork.Setup(x => x.Commit());
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            directoryService.Remove(path);
            //Assert
            Repository.Verify(u => u.Remove(It.IsAny<DirectoryStructure>()), Times.AtLeastOnce);
            DirectoryManager.Verify(u => u.Remove(It.IsAny<string>()), Times.Once);
            fdFileRepository.Verify(u => u.RemoveRange(It.IsAny<ICollection<FileStructure>>()), Times.AtLeastOnce);
            unitOfWork.Verify(u => u.Commit());
        }

        #endregion

        #region Search method
        [Test]
        [TestCase("~\\adminRoot\\folder", "pattern")]
        public void When_DirectoryDoseNotExistRemove_Expected_BusinessException(string path, string pattern)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var Repository = new Mock<IDirectoryRepository>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\adminRoot");
            Repository.Setup(u => u.FindByPath(It.IsAny<string>()))
                .Returns((DirectoryStructure)null);
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(Repository.Object);
            var autoMapper = new Mock<IMapper>();
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.Search(path, pattern),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~\\adminRoot", "pattern")]
        public void When_AccessToPathDeniedRemove_Expected_BusinessException(string path, string pattern)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\pashaRoot");
            var autoMapper = new Mock<IMapper>();
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.Search(path, pattern),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~adminRoot\\fol2", "File1")]
        public void When_SearchSeccessfullyExecuted_Expect_ReturnDirectoryStructureDto(string path, string pattern)
        {
            var rootDirectory = new DirectoryStructure
            {
                Name = "fol2",
                FullPath = path,
                ChildrenDirectories = new[]
                {
                    new DirectoryStructure
                    {
                        Name = "folder1",
                        FullPath = path+"\\folder1",
                        ChildrenDirectories = new []
                        {
                            new DirectoryStructure
                            {
                                Name = "Folder2",
                                FullPath = path+"\\folder1"+"\\Folder2",
                                Files = new []
                                {
                                    new FileStructure
                                    {
                                        Name = "File1.txt",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File2.html",
                                        Extension = ".html"
                                    }
                                }
                            }
                        } ,
                        Files = new []
                                {
                                    new FileStructure
                                    {
                                        Name = "File11.txt",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File12.html",
                                        Extension = ".html"
                                    }
                                }
                    },
                    new DirectoryStructure
                    {
                        Name = "folder2",
                        FullPath = path+"\\folder2",
                        ChildrenDirectories = new []
                        {
                            new DirectoryStructure
                            {
                                Name = "Folder3",
                                FullPath = path+"\\folder2"+"\\Folder3",
                                Files = new []
                                {
                                    new FileStructure
                                    {
                                        Name = "File31.txt",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File32.html",
                                        Extension = ".html"
                                    }
                                }
                            }
                        },
                        Files = new []
                                {
                                    new FileStructure
                                    {
                                        Name = "File21.txt",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File22.html",
                                        Extension = ".html"
                                    }
                                }
                    }

                },
                Files = new[]
                {
                    new FileStructure
                    {
                        Name = "File1.txt",
                        Extension = ".txt"
                    },
                    new FileStructure
                    {
                        Name = "File2.html",
                        Extension = ".html"
                    }
                }
            };
            var unitOfWork = new Mock<IUnitOfWork>();
            var Repository = new Mock<IDirectoryRepository>();
            var autoMapper = new Mock<IMapper>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~adminRoot");
            Repository.Setup(u => u.FindByPathEagerLoadingFull(It.Is<string>(s => s == path)))
                .Returns(rootDirectory);

            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(Repository.Object);

            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            var res = directoryService.Search(path, pattern);
            //Assert
            var actual = new List<string>(new[] {
                path + "\\folder1\\Folder2\\File1.txt",
                path + "\\folder1\\File11.txt",
                path + "\\folder1\\File12.html",
                path +"\\File1.txt"
                });
            CollectionAssert.AreEqual(res, actual);
        }

        #endregion

        #region ShowContent method
        [Test]
        [TestCase("~adminRoot\\NotExistingPath")]
        public void When_DirectoryDoseNotExistShow_Expected_BusinessException(string path)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var Repository = new Mock<IDirectoryRepository>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~adminRoot");
            Repository.Setup(u => u.FindByPath(It.IsAny<string>()))
                .Returns((DirectoryStructure)null);
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(Repository.Object);
            var autoMapper = new Mock<IMapper>();
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.ShowContent(path),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~\\adminRoot")]
        public void When_AccessToPathDeniedShow_Expected_BusinessException(string path)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\pashaRoot");
            var autoMapper = new Mock<IMapper>();
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.ShowContent(path),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~adminRoot\\fol2")]
        public void When_ShowContextSeccessfullyExecuted_Expect_ReturnDirectoryStructureDto(string path)
        {
            var rootDirectory = new DirectoryStructure
            {
                Name = "fol2",
                FullPath = path,
                ChildrenDirectories = new[]
                {
                    new DirectoryStructure
                    {
                        Name = "folder1",
                        FullPath = path+"\\folder1",
                        ChildrenDirectories = new []
                        {
                            new DirectoryStructure
                            {
                                Name = "Folder2",
                                FullPath = path+"\\folder1"+"\\Folder2",
                                Files = new []
                                {
                                    new FileStructure
                                    {
                                        Name = "File1.txt",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File2.html",
                                        Extension = ".html"
                                    }
                                }
                            }
                        } ,
                        Files = new []
                                {
                                    new FileStructure
                                    {
                                        Name = "File11.txt",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File12.html",
                                        Extension = ".html"
                                    }
                                }
                    },
                    new DirectoryStructure
                    {
                        Name = "folder2",
                        FullPath = path+"\\folder2",
                        ChildrenDirectories = new []
                        {
                            new DirectoryStructure
                            {
                                Name = "Folder3",
                                FullPath = path+"\\folder2"+"\\Folder3",
                                Files = new []
                                {
                                    new FileStructure
                                    {
                                        Name = "File31.txt",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File32.html",
                                        Extension = ".html"
                                    }
                                }
                            }
                        },
                        Files = new []
                                {
                                    new FileStructure
                                    {
                                        Name = "File21.txt",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File22.html",
                                        Extension = ".html"
                                    }
                                }
                    }

                },
                Files = new[]
                {
                    new FileStructure
                    {
                        Name = "File1.txt",
                        Extension = ".txt"
                    },
                    new FileStructure
                    {
                        Name = "File2.html",
                        Extension = ".html"
                    }
                }
            };
            var unitOfWork = new Mock<IUnitOfWork>();
            var Repository = new Mock<IDirectoryRepository>();
            var autoMapper = new Mock<IMapper>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~adminRoot");
            Repository.Setup(u => u.FindByPathEagerLoadingFull(It.Is<string>(s => s == path)))
                .Returns(rootDirectory);

            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(Repository.Object);

            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            var res = directoryService.ShowContent(path);
            //AssertSk
            var actualFiles = new List<string>(new[]
            {
                "File1.txt",
                "File2.html"
                });
            var actualDirectories = new List<string>(new[]
            {
                "folder1",
                "folder2",
            });

            CollectionAssert.AreEqual(res["Directories"], actualDirectories);
            CollectionAssert.AreEqual(res["Files"], actualFiles);
        }

        #endregion

        #region GetInfo method
        [Test]
        [TestCase("~adminRoot\\NotExistingPath")]
        public void When_DirectoryDoseNotExistGetInfo_Expected_BusinessException(string path)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var Repository = new Mock<IDirectoryRepository>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~adminRoot");
            Repository.Setup(u => u.FindByPath(It.IsAny<string>()))
                .Returns((DirectoryStructure)null);
            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(Repository.Object);
            var autoMapper = new Mock<IMapper>();
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.GetInfoByPath(path),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~\\adminRoot")]
        public void When_AccessToPathDeniedGetInfo_Expected_BusinessException(string path)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~\\pashaRoot");
            var autoMapper = new Mock<IMapper>();
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            //Assert
            Assert.That(() => directoryService.GetInfoByPath(path),
               Throws.TypeOf<BusinessException>());
        }

        [Test]
        [TestCase("~adminRoot\\folder2")]
        public void When_GetInfoByPathSeccessfullyExecuted_Expect_ReturnDirectoryStructureDto(string path)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var Repository = new Mock<IDirectoryRepository>();
            var autoMapper = new Mock<IMapper>();
            var userContext = new Mock<IUserContext>();
            userContext.SetupGet(x => x.RootDirectory).Returns("~adminRoot");
            Repository.Setup(u => u.FindByPath(It.Is<string>(s => s == path)))
                .Returns(new DirectoryStructure());
            autoMapper.Setup(u => u.Map<DirectoryStructureDto>(It.IsAny<DirectoryStructure>()))
                .Returns(new DirectoryStructureDto { FullPath = path });

            unitOfWork.Setup(x => x.Get<IDirectoryRepository>()).Returns(Repository.Object);

            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            var res = directoryService.GetInfoByPath(path);
            //Assert
            Assert.AreEqual(res.FullPath, path);
        }
        #endregion
    }
}
