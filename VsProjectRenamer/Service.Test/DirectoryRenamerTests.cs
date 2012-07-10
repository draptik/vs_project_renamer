using System;
using System.IO;
using NUnit.Framework;

namespace Service.Test
{
    [TestFixture]
    public class DirectoryRenamerTests : BaseIo
    {
        private string testDirPath;
        private string tmpDirPath;
        private DirectoryRenamer directoryRenamer;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            testDirPath = string.Format("{0}{1}{2}{3}{4}{5}{6}", 
                Environment.CurrentDirectory,
                Sep, Up, Sep, Up, Sep, TestDirName);

            tmpDirPath = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                Environment.CurrentDirectory,
                Sep, Up, Sep, Up, Sep, TestTmpDirName);

            ResetTestDirectories();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            ResetTestDirectories();
        }

        [SetUp]
        public void SetUp()
        {
            directoryRenamer = new DirectoryRenamer(testDirPath, tmpDirPath);
        }

        [TearDown]
        public void TearDown()
        {
            ResetTestDirectories();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_Empty_Should_ThrowException()
        {
            new DirectoryRenamer();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "baseDir is null or empty!")]
        public void Ctor_BaseDirNullOrEmpty_WithInvalidPath_Should_ThrowException([Values("", null)] String baseDir)
        {
            new DirectoryRenamer(baseDir);
        }

        [Test]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void Ctor_BaseDirNotFound_WithInvalidPath_Should_ThrowException()
        {
            new DirectoryRenamer(testDirPath + "Invalid");
        }

        [Test]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void Ctor_TmpDirNotFound_WithInvalidPath_Should_ThrowException()
        {
            new DirectoryRenamer(testDirPath, "InvalidTmpDirName");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Rename_With_InvalidOldDir_Should_ThrowException([Values("", null)] string oldDirPath)
        {
            const string newDirPath = "foo";
            directoryRenamer.Rename(oldDirPath, newDirPath);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Rename_With_InvalidNewDir_Should_ThrowException([Values("", null)] string newDirPath)
        {
            const string oldDirPath = DirNameToMove;
            directoryRenamer.Rename(oldDirPath, newDirPath);
        }

        [Test]
        [ExpectedException(typeof(DirectoryNotFoundException), ExpectedMessage = "does not exist!", MatchType = MessageMatch.Contains)]
        public void Rename_With_OldDirNotFound_Should_ThrowException()
        {
            const string oldDirPath = "dirDoesNotExist";
            const string newDirPath = "valid";
            directoryRenamer.Rename(oldDirPath, newDirPath);
        }

        [Test]
        public void Rename_With_ValidInputs_AndTmpDir_Should_Return_True()
        {
            const string oldDirPath = DirNameToMove;
            const string newDirPath = "bar";
            bool success = directoryRenamer.Rename(oldDirPath, newDirPath);
            Assert.IsTrue(success);
        }

        [Test]
        public void Rename_With_ValidInputs_WithoutTmpDir_Should_Return_True()
        {
            const string oldDirPath = DirNameToMove;
            const string newDirPath = "bar";

            tmpDirPath = testDirPath; // !!!!!!
            
            directoryRenamer = new DirectoryRenamer(testDirPath, tmpDirPath);
            bool success = directoryRenamer.Rename(oldDirPath, newDirPath);
            Assert.IsTrue(success);
        }
    
        private void ResetTestDirectories()
        {
            base.ResetTestDirectories(testDirPath, tmpDirPath);
        }

    }
}
