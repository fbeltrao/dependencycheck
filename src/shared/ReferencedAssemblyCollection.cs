using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DependencyCheck
{
    public class ReferencedAssemblyCollection
    {
        Dictionary<string, ReferencedAssembly> assemblies;

        internal ReferencedAssemblyCollection()
        {
            this.assemblies = new Dictionary<string, ReferencedAssembly>();
        }

        public static ReferencedAssemblyCollection BuildFromDirectory(string path)
        {
            
            var collection = new ReferencedAssemblyCollection();
            foreach (var file in Directory.GetFiles(path, "*.dll"))
            {
                try
                {
#if NETCOREAPP
                    var asm = Assembly.LoadFrom(file);
#else
                     var asm = Assembly.ReflectionOnlyLoadFrom(file);
#endif

                    if (asm.GetName().Version.ToString() != "0.0.0.0")
                    {
                        foreach (var reference in asm.GetReferencedAssemblies())
                        {
                            if (reference.Version.ToString() != "0.0.0.0")
                            {
                                if (!collection.assemblies.TryGetValue(reference.Name, out var referencedAssembly))
                                {
                                    referencedAssembly = new ReferencedAssembly(reference.Name);
                                    collection.assemblies.Add(referencedAssembly.Name, referencedAssembly);
                                }
                                referencedAssembly.RegisterDependency(asm.GetName(), reference.Version);
                            }
                        }
                    }

                }
                catch (BadImageFormatException)
                {

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                }
            }

            return collection;
        }
        
        
        /// <summary>
        /// Gets all assemblies that have a conflict (more than one version is referenced)
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<ReferencedAssembly> GetConflicts()
        {
            var list = this.assemblies
                .Values
                .Where(asm => asm.HasConflits())
                .ToList();
            list.Sort();
            return list;
        }

        /// <summary>
        /// Gets all assemblies that have a conflict (more than one version is referenced)
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<ReferencedAssembly> GetAll()
        {
            var list = this.assemblies
                .Values
                .ToList();
            list.Sort();
            return list;
        }
    }
}
