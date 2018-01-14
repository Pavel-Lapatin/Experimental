using Microsoft.Extensions.CommandLineUtils;
using Moq;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI;
using NetMastery.Lab05.FileManager.UI.Commands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UnitTests.UITests.CommandLineTests
{
    [TestFixture]
    public class TestLoginDirectory
    {
        [TestCaseSource(typeof(CommandLineArguments), nameof(CommandLineArguments.LoginRightCmd))]
        public void When_EnoughArguments_Expected_ZeroValue(string[] args)
        {
            //Arrange
            var result = new Mock<IResultProvider>();
            result.SetupProperty(x => x.Result);
            var userContext = new Mock<IUserContext>();
            var directoryService = new Mock<IAuthenticationService>();
            directoryService.Setup(x => x.Signin(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new AccountDto
                {
                    Login = "admin",
                    RootDirectory = "adminRoot"
                });
            var controlerFactory = new Func<LoginController>(() => new LoginController(directoryService.Object, userContext.Object));
            //Act
            var command = new LoginCommand(controlerFactory, result.Object);
            var commandResult = command.Execute(args);
            //Assert
            Assert.AreEqual(commandResult, 0);
        }

        [TestCaseSource(typeof(CommandLineArguments), nameof(CommandLineArguments.LoginWrongCmd))]
        public void When_MoreOrLessArguments_Expected_CommandParthingException(string[] args)
        {
            //Arrange
            var result = new Mock<IResultProvider>();
            result.SetupProperty(x => x.Result);
            var userContext = new Mock<IUserContext>();
            var directoryService = new Mock<IAuthenticationService>();
            directoryService.Setup(x => x.Signin(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new AccountDto
                {
                    Login = "admin",
                    RootDirectory = "adminRoot"
                });
            var controlerFactory = new Func<LoginController>(() => new LoginController(directoryService.Object, userContext.Object));
            //Act
            var command = new LoginCommand(controlerFactory, result.Object);
            //Assert
            Assert.That(() => command.Execute(args), Throws.TypeOf<CommandParsingException>());
        }
    }
}
