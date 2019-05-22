using System.IO;

namespace Messerli.TempDirectory
{
    public class TempDirectoryBuilder : ITempDirectoryBuilder
    {
        public ITempDirectoryBuilder Prefix(string prefix)
        {
            throw new System.NotImplementedException();
        }

        public ITempDirectoryBuilder PrefixSeparator(string prefixSeparator)
        {
            throw new System.NotImplementedException();
        }

        public ITempDirectoryBuilder Suffix(string suffix)
        {
            throw new System.NotImplementedException();
        }

        public ITempDirectoryBuilder SuffixSeparator(string suffixSeparator)
        {
            throw new System.NotImplementedException();
        }

        public TempDirectory Create()
        {
            throw new System.NotImplementedException();
        }

        private static string CreateTempDirectory(string directoryName)
        {
            var tempPath = Path.GetTempPath();
            var path = Path.Combine(tempPath, directoryName);

            Directory.CreateDirectory(path);

            return path;
        }

        private static OnDispose CreateDirectoryDeletionFn(string directoryPath)
        {
            return () => Directory.Delete(directoryPath, true);
        }
    }
}
