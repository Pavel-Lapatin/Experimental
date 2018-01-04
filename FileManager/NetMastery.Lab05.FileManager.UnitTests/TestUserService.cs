using AutoMapper;
using Moq;
using NetMastery.Lab05.FileManager.Bl.Servicies;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.DAL.Repository;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Dto;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var dbRepository = new Mock<IDbAccountRepository>();
            var autoMapper = new Mock<IMapper>();
            dbRepository.Setup(u => u.FindByLogin(It.Is<string>(s => s == login)))
                .Returns(new Account());
            autoMapper.Setup(u => u.Map<AccountDto>(It.IsAny<Account>()))
                .Returns(new AccountDto { Login = "admin" });

            unitOfWork.Setup(x => x.GetDbRepository<IDbAccountRepository>()).Returns(dbRepository.Object);

            //Act
            var userService = new UserService(unitOfWork.Object, autoMapper.Object);
            var res = userService.GetInfoByLogin(login);
            //Assert
            Assert.AreEqual(res.Login, login);
        }
    }
}

