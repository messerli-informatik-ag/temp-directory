using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Messerli.TempDirectory
{
    public class TempDirectory : IDisposable
    {
        public string Path { get; }

        public TempDirectory(string directoryName)
        {
            try
            {
                Path = CreateTempDirectory(directoryName);
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        public void Dispose()
        {
            Directory.Delete(Path, true);
        }

        private static string CreateTempDirectory(string directoryName)
        {
            var tempPath = System.IO.Path.GetTempPath();
            var path = System.IO.Path.Combine(tempPath, directoryName);

            Directory.CreateDirectory(path);

            return path;
        }


    }
}
