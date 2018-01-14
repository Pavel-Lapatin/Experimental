using Microsoft.Extensions.CommandLineUtils;
using Moq;
using NetMastery.Lab05.FileManager.UI.Commands;
using NUnit.Framework;

namespace NetMastery.Lab05.FileManager.UnitTests.UITests.CommandLineTests
{
    [TestFixture]
    public class TestExit
    {
        //[TestCaseSource(typeof(CommandLineArguments), nameof(CommandLineArguments.Empty))]
        //public void When_EnoughArguments_Expected_ZeroValue(string[] args)
        //{
        //    //Act
        //    var command = new ExitCommand();
        //    var commandResult = command.Execute(args);
        //    //Assert
        //    Assert.AreEqual(commandResult, 0);
        //}

        [TestCaseSource(typeof(CommandLineArguments), nameof(CommandLineArguments.OneArg))]
        public void When_MoreOrLessArguments_Expected_CommandParthingException(string[] args)
        {
            //Act
            var command = new ExitCommand();
            //Assert
            Assert.That(() => command.Execute(args), Throws.TypeOf<CommandParsingException>());
        }
    }
}
