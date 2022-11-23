using Xunit;

namespace Messerli.TempDirectory.Test;

public sealed class TempSubdirectoryTest
{
    private const string Prefix = "prefix";

    [Fact]
    public void CreatesTempDirectory()
    {
        using var tempDirectory = TempSubdirectory.Create();
        Assert.True(Directory.Exists(tempDirectory.FullName));
    }

    [Fact]
    public void CreatesTempDirectoryWithPrefix()
    {
        using var tempDirectory = TempSubdirectory.Create(Prefix);
        Assert.True(Directory.Exists(tempDirectory.FullName));
        Assert.StartsWith(Prefix, Path.GetFileName(tempDirectory.FullName));
    }

    [Fact]
    public void DeletesTempDirectoryOnDispose()
    {
        var tempDirectory = TempSubdirectory.Create();

        using (tempDirectory)
        {
            Assert.True(Directory.Exists(tempDirectory.FullName));
        }

        Assert.False(Directory.Exists(tempDirectory.FullName));
    }

    [Fact]
    public void AllowsTempDirectoryToBeDisposedMoreThanOnce()
    {
        var tempDirectory = TempSubdirectory.Create();

        using (tempDirectory)
        using (tempDirectory)
        {
        }
    }
}
