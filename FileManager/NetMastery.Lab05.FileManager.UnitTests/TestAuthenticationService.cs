using AutoMapper;
using Moq;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.Bl.Servicies;
using NetMastery.Lab05.FileManager.DAL.Interfacies;
using NetMastery.Lab05.FileManager.Domain;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace NetMastery.Lab05.FileManager.UnitTests
{
    [TestFixture]
    public class TestAuthenticationService
    {
        [Test]
        [TestCase("admin", null)]
        [TestCase(null, "admin")]
        public void When_PassworOrLoginIsNullInSigninMethod_Expected_ServiceArgumentNullException(string login, string password)
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var autoMapper = new Mock<IMapper>();
            var userContext = new Mock<IUserContext>();
            var authenticateService = new AuthenticationService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            Assert.That(() => authenticateService.Signin(login, password),
                Throws.TypeOf<BusinessException>());
        }

        [Test]
        public void When_AccountWithSuchLoginDoesNotExist_Expected_ServiceArgumentNullException()
        {
            var autoMapper = new Mock<IMapper>();
            
            autoMapper.Setup(m => m.Map<Account, AccountDto>(It.IsAny<Account>())).Returns((AccountDto)null);
            var Repository = new Mock<IAccountRepository>();
            var userContext = new Mock<IUserContext>();
            Repository.Setup(u => u.Find(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(new List<Account>());
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Get<IAccountRepository>()).Returns(Repository.Object);
            var authenticateService = new AuthenticationService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            Assert.That(() => authenticateService.Signin("admin", "admin"),
                Throws.TypeOf<BusinessException>());
        }

        [Test]
        public void When_LoginAndPasswordMatch_Expected_AccountDto()
        {
            var accountDto = new AccountDto
            {
                Login = "admin",
                Password = "$2a$10$q4Tpdy6rhVqWAIQgWNCzd.04Td7g4xy55RikeKYJP0CBHWtGBoJkW"
            };
            var autoMapper = new Mock<IMapper>();
            var userContext = new Mock<IUserContext>();
            autoMapper.Setup(m => m.Map<AccountDto>(It.IsAny<Account>()))
                      .Returns(accountDto);
            var Repository = new Mock<IAccountRepository>();
            var userContext = new Mock<IUserContext>();
            Repository.Setup(u => u.Find(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(new List<Account>(new[] { new Account() }));
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Get<IAccountRepository>()).Returns(Repository.Object);
            var authenticateService = new AuthenticationService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            Assert.AreEqual(authenticateService.Signin("admin", "admin"), accountDto);
               
        }
        [Test]
        public void When_PasswordWrong_Expected_ServiceArgumentException()
        {
            var accountDto = new AccountDto
            {
                Login = "admin",
                Password = "$2a$10$q4Tpdy6rhVqWAIQgWNCzd.04Td7g4xy55RikeKYJP0CBHWtGBoJkR"
            };
            var autoMapper = new Mock<IMapper>();
            var userContext = new Mock<IUserContext>();
            autoMapper.Setup(m => m.Map<AccountDto>(It.IsAny<Account>()))
                      .Returns(accountDto);
            var Repository = new Mock<IAccountRepository>();
            Repository.Setup(u => u.Find(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(new List<Account>(new[] { new Account() }));
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Get<IAccountRepository>()).Returns(Repository.Object);
            var authenticateService = new AuthenticationService(unitOfWork.Object, autoMapper.Object, userContext.Object);
            Assert.That(() => authenticateService.Signin("admin", "something"),
                Throws.TypeOf<BusinessException>());

        }


    }
}
