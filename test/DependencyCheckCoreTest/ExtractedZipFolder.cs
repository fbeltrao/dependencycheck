using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace DependencyCheckCoreTest
{
    /// <summary>
    /// Extracts a zip file to a temp folder and delete the contents on Dispose()
    /// </summary>
    public sealed class ExtractedZipFolder : IDisposable
    {
        public string DestinationPath { get; private set; }

        public ExtractedZipFolder(string zipFilePath)
        {
            this.DestinationPath = Path.Combine(Path.GetTempPath(), $"{Path.GetFileNameWithoutExtension(zipFilePath)}_{Guid.NewGuid().ToString()}");
            Directory.CreateDirectory(this.DestinationPath);
            ZipFile.ExtractToDirectory(zipFilePath, this.DestinationPath);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (this.DestinationPath.Length > 0)
            {
                try
                {
                    Directory.Delete(this.DestinationPath, true);
                    this.DestinationPath = string.Empty;
                }
                catch
                {

                }
            }
        }
    }
}
