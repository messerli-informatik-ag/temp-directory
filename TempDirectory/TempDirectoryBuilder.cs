using System;
using System.IO;

namespace Messerli.TempDirectory
{
    public sealed class TempDirectoryBuilder : ITempDirectoryBuilder
    {
        private readonly string _prefix = string.Empty;
        private readonly string _suffix = string.Empty;
        private readonly string _prefixSeparator = "-";
        private readonly string _suffixSeparator = "-";

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
            => DeepClone(prefix: prefix);

        public ITempDirectoryBuilder PrefixSeparator(string prefixSeparator)
            => DeepClone(prefixSeparator: prefixSeparator);

        public ITempDirectoryBuilder Suffix(string suffix)
            => DeepClone(suffix: suffix);

        public ITempDirectoryBuilder SuffixSeparator(string suffixSeparator)
            => DeepClone(suffixSeparator: suffixSeparator);

        public TempDirectory Create()
        {
            var tempPath = Path.GetTempPath();
            var directoryName = GenerateDirectoryName();
            var path = Path.Combine(tempPath, directoryName);

            Directory.CreateDirectory(path);
            var onDispose = CreateDirectoryDeletionFunction(path);

            return new TempDirectory(directoryName, path, onDispose);
        }

        private ITempDirectoryBuilder DeepClone(
            string? prefix = null,
            string? suffix = null,
            string? prefixSeparator = null,
            string? suffixSeparator = null)
            => new TempDirectoryBuilder(
                prefix ?? _prefix,
                suffix ?? _suffix,
                prefixSeparator ?? _prefixSeparator,
                suffixSeparator ?? _suffixSeparator);

        private string GenerateDirectoryName()
        {
            var guid = Guid.NewGuid().ToString();
            var prefixSubstring = string.IsNullOrEmpty(_prefix) ? string.Empty : _prefix + _prefixSeparator;
            var suffixSubstring = string.IsNullOrEmpty(_suffix) ? string.Empty : _suffixSeparator + _suffix;

            return $"{prefixSubstring}{guid}{suffixSubstring}";
        }

        private static OnDispose CreateDirectoryDeletionFunction(string directoryPath)
            => () => Directory.Delete(directoryPath, true);
    }
}
