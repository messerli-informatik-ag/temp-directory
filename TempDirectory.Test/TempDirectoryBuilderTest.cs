using System.IO;
using Messerli.TempDirectory;
using Xunit;

namespace TempDirectory.Test
{
    public class TempDirectoryBuilderTest
    {
        [Fact]
        public void CreatesTempDirectory()
        {
            var tempDirectoryBuilder = CreateTempDirectoryBuilder();
            using (var tempDirectory = tempDirectoryBuilder.Create())
            {
                Assert.True(Directory.Exists(tempDirectory.FullName));
            }
        }

        [Fact]
        public void FullNameContainsName()
        {
            var tempDirectoryBuilder = CreateTempDirectoryBuilder();
            using (var tempDirectory = tempDirectoryBuilder.Create())
            {
                Assert.Contains(tempDirectory.FullName, tempDirectory.Name);
            }
        }

        private static ITempDirectoryBuilder CreateTempDirectoryBuilder()
        {
            return new TempDirectoryBuilder();
        }
    }
}
