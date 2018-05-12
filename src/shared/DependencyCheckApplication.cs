using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DependencyCheck
{
    /// <summary>
    /// Dependency check application
    /// </summary>
    public class DependencyCheckApplication : CommandLineApplication
    {
        private readonly CommandOption folder;
        private readonly CommandOption displayAll;
        private readonly CommandOption outputType;
        private readonly CommandOption assemblyNameStarts;

        public DependencyCheckApplication()
        {
            Name = "Dependency Check";
            Description = "Checks for dependencies between assemblies in a folder";

            HelpOption("-?|-h|--help");

            this.folder = Option("-f |--folder",
                                       "Directory where the assemblies are located (default is current folder)",
                                       CommandOptionType.SingleValue);

            this.displayAll = Option("-a |--all",
                             "Toggle to display all references. By default only conflicting references are listed",
                             CommandOptionType.NoValue);

            this.outputType = Option("-o |--output",
                           "Optional to define output format (console, json, html)",
                           CommandOptionType.SingleValue);

            this.assemblyNameStarts = Option("-s |--startsWith",
                "Filter assemblies by name (optional)",
                CommandOptionType.SingleValue);

            this.OnExecute(() => InternalExecute());
        }

        int InternalExecute()
        {
            var folderValue = folder.HasValue() ? folder.Value() : Directory.GetCurrentDirectory();
            if (!Directory.Exists(folderValue))
            {
                this.ShowHelp();
                OutputError($"Folder {folderValue} does not exist");
                return 1;
            }

            var referencesAssemblyCollection = ReferencedAssemblyCollection.BuildFromDirectory(folderValue);

            var selectedOutputType = outputType.HasValue() ? outputType.Value().ToLowerInvariant() : "console";

            var assemblyList = displayAll.HasValue() ? referencesAssemblyCollection.GetAll() : referencesAssemblyCollection.GetConflicts();
            switch (selectedOutputType)
            {
                case "json":
                    break;

                default:
                    Out.WriteLine($"Scanning folder '{folderValue}'");
                    OutputToOutChannel(assemblyList);
                    break;
            }

            return 0;
        }

        private void OutputError(string text)
        {
            if (this.Error == Console.Error)
                OutputWithColor(ConsoleColor.Red, text);
            else
                Error.WriteLine(text);

        }

        private void OutputWithColor(ConsoleColor color, string text)
        {
            if (Out == Console.Out)
            {
                var previousColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(text);
                Console.ForegroundColor = previousColor;
            }
            else
            {
                Out.WriteLine(text);
            }
        }
        

        private void OutputToOutChannel(IReadOnlyList<ReferencedAssembly> assemblyList)
        {
            if (assemblyList.Count == 0)
            {
                if (this.displayAll.HasValue())
                    Out.WriteLine("No assembly was found");
                else
                    Out.WriteLine("No conflicting assembly was found");
            }
            for (int i = 0; i < assemblyList.Count; i++)
            {
                ReferencedAssembly asm = assemblyList[i];
                if (Filter(asm))
                {
                    PrintAssemblyReference(asm);
                }
                Out.WriteLine();
            }
        }

        private bool Filter(ReferencedAssembly asm)
        {
            return !assemblyNameStarts.HasValue() ||
                asm.Name.StartsWith(assemblyNameStarts.Value(), StringComparison.InvariantCultureIgnoreCase);
        }

        void PrintAssemblyReference(ReferencedAssembly asm)
        {
            OutputWithColor(asm.HasConflits() ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen, asm.Name);
            foreach (var version in asm.GetVersions())
            {
                OutputWithColor(ConsoleColor.Yellow, $"  {version.ToString()}");

                foreach (var dependent in asm.GetDependents(version))
                {
                    Out.WriteLine($"    {dependent}");
                }

                Out.WriteLine();
            }
        }

       
    }
}
