using Microsoft.Extensions.CommandLineUtils;
using Moq;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI;
using NetMastery.Lab05.FileManager.UI.Commands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using NUnit.Framework;
using System;

namespace NetMastery.Lab05.FileManager.UnitTests.UITests.CommandLineTests
{
    [TestFixture]
    public class TestSearchDirectory
    {
        [TestCaseSource(typeof(CommandLineArguments), nameof(CommandLineArguments.TwoArgs))]
        public void When_EnoughArgumentsSearch_Expected_ZeroValue(string[] args)
        {
            //Arrange
            var result = new Mock<IResultProvider>();
            result.SetupProperty(x => x.Result);
            var userContext = new Mock<IUserContext>();
            var directoryService = new Mock<IDirectoryService>();
            directoryService.Setup(x => x.Search(It.IsAny<string>(), It.IsAny<string>()));
            var controlerFactory = new Func<DirectoryController>(() => new DirectoryController(directoryService.Object, userContext.Object));
            //Act
            var command = new SearchDirectoryCommand(controlerFactory, result.Object);
            var commandResult = command.Execute(args);
            //Assert
            Assert.AreEqual(commandResult, 0);
        }

        [TestCaseSource(typeof(CommandLineArguments), nameof(CommandLineArguments.Empty))]
        [TestCaseSource(typeof(CommandLineArguments), nameof(CommandLineArguments.OneArg))]
        [TestCaseSource(typeof(CommandLineArguments), nameof(CommandLineArguments.ThreeArgs))]
        public void When_MoreOrLessArgumentsSearch_Expected_CommandParthingException(string[] args)
        {
            //Arrange
            var result = new Mock<IResultProvider>();
            result.SetupProperty(x => x.Result);
            var userContext = new Mock<IUserContext>();
            var directoryService = new Mock<IDirectoryService>();
            directoryService.Setup(x => x.Search(It.IsAny<string>(), It.IsAny<string>()));
            var controlerFactory = new Func<DirectoryController>(() => new DirectoryController(directoryService.Object, userContext.Object));
            //Act
            var command = new SearchDirectoryCommand(controlerFactory, result.Object);
            //Assert
            Assert.That(() => command.Execute(args), Throws.TypeOf<CommandParsingException>());
        }
    }
}
