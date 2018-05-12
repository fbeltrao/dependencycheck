using DependencyCheck;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Xunit;

namespace DependencyCheckCoreTest
{
    [Collection("ZipFolder collection")]
    public class DependencyCheckApplicationTest
    {
        private readonly ZipFolderFixture fixture;

        StringBuilder output;
        TextWriter textOutput;

        StringBuilder error;
        TextWriter errorOutput;
        DependencyCheckApplication target;

 
        public DependencyCheckApplicationTest(ZipFolderFixture fixture)
        {
            output = new StringBuilder();
            textOutput = new StringWriter(output);
            target = new DependencyCheckApplication();
            error = new StringBuilder();
            errorOutput = new StringWriter(error);

            target.Out = textOutput;
            target.Error = errorOutput;
            this.fixture = fixture;
        }

        [Fact]
        public void PassingWrongPath_ShowsError()
        {
            const string folder = "c:\\ijn3n3n12h3hh3h1h1h1";
            var result = target.Execute("-f", folder);
            Assert.Equal(1, result);
            Assert.Equal(0, string.Compare($"Folder {folder} does not exist\r\n", error.ToString()));

           // Assert.Matches complains about \i
        }

        [Fact]
        public void Test_AzureFunctionWithCosmos()
        {
            var folder = fixture.GetAzureFunctionFolder();

            var result = target.Execute("-f", folder);
            Assert.Equal(0, result);

            var actualOutput = output.ToString();
            #region Expected result
            var expected = $"Scanning folder '{folder}'" +
               @"
Microsoft.WindowsAzure.Storage
  8.5.0.0
    DurableTask.AzureStorage, Version=2.0.0.4

  8.6.0.0
    IceCreamOrdersV2, Version=1.0.0.0
    Microsoft.Azure.WebJobs.Extensions, Version=3.0.0.0
    Microsoft.Azure.WebJobs.Host, Version=3.0.0.0


Newtonsoft.Json
  9.0.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0
    Microsoft.Azure.Documents.ChangeFeedProcessor, Version=1.0.0.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0

  10.0.0.0
    DurableTask.AzureStorage, Version=2.0.0.4
    DurableTask.Core, Version=2.0.0.4
    IceCreamOrdersV2, Version=1.0.0.0
    Microsoft.AspNetCore.JsonPatch, Version=2.0.0.0
    Microsoft.AspNetCore.Mvc.Formatters.Json, Version=2.0.2.0
    Microsoft.AspNetCore.Mvc.WebApiCompatShim, Version=2.0.2.0
    Microsoft.Azure.WebJobs.Extensions, Version=3.0.0.0
    Microsoft.Azure.WebJobs.Extensions.CosmosDB, Version=3.0.0.0
    Microsoft.Azure.WebJobs.Extensions.DurableTask, Version=1.3.1.0
    Microsoft.Azure.WebJobs.Extensions.Http, Version=3.0.0.0
    Microsoft.Azure.WebJobs.Host, Version=3.0.0.0
    Microsoft.Extensions.Configuration.Json, Version=2.0.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0
    System.Net.Http.Formatting, Version=5.2.4.0


System.Collections
  4.0.0.0
    Microsoft.Data.Edm, Version=5.8.1.0
    Microsoft.Data.OData, Version=5.8.1.0
    NCrontab, Version=3.2.20120.0
    System.Spatial, Version=5.8.1.0

  4.0.10.0
    Dynamitey, Version=2.0.6.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0

  4.1.1.0
    System.Collections.Concurrent, Version=4.0.14.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0
    System.Diagnostics.DiagnosticSource, Version=4.0.3.0
    System.Linq, Version=4.2.1.0
    System.Linq.Expressions, Version=4.2.1.0
    System.Linq.Queryable, Version=4.0.3.0
    System.Net.WebHeaderCollection, Version=4.1.1.0
    System.ObjectModel, Version=4.1.1.0
    System.Runtime.Serialization.Formatters, Version=4.0.3.0
    System.Security.Claims, Version=4.1.1.0
    System.Text.RegularExpressions, Version=4.2.1.0


System.Collections.Concurrent
  4.0.10.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.0.14.0
    System.Runtime.Serialization.Formatters, Version=4.0.3.0


System.Collections.NonGeneric
  4.0.1.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.1.1.0
    System.ComponentModel.Primitives, Version=4.2.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0
    System.Runtime.Serialization.Formatters, Version=4.0.3.0


System.Collections.Specialized
  4.0.1.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.1.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0
    System.Net.WebHeaderCollection, Version=4.1.1.0


System.Diagnostics.Debug
  4.0.0.0
    Microsoft.Data.Edm, Version=5.8.1.0
    Microsoft.Data.OData, Version=5.8.1.0
    NCrontab, Version=3.2.20120.0

  4.0.10.0
    Dynamitey, Version=2.0.6.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0

  4.1.1.0
    System.Collections.Concurrent, Version=4.0.14.0
    System.Collections.NonGeneric, Version=4.1.1.0
    System.Diagnostics.DiagnosticSource, Version=4.0.3.0
    System.Linq, Version=4.2.1.0
    System.Linq.Queryable, Version=4.0.3.0
    System.ObjectModel, Version=4.1.1.0
    System.Security.Cryptography.Primitives, Version=4.1.1.0
    System.Text.RegularExpressions, Version=4.2.1.0


System.Diagnostics.Tools
  4.0.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.1.1.0
    System.Linq, Version=4.2.1.0
    System.Linq.Expressions, Version=4.2.1.0


System.Diagnostics.TraceSource
  4.0.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.1.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0


System.Diagnostics.Tracing
  4.0.20.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0

  4.1.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.2.1.0
    System.Collections.Concurrent, Version=4.0.14.0
    System.Diagnostics.DiagnosticSource, Version=4.0.3.0
    System.Net.WebHeaderCollection, Version=4.1.1.0


System.Globalization
  4.0.0.0
    Microsoft.Data.Edm, Version=5.8.1.0
    Microsoft.Data.OData, Version=5.8.1.0
    NCrontab, Version=3.2.20120.0
    System.Spatial, Version=5.8.1.0

  4.0.10.0
    Dynamitey, Version=2.0.6.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0


System.IO
  4.0.0.0
    Microsoft.Data.Edm, Version=5.8.1.0
    Microsoft.Data.OData, Version=5.8.1.0
    NCrontab, Version=3.2.20120.0
    System.Spatial, Version=5.8.1.0

  4.0.10.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0

  4.1.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0


System.IO.FileSystem
  4.0.1.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0

  4.1.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0


System.Linq
  4.0.0.0
    Dynamitey, Version=2.0.6.0
    Microsoft.Data.Edm, Version=5.8.1.0
    Microsoft.Data.OData, Version=5.8.1.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0
    System.Spatial, Version=5.8.1.0

  4.1.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.2.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0
    System.Linq.Expressions, Version=4.2.1.0
    System.Linq.Queryable, Version=4.0.3.0
    System.Net.WebHeaderCollection, Version=4.1.1.0


System.Linq.Expressions
  4.0.0.0
    Microsoft.Data.OData, Version=5.8.1.0

  4.0.10.0
    Dynamitey, Version=2.0.6.0

  4.1.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.2.1.0
    System.Dynamic.Runtime, Version=4.1.1.0
    System.Linq.Queryable, Version=4.0.3.0


System.Net.Security
  4.0.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.1.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0


System.ObjectModel
  4.0.10.0
    Dynamitey, Version=2.0.6.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.1.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0
    System.Linq.Expressions, Version=4.2.1.0


System.Reflection
  4.0.0.0
    Microsoft.Data.Edm, Version=5.8.1.0
    Microsoft.Data.OData, Version=5.8.1.0
    System.Spatial, Version=5.8.1.0

  4.0.10.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0

  4.1.0.0
    Dynamitey, Version=2.0.6.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0


System.Reflection.Emit.ILGeneration
  4.0.0.0
    ImpromptuInterface, Version=7.0.1.0

  4.0.3.0
    System.Text.RegularExpressions, Version=4.2.1.0


System.Resources.ResourceManager
  4.0.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0
    Microsoft.Data.Edm, Version=5.8.1.0
    Microsoft.Data.OData, Version=5.8.1.0
    NCrontab, Version=3.2.20120.0
    System.Spatial, Version=5.8.1.0

  4.1.1.0
    System.Collections.Concurrent, Version=4.0.14.0
    System.Collections.NonGeneric, Version=4.1.1.0
    System.Collections.Specialized, Version=4.1.1.0
    System.ComponentModel.Primitives, Version=4.2.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0
    System.Linq, Version=4.2.1.0
    System.Linq.Queryable, Version=4.0.3.0
    System.Net.WebHeaderCollection, Version=4.1.1.0
    System.ObjectModel, Version=4.1.1.0
    System.Runtime.Numerics, Version=4.1.1.0
    System.Runtime.Serialization.Formatters, Version=4.0.3.0
    System.Runtime.Serialization.Primitives, Version=4.2.1.0
    System.Security.Claims, Version=4.1.1.0
    System.Security.Cryptography.OpenSsl, Version=4.1.1.0
    System.Security.Cryptography.Primitives, Version=4.1.1.0
    System.Text.RegularExpressions, Version=4.2.1.0


System.Runtime
  4.0.0.0
    Microsoft.Data.Edm, Version=5.8.1.0
    Microsoft.Data.OData, Version=5.8.1.0
    NCrontab, Version=3.2.20120.0
    System.Runtime.CompilerServices.Unsafe, Version=4.0.3.0
    System.Spatial, Version=5.8.1.0

  4.0.20.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0

  4.1.0.0
    Dynamitey, Version=2.0.6.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.2.1.0
    System.Collections.Concurrent, Version=4.0.14.0
    System.Collections.NonGeneric, Version=4.1.1.0
    System.Collections.Specialized, Version=4.1.1.0
    System.ComponentModel, Version=4.0.3.0
    System.ComponentModel.Primitives, Version=4.2.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0
    System.Diagnostics.DiagnosticSource, Version=4.0.3.0
    System.Dynamic.Runtime, Version=4.1.1.0
    System.IO.FileSystem.Primitives, Version=4.1.1.0
    System.Linq, Version=4.2.1.0
    System.Linq.Expressions, Version=4.2.1.0
    System.Linq.Queryable, Version=4.0.3.0
    System.Net.WebHeaderCollection, Version=4.1.1.0
    System.ObjectModel, Version=4.1.1.0
    System.Runtime.Numerics, Version=4.1.1.0
    System.Runtime.Serialization.Formatters, Version=4.0.3.0
    System.Runtime.Serialization.Primitives, Version=4.2.1.0
    System.Security.Claims, Version=4.1.1.0
    System.Security.Cryptography.OpenSsl, Version=4.1.1.0
    System.Security.Cryptography.Primitives, Version=4.1.1.0
    System.Security.Principal, Version=4.1.1.0
    System.Text.RegularExpressions, Version=4.2.1.0
    System.Xml.ReaderWriter, Version=4.2.1.0
    System.Xml.XmlDocument, Version=4.1.1.0


System.Runtime.Extensions
  4.0.0.0
    Microsoft.Data.Edm, Version=5.8.1.0
    Microsoft.Data.OData, Version=5.8.1.0
    System.Spatial, Version=5.8.1.0

  4.0.10.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0

  4.1.0.0
    Dynamitey, Version=2.0.6.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.2.1.0
    System.Collections.Concurrent, Version=4.0.14.0
    System.Collections.NonGeneric, Version=4.1.1.0
    System.Collections.Specialized, Version=4.1.1.0
    System.ComponentModel.Primitives, Version=4.2.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0
    System.Diagnostics.DiagnosticSource, Version=4.0.3.0
    System.Linq, Version=4.2.1.0
    System.Linq.Expressions, Version=4.2.1.0
    System.Net.WebHeaderCollection, Version=4.1.1.0
    System.Runtime.Numerics, Version=4.1.1.0
    System.Runtime.Serialization.Formatters, Version=4.0.3.0
    System.Security.Claims, Version=4.1.1.0
    System.Security.Cryptography.OpenSsl, Version=4.1.1.0
    System.Text.RegularExpressions, Version=4.2.1.0
    System.Threading, Version=4.1.1.0
    System.Threading.Thread, Version=4.1.1.0


System.Runtime.InteropServices
  4.0.20.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0

  4.1.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.2.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0
    System.Security.Cryptography.OpenSsl, Version=4.1.1.0


System.Security.Cryptography.Algorithms
  4.0.0.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0

  4.2.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.3.1.0
    System.Security.Cryptography.OpenSsl, Version=4.1.1.0


System.Security.Cryptography.Primitives
  4.0.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0

  4.1.1.0
    System.Security.Cryptography.OpenSsl, Version=4.1.1.0


System.Text.Encoding
  4.0.0.0
    Microsoft.Data.OData, Version=5.8.1.0

  4.0.10.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0


System.Text.Encoding.Extensions
  4.0.0.0
    Microsoft.Data.OData, Version=5.8.1.0

  4.0.10.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0

  4.1.1.0
    System.Runtime.Serialization.Formatters, Version=4.0.3.0


System.Text.RegularExpressions
  4.0.0.0
    Microsoft.Data.Edm, Version=5.8.1.0
    Microsoft.Data.OData, Version=5.8.1.0
    System.Spatial, Version=5.8.1.0

  4.0.10.0
    Dynamitey, Version=2.0.6.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0

  4.2.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0


System.Threading
  4.0.0.0
    Microsoft.Data.Edm, Version=5.8.1.0
    Microsoft.Data.OData, Version=5.8.1.0
    System.Spatial, Version=5.8.1.0

  4.0.10.0
    Dynamitey, Version=2.0.6.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0

  4.1.1.0
    System.Collections.Concurrent, Version=4.0.14.0
    System.Collections.NonGeneric, Version=4.1.1.0
    System.Collections.Specialized, Version=4.1.1.0
    System.ComponentModel.Primitives, Version=4.2.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0
    System.Diagnostics.DiagnosticSource, Version=4.0.3.0
    System.ObjectModel, Version=4.1.1.0
    System.Runtime.Serialization.Formatters, Version=4.0.3.0
    System.Security.Cryptography.Primitives, Version=4.1.1.0
    System.Text.RegularExpressions, Version=4.2.1.0


System.Threading.Tasks
  4.0.0.0
    Microsoft.Data.OData, Version=5.8.1.0

  4.0.10.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0

  4.1.1.0
    System.Collections.Concurrent, Version=4.0.14.0
    System.Security.Cryptography.Primitives, Version=4.1.1.0


System.Threading.Timer
  4.0.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0

  4.1.1.0
    System.ComponentModel.TypeConverter, Version=4.2.1.0


System.Xml.ReaderWriter
  4.0.0.0
    Microsoft.Data.Edm, Version=5.8.1.0
    Microsoft.Data.OData, Version=5.8.1.0
    System.Spatial, Version=5.8.1.0

  4.0.10.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0
";
            #endregion
            Utils.AssertMultiLineAreSame(expected, output.ToString());
        }

