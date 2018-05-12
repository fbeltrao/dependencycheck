using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyCheck
{

    /// <summary>
    /// Keeps an assembly and all references to it
    /// </summary>
    public class ReferencedAssembly : IComparable<ReferencedAssembly>
    {
        /// <summary>
        /// Defines the assembly name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of dependents per version
        /// </summary>
        Dictionary<Version, SortedSet<string>> dependents;


        /// <summary>
        /// Gets all versions being referenced
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Version> GetVersions()
        {
            var sortedVersions = this.dependents.Keys.ToList();
            sortedVersions.Sort();
            return sortedVersions;
        }

        /// <summary>
        /// Gets all assemblies that depend on a specific version
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public IEnumerable<string> GetDependents(Version version)
        {
            if (this.dependents.TryGetValue(version, out var list))
            {
                return list;
            }

            return new string[0];
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public ReferencedAssembly(string name)
        {
            this.Name = name;
            this.dependents = new Dictionary<Version, SortedSet<string>>();
        }


        /// <summary>
        /// Register a dependency to this assembly
        /// Dependency = an assembly that depends on this assembly
        /// </summary>
        public void RegisterDependency(AssemblyName assemblyName, Version version)
        {
            if (!dependents.TryGetValue(version, out var dependent))
            {
                dependent = new SortedSet<string>();
                dependents[version] = dependent;
            }

            var simpleName = $"{assemblyName.Name}, Version={assemblyName.Version.ToString()}";
            dependent.Add(simpleName);
        }

        /// <summary>
        /// Returns whether or not this assembly is being referenced by distinct versions
        /// </summary>
        /// <returns></returns>
        public bool HasConflits()
        {
            return this.dependents.Count > 1;
        }

        public int CompareTo(ReferencedAssembly other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name.ToString();
        }
    }
}
