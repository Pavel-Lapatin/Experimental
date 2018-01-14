using NetMastery.Lab05.FileManager.Helpers;
using NetMastery.Lab05.FileManager.UI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UnitTests
{
    [TestFixture]
    public class TestUIHelpers
    {

        [TestCase("command argument1 argument2 ")]
        public void When_CoommandWithTwoArguments_Expected_TheArrayOfThreeStrings(string commandLine)
        {
            //Act
            var args = UIHelpers.ParseArguemts(commandLine);
            //Assert
            Assert.AreEqual(args.Length, 3);
            CollectionAssert.AreEqual(args, new[] { "command", "argument1", "argument2" });
        }

        [TestCase("command \"some argument\" \"some other argument\" ")]
        public void When_CommandWithTwoQuotedArguments_Expected_TheArrayOfThreeStrings(string commandLine)
        {
            //Act
            var args = UIHelpers.ParseArguemts(commandLine);
            //Assert
            Assert.AreEqual(args.Length, 3);
            CollectionAssert.AreEqual(args, new[] { "command", "some argument", "some other argument" });
        }

        [TestCase("command1 command2 \"some argument\" \"some other argument\" ")]
        public void When_TwoCommandWithTwoQuotedArguments_Expected_TheArrayOfFourStrings(string commandLine)
        {
            //Act
            var args = UIHelpers.ParseArguemts(commandLine);
            //Assert
            Assert.AreEqual(args.Length, 4);
            CollectionAssert.AreEqual(args, new[] { "command1", "command2", "some argument", "some other argument" });
        }

        [TestCase("command1 \"some argument ")]
        public void When_CommandWithOneQoutes_TheArrayOfTwoStrings(string commandLine)
        {
            //Assert
            Assert.That(() => UIHelpers.ParseArguemts(commandLine), Throws.TypeOf<UIException>());
        }

        [TestCase("command1 \"some argument\" command2 command3 \"some other argument\" ")]
        [TestCase("command1\"some argument\"command2 command3\"some other argument\"")]
        public void When_CommandsLocatedBetweenArguments_Expected_TheArrayWithRightSequence(string commandLine)
        {
            //Act
            var args = UIHelpers.ParseArguemts(commandLine);
            //Assert
            Assert.AreEqual(args.Length, 5);
            CollectionAssert.AreEqual(args, new[] { "command1", "some argument", "command2", "command3", "some other argument" });
        }

        [TestCase("login -l admin -p admin")]
        public void When_CommandLineWithoutQuotes_Expected_TheArrayWithRightSequence(string commandLine)
        {
            //Act
            var args = UIHelpers.ParseArguemts(commandLine);
            //Assert
            Assert.AreEqual(args.Length, 5);
            CollectionAssert.AreEqual(args, new[] { "login", "-l", "admin", "-p", "admin" });
        }

        [TestCase("login -l \"\" -p \"\"")]
        public void When_EmptyStringBetweenQuotes_Expected_EmptyStringsArgumentsInArray(string commandLine)
        {
            //Act
            var args = UIHelpers.ParseArguemts(commandLine);
            //Assert
            Assert.AreEqual(args.Length, 5);
            CollectionAssert.AreEqual(args, new[] { "login", "-l", "", "-p", "" });
        }


        [TestCase(@"E:\Test1", @".\Test2")]
        public void When_CurrentDirectoryCharacter_Expected_ValidPath(string currentPath, string path)
        {
            //Act
            var newPath = path.CreatePath(currentPath);
            //Assert
            Assert.AreEqual(newPath, @"E:\Test1\Test2");
        }


        [TestCase(@"E:\Test1", @"..\Test2")]
        [TestCase(@"E:\Test1", @".\..\Test2")]
        [TestCase(@"E:\Test1\Test2", @"..\..\Test2")]
        public void When_PreviousDirectoryCharacter_Expected_ValidPath(string currentPath, string path)
        {
            //Act
            var newPath = path.CreatePath(currentPath);
            //Assert
            Assert.AreEqual(newPath, @"E:\Test2");
        }

        [TestCase(@"E:", @"..\Test2")]
        [TestCase(@"E:\Test1", @"..\..\Test2")]
        [TestCase(@"E:\Test1", @"..\..\..\Test2")]
        public void When_PreviousDirectoryDisk_Expected_ValidPath(string currentPath, string path)
        {
            //Act
            var newPath = path.CreatePath(currentPath);
            //Assert
            Assert.AreEqual(newPath, @"E:\Test2");
        }

        [TestCase(@"E:\Test1", @"~\Test2")]
        [TestCase(@"E:\Test1", @"~\.\Test2")]
        [TestCase(@"E:\Test1", @"~\Some Folder\..\Test2")]
        [TestCase(@"E:\Test1", @"~\Test2\admin-Dir1-Lvl1\Test\admin-Dir2.1-Lvl2\..\..\.\..\")]
        public void When_AbsolutePath_Expected_ValidPath(string currentPath, string path)
        {
            //Act
            var newPath = path.CreatePath(currentPath);
            //Assert
            Assert.AreEqual(newPath, @"~\Test2");
        }


        [TestCase(@"E:\Test1", @"~\Test2\admin-Dir1-Lvl1\Test\admin-Dir2.1-Lvl2\..\..\..\..\.\..\")]
        public void When_AbsolutePat_Expected_ValidPath(string currentPath, string path)
        {
            //Act
            var newPath = path.CreatePath(currentPath);
            //Assert
            Assert.AreEqual(newPath, @"~");
        }

        [TestCase(null, @".\Test2")]
        [TestCase(@"E:", null)]
        public void When_PathNull_Expected_ArgumentNullException(string currentPath, string path)
        {
            //Assert
            Assert.That(()=> path.CreatePath(currentPath), Throws.TypeOf<ArgumentNullException>());
        }
        //[TestCase(@"~\adminRoot\Test1", @"~\adminRoot")]
        //public void When_AccessAllowed_Expected_true(string path, string rootFolder)
        //{
        //    //Act
        //    var result = path.IsVirtualAccessAllowed(rootFolder);
        //    //Assert
        //    Assert.AreEqual(result, true);
        //}

        //[TestCase(@"~\pashaRoot\Test1", @"~\adminRoot")]
        //public void When_AccessIsDenied_Expected_false(string path, string rootFolder)
        //{
        //    //Act
        //    var result = path.IsVirtualAccessAllowed(rootFolder);
        //    //Assert
        //    Assert.AreEqual(result, false);
        //}

        //[TestCase(@"E:\\pashaRoot\Test1")]
        //public void When_PathIsNotVirtual_Expected_false(string path)
        //{
        //    //Act
        //    var result = path.IsPathVirtual();
        //    //Assert
        //    Assert.AreEqual(result, false);
        //}

        //[TestCase(@"~\pashaRoot\Test1")]
        //public void When_PathIsVirtual_Expected_true(string path)
        //{
        //    //Act
        //    var result = path.IsPathVirtual();
        //    //Assert
        //    Assert.AreEqual(result, true);
        //}
    }
}
