using AutoMapper;
using Moq;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.Bl.Servicies;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Dto;
using NUnit.Framework;

namespace NetMastery.Lab05.FileManager.UnitTests
{
    [TestFixture]
    public class TestUserService
    {
        [Test]
        [TestCase("admin")]
        public void When_GetInfoByPathSeccessfullyExecuted_Expect_ReturnAccount(string login)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var Repository = new Mock<IAccountRepository>();
            var autoMapper = new Mock<IMapper>();
            Repository.Setup(u => u.FindByLogin(It.Is<string>(s => s == login)))
                .Returns(new Account());
            autoMapper.Setup(u => u.Map<AccountDto>(It.IsAny<Account>()))
                .Returns(new AccountDto { Login = "admin" });

            unitOfWork.Setup(x => x.Get<IAccountRepository>()).Returns(Repository.Object);

            //Act
            var userService = new UserService(unitOfWork.Object, autoMapper.Object);
            var res = userService.GetInfoByLogin(login);
            //Assert
            Assert.AreEqual(res.Login, login);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void When_LoginIsNullOrEmpty_Expected_ServiceNullArgumentException(string login)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var autoMapper = new Mock<IMapper>();
            var userService = new UserService(unitOfWork.Object, autoMapper.Object);
            Assert.That(() => userService.GetInfoByLogin(login),
                Throws.TypeOf<ServiceArgumentNullException>());
        }
    }
}

