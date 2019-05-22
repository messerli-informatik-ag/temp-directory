using System;

namespace Messerli.TempDirectory
{
    public interface ITempDirectory: IDisposable
    {
        string Name { get; }

        string FullName { get; }
    }
}
