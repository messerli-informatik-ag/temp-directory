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
            // ReSharper disable once ArgumentsStyleNamedExpression
            return DeepClone(prefix: prefix);
        }

        public ITempDirectoryBuilder PrefixSeparator(string prefixSeparator)
        {
            return DeepClone(prefixSeparator: prefixSeparator);
        }

        public ITempDirectoryBuilder Suffix(string suffix)
        {
            return DeepClone(suffix: suffix);
        }

        public ITempDirectoryBuilder SuffixSeparator(string suffixSeparator)
        {
            return DeepClone(suffixSeparator: suffixSeparator);
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

        private ITempDirectoryBuilder DeepClone(string prefix = null, string suffix = null, string prefixSeparator = null, string suffixSeparator = null)
        {
            return new TempDirectoryBuilder(
                prefix ?? _prefix,
                suffix ?? _suffix,
                prefixSeparator ?? _prefixSeparator,
                suffixSeparator ?? _suffixSeparator);
        }

        private static OnDispose CreateDirectoryDeletionFn(string directoryPath)
        {
            return () => Directory.Delete(directoryPath, true);
        }
    }
}
