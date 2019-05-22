using System.Collections.Generic;
using System.IO;
using Messerli.TempDirectory;
using Xunit;
using Jetbrains.Annotations;

namespace TempDirectory.Test
{
    public class TempDirectoryBuilderTest
    {
        [Theory]
        [MemberData(nameof(GetTempDirectoryBuilderConfigurations))]
        public void CreatesTempDirectory(TempDirectoryBuilder tempDirectoryBuilder)
        {
            using (var tempDirectory = tempDirectoryBuilder.Create())
            {
                Assert.True(Directory.Exists(tempDirectory.FullName));
            }
        }

        [Theory]
        [MemberData(nameof(GetTempDirectoryBuilderConfigurations))]
        public void DeletesTempDirectory(TempDirectoryBuilder tempDirectoryBuilder)
        {
            var tempDirectory = tempDirectoryBuilder.Create();
            Assert.True(Directory.Exists(tempDirectory.FullName));
            tempDirectory.Dispose();

            Assert.False(Directory.Exists(tempDirectory.FullName));
        }

        [Theory]
        [MemberData(nameof(GetTempDirectoryBuilderConfigurations))]
        public void FullNameContainsName(TempDirectoryBuilder tempDirectoryBuilder)
        {
            using (var tempDirectory = tempDirectoryBuilder.Create())
            {
                Assert.Contains(tempDirectory.FullName, tempDirectory.Name);
            }
        }

        [Theory]
        [MemberData(nameof(GetTempDirectoryBuilderConfigurations))]
        public void FullNameIsNotName(TempDirectoryBuilder tempDirectoryBuilder)
        {
            using (var tempDirectory = tempDirectoryBuilder.Create())
            {
                Assert.NotEqual(tempDirectory.FullName, tempDirectory.Name);
            }
        }

        [UsedImplicitely]
        public static IEnumerable<object[]> GetTempDirectoryBuilderConfigurations()
        {
            yield return new object[] { new TempDirectoryBuilder() };
            yield return new object[] { new TempDirectoryBuilder().Prefix(Prefix) };
            yield return new object[] { new TempDirectoryBuilder().Suffix(Suffix) };
            yield return new object[] { new TempDirectoryBuilder().Prefix(Prefix).Suffix(Suffix) };
            yield return new object[] { new TempDirectoryBuilder().Prefix(Prefix).PrefixSeparator(PrefixSeparator) };
            yield return new object[] { new TempDirectoryBuilder().Suffix(Prefix).SuffixSeparator(SuffixSeparator) };
            yield return new object[] { new TempDirectoryBuilder().Prefix(Prefix).PrefixSeparator(PrefixSeparator).Suffix(Prefix).SuffixSeparator(SuffixSeparator) };
        }

        private const string Prefix = "prefix";
        private const string Suffix = "prefix";
        private const string PrefixSeparator = "-";
        private const string SuffixSeparator = "-";
    }
}
