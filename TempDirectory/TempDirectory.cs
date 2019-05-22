using System;
using System.IO;

namespace Messerli.TempDirectory
{
    internal class TempDirectory : ITempDirectory
    {
        public string Name { get; }

        public string FullName { get; }

        public TempDirectory(string name, string fullName)
        {
            Name = name;
            FullName = fullName;
        }

        public void Dispose()
        {
            Directory.Delete(FullName, true);
        }
    }
}
