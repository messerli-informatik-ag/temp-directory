using System;
using System.IO;

namespace Messerli.TempDirectory
{
    public class TempDirectoryBuilder : ITempDirectoryBuilder
    {
        private string _prefix = "";
        private string _suffix = "";
        private string _prefixSeparator = "-";
        private string _suffixSeparator = "-";

        public TempDirectoryBuilder()
        {
        }

        private TempDirectoryBuilder(string prefix, string suffix, string prefixSeparator, string suffixSeparator)
        {
            _prefix = prefix;
            _suffix = suffix;
            _prefixSeparator = prefixSeparator;
            _suffixSeparator = suffixSeparator;
        }

        public ITempDirectoryBuilder Prefix(string prefix)
        {
            _prefix = prefix;
            return DeepClone();
        }

        public ITempDirectoryBuilder PrefixSeparator(string prefixSeparator)
        {
            _prefixSeparator = prefixSeparator;
            return DeepClone();
        }

        public ITempDirectoryBuilder Suffix(string suffix)
        {
            _suffix = suffix;
            return DeepClone();
        }

        public ITempDirectoryBuilder SuffixSeparator(string suffixSeparator)
        {
            _suffixSeparator = suffixSeparator;
            return DeepClone();
        }

        public TempDirectory Create()
        {
            var tempPath = Path.GetTempPath();
            var guid = Guid.NewGuid().ToString();
            var prefixSubstring = string.IsNullOrEmpty(_prefix) ? "" : _prefix + _prefixSeparator;
            var suffixSubstring = string.IsNullOrEmpty(_suffix) ? "" : _suffix + _suffixSeparator;

            var directoryName = $"{prefixSubstring}{guid}{suffixSubstring}";
            var path = Path.Combine(tempPath, directoryName);

            Directory.CreateDirectory(path);
            var onDispose = CreateDirectoryDeletionFn(path);

            return new TempDirectory(directoryName, path, onDispose);
        }

        private ITempDirectoryBuilder DeepClone()
        {
            return new TempDirectoryBuilder(_prefix, _suffix, _prefixSeparator, _suffixSeparator);
        }

        private static OnDispose CreateDirectoryDeletionFn(string directoryPath)
        {
            return () => Directory.Delete(directoryPath, true);
        }
    }
}
