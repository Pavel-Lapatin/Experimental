using NetMastery.Lab05.FileManager.UI.ViewModels;
using NUnit.Framework;


namespace NetMastery.Lab05.FileManager.UnitTests.UITests.ViewModels
{
    [TestFixture]
    public class TestPathViewModel
    {
        [Test]
        [TestCase(@"D:\Directory1:\")]
        public void When_PathHasMoreThanTwoColons_Expected_ModelIsValidFalse(string path)
        {
            var viewModel = new PathViewModel(path);

            Assert.AreEqual(viewModel.IsValid, false);
            Assert.AreEqual(viewModel.Errors.Count, 1);
        }

        [Test]
        [TestCase(null)]
        public void When_PathIsNull_Expected_ModelIsValidFalse(string path)
        {
            var viewModel = new PathViewModel(path);

            Assert.AreEqual(viewModel.IsValid, false);
            Assert.AreEqual(viewModel.Errors.Count, 1);
        }

        [Test]
        [TestCase(@"path~")]
        [TestCase(@"path*")]
        [TestCase(@"path/")]
        [TestCase(@"path<")]
        [TestCase(@"path>")]
        [TestCase(@"path?")]
        [TestCase(@"path|")]
        [TestCase("path\"")]
        public void When_NameWithUnacceptableChars_Expected_ModelIsValidFalse(string path)
        {
            var viewModel = new PathViewModel(path);

            Assert.AreEqual(viewModel.IsValid, false);
            Assert.AreEqual(viewModel.Errors.Count, 1);
        }

        [Test]
        [TestCase(@"~\adminRoot\Folder1\File1.txt")]
        [TestCase(@"E:\adminRoot\Folder1\File1.txt")]
        [TestCase(@".\Folder1\File1.txt")]
        [TestCase(@"..\..\Folder1\File1.txt")]
        public void When_PathIsCorrect_Expected_ModelIsValidFalse(string path)
        {
            var viewModel = new PathViewModel(path);

            Assert.AreEqual(viewModel.IsValid, true);
        }

    }
}
