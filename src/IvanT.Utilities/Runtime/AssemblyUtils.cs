// --------------------------------------------------------------------
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// Copyright (c) 2017  Ivan Taturevich
// --------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace IvanT.Utilities.Runtime
{
    public static class AssemblyUtils
    {
        public static IEnumerable<Type> GetInheritedClasses<T>()
            where T : class
        {
            return GetReferencingAssemblies()
                .SelectMany(assembly => assembly
                .DefinedTypes
                .Where(x => x.ImplementedInterfaces.Contains(typeof(T)))
                .Select(x => x.DeclaringType));
        }

        public static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            var currentAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            return from library in dependencies
                   where IsCandidateLibrary(library, currentAssemblyName)
                   select Assembly.Load(new AssemblyName(library.Name));
        }

        private static bool IsCandidateLibrary(RuntimeLibrary library, string assemblyName)
        {
            return library.Name == assemblyName
                   || library.Dependencies.Any(d => d.Name.Contains(assemblyName));
        }
    }
}
