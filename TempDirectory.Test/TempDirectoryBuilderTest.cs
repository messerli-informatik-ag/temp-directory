using System.Diagnostics.CodeAnalysis;
using System.IO;
using Xunit;

namespace Messerli.TempDirectory.Test
{
    public sealed class TempDirectoryBuilderTest
    {
        private const string Prefix = "prefix";
        private const string Suffix = "suffix";
        private const string PrefixSeparator = "@";
        private const string SuffixSeparator = ".";

        [Theory]
        [MemberData(nameof(GetTempDirectoryBuilderConfigurations))]
        public void CreatesTempDirectory(ITempDirectoryBuilder tempDirectoryBuilder)
        {
            using var tempDirectory = tempDirectoryBuilder.Create();
            Assert.True(Directory.Exists(tempDirectory.FullName));
        }

        [Theory]
        [MemberData(nameof(GetTempDirectoryBuilderConfigurations))]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP017:Prefer using", Justification = "Intentional for testing purposes.")]
        public void DeletesTempDirectory(ITempDirectoryBuilder tempDirectoryBuilder)
        {
            var tempDirectory = tempDirectoryBuilder.Create();
            var fullName = tempDirectory.FullName;
            Assert.True(Directory.Exists(tempDirectory.FullName));
            tempDirectory.Dispose();

            Assert.False(Directory.Exists(fullName));
        }

        [Theory]
        [MemberData(nameof(GetTempDirectoryBuilderConfigurations))]
        public void FullNameContainsName(ITempDirectoryBuilder tempDirectoryBuilder)
        {
            using var tempDirectory = tempDirectoryBuilder.Create();
            Assert.Contains(tempDirectory.Name, tempDirectory.FullName);
        }

        [Theory]
        [MemberData(nameof(GetTempDirectoryBuilderConfigurations))]
        public void FullNameIsNotName(ITempDirectoryBuilder tempDirectoryBuilder)
        {
            using var tempDirectory = tempDirectoryBuilder.Create();
            Assert.NotEqual(tempDirectory.FullName, tempDirectory.Name);
        }

        [Fact]
        public void PrefixIsInName()
        {
            using var tempDirectory = new TempDirectoryBuilder().Prefix(Prefix).Create();
            Assert.StartsWith(Prefix, tempDirectory.Name);
        }

        [Fact]
        public void SuffixIsInName()
        {
            using var tempDirectory = new TempDirectoryBuilder().Suffix(Suffix).Create();
            Assert.EndsWith(Suffix, tempDirectory.Name);
        }

        [Fact]
        public void SeparatorsAreInName()
        {
            using var tempDirectory = new TempDirectoryBuilder()
                .Prefix(Prefix)
                .PrefixSeparator(PrefixSeparator)
                .Suffix(Prefix)
                .SuffixSeparator(SuffixSeparator)
                .Create();
            Assert.Contains(PrefixSeparator, tempDirectory.Name);
            Assert.Contains(SuffixSeparator, tempDirectory.Name);
        }

        [Fact]
        public void SeparatorsAreNotInNameWithoutPrefixOrSuffix()
        {
            using var tempDirectory = new TempDirectoryBuilder()
                .PrefixSeparator(PrefixSeparator)
                .SuffixSeparator(SuffixSeparator)
                .Create();
            Assert.DoesNotContain(PrefixSeparator, tempDirectory.Name);
            Assert.DoesNotContain(SuffixSeparator, tempDirectory.Name);
        }

        public static TheoryData<ITempDirectoryBuilder> GetTempDirectoryBuilderConfigurations()
            => new TheoryData<ITempDirectoryBuilder>
            {
                new TempDirectoryBuilder(),
                new TempDirectoryBuilder().Prefix(Prefix),
                new TempDirectoryBuilder().Suffix(Suffix),
                new TempDirectoryBuilder().Prefix(Prefix).Suffix(Suffix),
                new TempDirectoryBuilder().Prefix(Prefix).PrefixSeparator(PrefixSeparator),
                new TempDirectoryBuilder().Suffix(Prefix).SuffixSeparator(SuffixSeparator),
                new TempDirectoryBuilder().Prefix(Prefix).PrefixSeparator(PrefixSeparator).Suffix(Prefix).SuffixSeparator(SuffixSeparator),
            };
    }
}
