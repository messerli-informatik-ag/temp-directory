namespace Messerli.TempDirectory;

/// <summary>A temporary subdirectory in the temporary folder of the current user.
/// Disposing will remove the temporary subdirectory.</summary>
public sealed class TempSubdirectory : IDisposable
{
    private bool _isDisposed;

    private TempSubdirectory(string fullName)
    {
        FullName = fullName;
    }

    /// <summary>Gets the full path of the directory.</summary>
    public string FullName { get; }

    /// <summary>Creates a temporary subdirectory in the temporary folder of the current user.</summary>
    public static TempSubdirectory Create(string? prefix = null)
    {
        var fullName = Path.Combine(Path.GetTempPath(), $"{prefix}{Guid.NewGuid()}");
        Directory.CreateDirectory(fullName);
        return new TempSubdirectory(fullName);
    }

    public void Dispose()
    {
        if (!_isDisposed)
        {
            _isDisposed = true;
            Directory.Delete(FullName, recursive: true);
        }
    }

    public override string ToString() => $"{nameof(TempSubdirectory)}({FullName})";
}
