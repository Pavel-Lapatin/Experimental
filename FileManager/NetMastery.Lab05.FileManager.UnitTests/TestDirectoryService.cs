using AutoMapper;
using Moq;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.Bl.Servicies;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Dto;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UnitTests
{
    [TestFixture]
    public class TestDirectoryService
    {

        [SetUp]
        public void Init()
        {

        }

        [TearDown]
        public void Dispose()
        {

        }

        [Test]
        [TestCase(null, "something")]
        [TestCase("something", null)]
        public void When_InputParametersAreNull_Expected_ServiceNullArgumentException(string parametr1, string parametr2)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var autoMapper = new Mock<IMapper>();
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object);
            Assert.That(() => directoryService.Add(parametr1, parametr2),
                Throws.TypeOf<ServiceArgumentNullException>());
            Assert.That(() => directoryService.GetInfoByPath(null),
                Throws.TypeOf<ServiceArgumentNullException>());
            Assert.That(() => directoryService.Move(parametr1, parametr2),
                Throws.TypeOf<ServiceArgumentNullException>());
            Assert.That(() => directoryService.Remove(null),
                Throws.TypeOf<ServiceArgumentNullException>());
            Assert.That(() => directoryService.Search(parametr1, parametr2),
                Throws.TypeOf<ServiceArgumentNullException>());
            Assert.That(() => directoryService.ShowContent(null),
                Throws.TypeOf<ServiceArgumentNullException>());
        }

        [Test]
        [TestCase("ExistingPath", "NotExistingPath")]
        [TestCase("NotExistingPath", "ExistingPath")]
        public void When_DirectoryDosentExist_Expected_ServiceNullArgumentException(string parametr1, string parametr2)
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var DbRepository = new Mock<IDbDirectoryRepository> ();
            DbRepository.Setup(
                u => u.Find(
                    x => x.Name == "ExistingPath"))
                .Returns(new List<DirectoryStructure>(
                    new[]
                    {
                        new DirectoryStructure()
                    }));

            DbRepository.Setup(
                u => u.Find(
                    x => x.Name != "ExistingPath"))
                .Returns(new List<DirectoryStructure>());

            unitOfWork.Setup(x => x.GetDbRepository<IDbDirectoryRepository>()).Returns(DbRepository.Object);
            var autoMapper = new Mock<IMapper>();
            autoMapper.Setup(m => m.Map<DirectoryStructure>(It.Is<Account>(x => x != null)))
                      .Returns(new DirectoryStructure());
            autoMapper.Setup(m => m.Map<DirectoryStructure>(It.Is<Account>(x => x == null)))
                     .Returns((DirectoryStructure)null);
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object);
            //Assert
            Assert.That(() => directoryService.Add("NotExistingPath", "newFolder"),
               Throws.TypeOf<DirectoryDoesNotExistException>());
            Assert.That(() => directoryService.GetInfoByPath("NotExistingPath"),
                Throws.TypeOf<DirectoryDoesNotExistException>());
            Assert.That(() => directoryService.Move(parametr1, parametr2),
                Throws.TypeOf<DirectoryDoesNotExistException>());
            Assert.That(() => directoryService.Remove("NotExistingPath"),
                Throws.TypeOf<DirectoryDoesNotExistException>());
            Assert.That(() => directoryService.Search("NotExistingPath", "something"),
                Throws.TypeOf<DirectoryDoesNotExistException>());
            Assert.That(() => directoryService.ShowContent("NotExistingPath"),
                Throws.TypeOf<DirectoryDoesNotExistException>());

        }

        [Test]
        [TestCase("ExistingPath", "ExistingFolderName")]
        public void When_DirectoryAlreadyExist_Expect_DerictoryExistException(string path, string name)
        {
            //Arrange
            var data = new List<DirectoryStructure>(new[]
                {
                    new DirectoryStructure{FullPath = "ExistingPath\\ExistingFolderName"},
                    new DirectoryStructure{FullPath = "ExistingPath"}
                });

            var unitOfWork = new Mock<IUnitOfWork>();
            var dbRepository = new Mock<IDbDirectoryRepository>();
            var autoMapper = new Mock<IMapper>();
            dbRepository.Setup(
                u => u.FindByPath(It.Is<string>(s=>s == path + '\\'+name)))
                .Returns(new DirectoryStructure());

            unitOfWork.Setup(x => x.GetDbRepository<IDbDirectoryRepository>()).Returns(dbRepository.Object);
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object);
            //Assert
            Assert.That(() => directoryService.Add(path, name),
               Throws.TypeOf<DirectoryExistsException>());
        }

        [Test]
        [TestCase("ParentDirectoryFullPath", "newFolderName")]
        public void When_AddDirectorySeccessfully_Expected_ExecuteAddMethod(string path, string name)
        {
            //Arrange

            var unitOfWork = new Mock<IUnitOfWork>();
            var DbRepository = new Mock<IDbDirectoryRepository>(); 
            DbRepository.Setup(u => u.FindByPath(It.Is<string>(s=>s == "ParentDirectoryFullPath")))
                .Returns(new DirectoryStructure { DirectoryId = 1, FullPath = "ParentDirectoryFullPath" });
            DbRepository.Setup(u => u.FindByPath(It.Is<string>(s => s != "ParentDirectoryFullPath")))
               .Returns((DirectoryStructure)null);
            DbRepository.Setup(u => u.Add(It.IsAny<DirectoryStructure>()));
            var fsDirectoryManager = new Mock<IFSDirectoryManager>();
            fsDirectoryManager.Setup(u => u.AddFolder(It.IsAny<string>(), It.IsAny<string>()));
            var autoMapper = new Mock<IMapper>();
            autoMapper.Setup(x => x.Map<DirectoryStructure>(It.IsAny<DirectoryStructureDto>()))
                .Returns(new DirectoryStructure { FullPath = path + '\\'+name});

            unitOfWork.Setup(x => x.GetDbRepository<IDbDirectoryRepository>()).Returns(DbRepository.Object);
            unitOfWork.Setup(x => x.GetfsDirectoryManager<IFSDirectoryManager>()).Returns(fsDirectoryManager.Object);
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object);
            directoryService.Add(path, name);
            //Assert
            DbRepository.Verify(u => u.Add(It.IsAny<DirectoryStructure>()), Times.Once);
            fsDirectoryManager.Verify(u => u.AddFolder(path, name), Times.Once);
        }

        [Test]
        [TestCase("~adminRoot\folder1", "~adminRoot\folder2")]
        public void When_MoveDirectorySuccessfully_Expect_ExecutionOfMoveMethod(string pathFrom, string pathTo)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var dbRepository = new Mock<IDbDirectoryRepository>();

            var fsDirectoryManager = new Mock<IFSDirectoryManager>();
            var autoMapper = new Mock<IMapper>();
            dbRepository.Setup(u => u.FindByPath(It.Is<string>(s => s == pathTo)))
                .Returns(new DirectoryStructure { FullPath = pathTo });

            dbRepository.Setup(u => u.FindByPathEagerLoadingParentDirectory(It.Is<string>(s => s == pathFrom)))
                .Returns(new DirectoryStructure
                {
                    ParentDirectory = new DirectoryStructure
                    {
                        FullPath = "~adminRoot\\"
                    },
                FullPath = pathFrom });

            fsDirectoryManager.Setup(u => u.Move(It.IsAny<string>(), It.IsAny<string>()));
            
            unitOfWork.Setup(x => x.GetDbRepository<IDbDirectoryRepository>()).Returns(dbRepository.Object);
            unitOfWork.Setup(x => x.GetfsDirectoryManager<IFSDirectoryManager>()).Returns(fsDirectoryManager.Object);
            unitOfWork.Setup(x => x.Commit());
            //Act
            new DirectoryService(unitOfWork.Object, autoMapper.Object).Move(pathFrom, pathTo);
            //Assert
            fsDirectoryManager.Verify(u => u.Move(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            unitOfWork.Verify(u => u.Commit());
        }

        [Test]
        [TestCase("~adminRoot\\folder2", "~adminRoot\\folder2\\folder3")]
        public void When_DirectoryMovedToSubDirectory_Expect_ServiceArgumentException(string pathFrom, string pathTo)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var dbRepository = new Mock<IDbDirectoryRepository>();

            var fsDirectoryManager = new Mock<IFSDirectoryManager>();
            var autoMapper = new Mock<IMapper>();
            dbRepository.Setup(u => u.FindByPath(It.Is<string>(s => s == pathTo)))
                .Returns(new DirectoryStructure { FullPath = pathTo });

            dbRepository.Setup(u => u.FindByPathEagerLoadingParentDirectory(It.Is<string>(s => s == pathFrom)))
                .Returns(new DirectoryStructure
                {
                    ParentDirectory = new DirectoryStructure
                    {
                        FullPath = "~adminRoot\\"
                    },
                    FullPath = pathFrom
                });

            fsDirectoryManager.Setup(u => u.Move(It.IsAny<string>(), It.IsAny<string>()));

            unitOfWork.Setup(x => x.GetDbRepository<IDbDirectoryRepository>()).Returns(dbRepository.Object);
            unitOfWork.Setup(x => x.GetfsDirectoryManager<IFSDirectoryManager>()).Returns(fsDirectoryManager.Object);
            unitOfWork.Setup(x => x.Commit());
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object);
            //Assert
            Assert.That(() => directoryService.Move(pathFrom, pathTo),
                Throws.TypeOf<ServiceArgumentException>());
        }

        [Test]
        [TestCase("~adminRoot\\folder2")]
        public void When_DirectoryRemovedSeccessfully_Expect_RemoveMethodExucutes(string path)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var dbRepository = new Mock<IDbDirectoryRepository>();
            var fsDirectoryManager = new Mock<IFSDirectoryManager>();
            var fdFileRepository = new Mock<IDbFileRepository>();
            var autoMapper = new Mock<IMapper>();
            dbRepository.Setup(u => u.FindByPathEagerLoadingFiles(It.Is<string>(s => s == path)))
                .Returns(new DirectoryStructure { FullPath = path,
                                                    Files = new[]
                                                    {
                                                        new FileStructure(),
                                                        new FileStructure()
                                                    }
                                                 });

            dbRepository.Setup(u => u.Remove(It.IsAny<DirectoryStructure>()));
            fsDirectoryManager.Setup(u => u.Remove(It.IsAny<string>()));
            fdFileRepository.Setup(u => u.RemoveRange(It.IsAny<ICollection<FileStructure>>()));

            unitOfWork.Setup(x => x.GetDbRepository<IDbDirectoryRepository>()).Returns(dbRepository.Object);
            unitOfWork.Setup(x => x.GetDbRepository<IDbFileRepository>()).Returns(fdFileRepository.Object);
            unitOfWork.Setup(x => x.GetfsDirectoryManager<IFSDirectoryManager>()).Returns(fsDirectoryManager.Object);
            unitOfWork.Setup(x => x.Commit());
            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object);
            directoryService.Remove(path);
            //Assert
            dbRepository.Verify(u => u.Remove(It.IsAny<DirectoryStructure>()), Times.AtLeastOnce);
            fsDirectoryManager.Verify(u => u.Remove(It.IsAny<string>()), Times.Once);
            fdFileRepository.Verify(u => u.RemoveRange(It.IsAny<ICollection<FileStructure>>()), Times.AtLeastOnce);
            unitOfWork.Verify(u => u.Commit());
        }

        [Test]
        [TestCase("~adminRoot\\folder2")]
        public void When_GetInfoByPathSeccessfullyExecuted_Expect_ReturnDirectoryStructureDto(string path)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var dbRepository = new Mock<IDbDirectoryRepository>();
            var autoMapper = new Mock<IMapper>();
            dbRepository.Setup(u => u.FindByPath(It.Is<string>(s => s == path)))
                .Returns(new DirectoryStructure());
            autoMapper.Setup(u => u.Map<DirectoryStructureDto>(It.IsAny<DirectoryStructure>()))
                .Returns(new DirectoryStructureDto { FullPath = path });
            
            unitOfWork.Setup(x => x.GetDbRepository<IDbDirectoryRepository>()).Returns(dbRepository.Object);

            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object);
            var res = directoryService.GetInfoByPath(path);
            //Assert
            Assert.AreEqual(res.FullPath, path);
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
                                        Name = "File1",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File2",
                                        Extension = ".html"
                                    }
                                }
                            }
                        } ,
                        Files = new []
                                {
                                    new FileStructure
                                    {
                                        Name = "File11",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File12",
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
                                        Name = "File31",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File32",
                                        Extension = ".html"
                                    }
                                }
                            }
                        },
                        Files = new []
                                {
                                    new FileStructure
                                    {
                                        Name = "File21",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File22",
                                        Extension = ".html"
                                    }
                                }
                    }

                },
                Files = new[]
                {
                    new FileStructure
                    {
                        Name = "File1",
                        Extension = ".txt"
                    },
                    new FileStructure
                    {
                        Name = "File2",
                        Extension = ".html"
                    }
                }
            };
            
            var unitOfWork = new Mock<IUnitOfWork>();
            var dbRepository = new Mock<IDbDirectoryRepository>();
            var autoMapper = new Mock<IMapper>();
            dbRepository.Setup(u => u.FindByPathEagerLoadingFull(It.Is<string>(s => s == path)))
                .Returns(rootDirectory);

            unitOfWork.Setup(x => x.GetDbRepository<IDbDirectoryRepository>()).Returns(dbRepository.Object);

            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object);
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
                                        Name = "File1",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File2",
                                        Extension = ".html"
                                    }
                                }
                            }
                        } ,
                        Files = new []
                                {
                                    new FileStructure
                                    {
                                        Name = "File11",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File12",
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
                                        Name = "File31",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File32",
                                        Extension = ".html"
                                    }
                                }
                            }
                        },
                        Files = new []
                                {
                                    new FileStructure
                                    {
                                        Name = "File21",
                                        Extension = ".txt"
                                    },
                                    new FileStructure
                                    {
                                        Name = "File22",
                                        Extension = ".html"
                                    }
                                }
                    }

                },
                Files = new[]
                {
                    new FileStructure
                    {
                        Name = "File1",
                        Extension = ".txt"
                    },
                    new FileStructure
                    {
                        Name = "File2",
                        Extension = ".html"
                    }
                }
            };

            var unitOfWork = new Mock<IUnitOfWork>();
            var dbRepository = new Mock<IDbDirectoryRepository>();
            var autoMapper = new Mock<IMapper>();
            dbRepository.Setup(u => u.FindByPathEagerLoadingFull(It.Is<string>(s => s == path)))
                .Returns(rootDirectory);

            unitOfWork.Setup(x => x.GetDbRepository<IDbDirectoryRepository>()).Returns(dbRepository.Object);

            //Act
            var directoryService = new DirectoryService(unitOfWork.Object, autoMapper.Object);
            var res = directoryService.ShowContent(path);
            //AssertSk
            var actual = new List<string>(new[] {
                "folder1",
                "folder2",
                "File1.txt",
                "File2.html"
                });
            CollectionAssert.AreEqual(res, actual);
        }

    }
}
