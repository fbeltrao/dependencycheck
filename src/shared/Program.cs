using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DependencyCheck
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = new DependencyCheckApplication();
            var res = app.Execute(args);

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press <ENTER> to exit");
                Console.ReadLine();
            }

            return res;
        }
    }
}
