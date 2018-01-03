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
    public class TestAuthenticationService
    {
        [Test]       
        public void When_PassworOrLoginIsNullInSigninMethod_Expected_ServiceArgumentNullException()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var autoMapper = new Mock<IMapper>();
            var authenticateService = new AuthenticationService(unitOfWork.Object, autoMapper.Object);
            Assert.That(() => authenticateService.Signin("admin", null),
                Throws.TypeOf<ServiceArgumentNullException>());
            Assert.That(() => authenticateService.Signin(null, "admin"),
                Throws.TypeOf<ServiceArgumentNullException>());
        }

        [Test]
        public void When_AccountWithSuchLoginDoesNotExist_Expected_ServiceArgumentNullException()
        {
            var autoMapper = new Mock<IMapper>();
            
            autoMapper.Setup(m => m.Map<Account, AccountDto>(It.IsAny<Account>())).Returns((AccountDto)null);
            var DbRepository = new Mock<IDbAccountRepository>();
            DbRepository.Setup(u => u.Find(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(new List<Account>());
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.GetDbRepository<IDbAccountRepository>()).Returns(DbRepository.Object);
            var authenticateService = new AuthenticationService(unitOfWork.Object, autoMapper.Object);
            Assert.That(() => authenticateService.Signin("admin", "admin"),
                Throws.TypeOf<ServiceArgumentNullException>());
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
            autoMapper.Setup(m => m.Map<AccountDto>(It.IsAny<Account>()))
                      .Returns(accountDto);
            var DbRepository = new Mock<IDbAccountRepository>();
            DbRepository.Setup(u => u.Find(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(new List<Account>(new[] { new Account() }));
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.GetDbRepository<IDbAccountRepository>()).Returns(DbRepository.Object);
            var authenticateService = new AuthenticationService(unitOfWork.Object, autoMapper.Object);
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
            autoMapper.Setup(m => m.Map<AccountDto>(It.IsAny<Account>()))
                      .Returns(accountDto);
            var DbRepository = new Mock<IDbAccountRepository>();
            DbRepository.Setup(u => u.Find(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(new List<Account>(new[] { new Account() }));
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.GetDbRepository<IDbAccountRepository>()).Returns(DbRepository.Object);
            var authenticateService = new AuthenticationService(unitOfWork.Object, autoMapper.Object);
            Assert.That(() => authenticateService.Signin("admin", "something"),
                Throws.TypeOf<ServiceArgumentException>());

        }


    }
}