        [Fact]
        public void Test_AzureFunctionWithCosmos_FilteredBy_Newtonsoft_Json()
        {
            var folder = fixture.GetAzureFunctionFolder();
            var result = target.Execute("-f", folder, "-s", "Newtonsoft.Json");
            Assert.Equal(0, result);

            var actualOutput = output.ToString();
            #region Expected result
            var expected = $"Scanning folder '{folder}'" +
              @"
Newtonsoft.Json
  9.0.0.0
    Microsoft.Azure.DocumentDB.Core, Version=1.5.1.0
    Microsoft.Azure.Documents.ChangeFeedProcessor, Version=1.0.0.0
    Microsoft.WindowsAzure.Storage, Version=8.6.0.0

  10.0.0.0
    DurableTask.AzureStorage, Version=2.0.0.4
    DurableTask.Core, Version=2.0.0.4
    IceCreamOrdersV2, Version=1.0.0.0
    Microsoft.AspNetCore.JsonPatch, Version=2.0.0.0
    Microsoft.AspNetCore.Mvc.Formatters.Json, Version=2.0.2.0
    Microsoft.AspNetCore.Mvc.WebApiCompatShim, Version=2.0.2.0
    Microsoft.Azure.WebJobs.Extensions, Version=3.0.0.0
    Microsoft.Azure.WebJobs.Extensions.CosmosDB, Version=3.0.0.0
    Microsoft.Azure.WebJobs.Extensions.DurableTask, Version=1.3.1.0
    Microsoft.Azure.WebJobs.Extensions.Http, Version=3.0.0.0
    Microsoft.Azure.WebJobs.Host, Version=3.0.0.0
    Microsoft.Extensions.Configuration.Json, Version=2.0.0.0
    Newtonsoft.Json.Bson, Version=1.0.0.0
    System.Net.Http.Formatting, Version=5.2.4.0
";
            #endregion
            Utils.AssertMultiLineAreSame(expected, output.ToString());
        }
    }
}
