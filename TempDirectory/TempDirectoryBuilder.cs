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

        public ITempDirectory Create()
        {
            throw new System.NotImplementedException();
        }

        private string CreateTempDirectory(string directoryName)
        {
            var tempPath = System.IO.Path.GetTempPath();
            var path = System.IO.Path.Combine(tempPath, directoryName);

            Directory.CreateDirectory(path);

            return path;
        }
    }
}
