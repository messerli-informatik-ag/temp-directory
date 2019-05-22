using System.IO;
using Xunit;
using TempDir = Messerli.TempDirectory.TempDirectory;

namespace TempDirectory.Test
{
    public class TempDirectoryTest
    {
        private const string TempDirectoryName = "temp_directory_test";

        [Fact]
        public void CreatesTempDirectory()
        {
            using (var generateFileStructure = new TempDir(TempDirectoryName))
            {
                var path = generateFileStructure.Path;
                Assert.True(Directory.Exists(path));
            }
        }

        [Fact]
        public void RemovesTempDirectory()
        {
            var generateFileStructure = new TempDir(TempDirectoryName);
            var path = generateFileStructure.Path;
            Assert.True(Directory.Exists(path));

            generateFileStructure.Dispose();

            Assert.False(Directory.Exists(path));
        }
    }
}
