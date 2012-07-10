using System.IO;

namespace Service.Test
{
    public class BaseIo : BaseConstants
    {
        protected readonly char Sep = Path.DirectorySeparatorChar;
        protected const string Up = "..";

        internal const string DirNameToMove      = "foo";
        internal const string DirNameDestination = "bar";


        protected void ResetTestDirectories(string testDirPath, string tmpDirPath)
        {
            string dirToMove = Path.Combine(testDirPath, DirNameToMove);
            var directoryInfoDirToMove = new DirectoryInfo(dirToMove);
            if (!directoryInfoDirToMove.Exists) {
                directoryInfoDirToMove.Create();
            }

            string dirDestination = Path.Combine(tmpDirPath, DirNameDestination);
            var directoryInfoDestination = new DirectoryInfo(dirDestination);
            if (directoryInfoDestination.Exists) {
                directoryInfoDestination.Delete(true);
            }
        }
    }
}
