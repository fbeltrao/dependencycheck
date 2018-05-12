using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DependencyCheckCoreTest
{
    class Utils
    {
        internal static void AssertMultiLineAreSame(string t1, string t2)
        {
            var lines1 = t1.Split('\n').Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
            var lines2 = t2.Split('\n').Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

            Assert.Equal(lines1, lines2, StringComparer.InvariantCulture);
        }
    }
}
