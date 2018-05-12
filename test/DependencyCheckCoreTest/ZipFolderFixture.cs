using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace DependencyCheckCoreTest
{
    public sealed class ZipFolderFixture : IDisposable
    {
        object azureFunctionLock = new object();
        ExtractedZipFolder azureFunction;

        public string GetAzureFunctionFolder()
        {
            if (azureFunction == null)
            {
                lock (azureFunctionLock)
                {
                    if (azureFunction == null)
                    {
                        azureFunction = new ExtractedZipFolder(Path.Combine(Directory.GetCurrentDirectory(), "azurefunction.zip"));
                    }
                }
            }

            return Path.Combine(azureFunction.DestinationPath, "azurefunction");
        }

        public void Dispose()
        {
            azureFunction?.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    [CollectionDefinition("ZipFolder collection")]
    public class DatabaseCollection : ICollectionFixture<ZipFolderFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

}
