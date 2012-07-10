using System;
using System.IO;

namespace Service
{
    public class DirectoryRenamer : IBaseRenamer
    {
        public DirectoryRenamer()
        {
            if (string.IsNullOrEmpty(BaseDir)) {
                throw new ArgumentNullException();
            }
        }

        public DirectoryRenamer(string baseDirectory, string tmpDirectory = null)
        {
            if (string.IsNullOrEmpty(baseDirectory)) throw new ArgumentNullException("baseDirectory", "baseDir is null or empty!");

            var directoryInfoBaseDir = new DirectoryInfo(baseDirectory);
            if (!directoryInfoBaseDir.Exists) {
                throw new DirectoryNotFoundException("baseDirectory");
            }
            BaseDir = baseDirectory;

            if (!string.IsNullOrEmpty(tmpDirectory)) {
                var directoryInfoTmpDir = new DirectoryInfo(tmpDirectory);
                if (!directoryInfoTmpDir.Exists) {
                    throw new DirectoryNotFoundException("tmpDirectory");
                } 
            }
            TmpDir = tmpDirectory;
        }

        public string BaseDir { get; set; }
        public string TmpDir { get; set; }
        public string DestinationDir { get { return string.IsNullOrEmpty(TmpDir) ? BaseDir : TmpDir; } }

        public bool Rename(string oldDir, string newDir)
        {
            if (string.IsNullOrEmpty(oldDir)) throw new ArgumentNullException("oldDir");
            if (string.IsNullOrEmpty(newDir)) throw new ArgumentNullException("newDir");

            string oldPath = Path.Combine(BaseDir, oldDir);
            var directoryInfoOldDir = new DirectoryInfo(oldPath);
            if (!directoryInfoOldDir.Exists) {
                throw new DirectoryNotFoundException("oldDir '" + oldPath + "' does not exist!");
            }
            
            string destination = Path.Combine(DestinationDir, newDir);

            bool result = false;

            if (!new DirectoryInfo(destination).Exists) {
                directoryInfoOldDir.MoveTo(destination);
                result = new DirectoryInfo(destination).Exists;
            }

            return result;
        }
    }
}
