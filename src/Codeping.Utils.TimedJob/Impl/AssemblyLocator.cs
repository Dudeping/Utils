using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyModel;

namespace Codeping.Utils.TimedJob
{
    public class AssemblyLocator : IAssemblyLocator
    {
        private static readonly string AssemblyRoot = typeof(IJob).GetTypeInfo().Assembly.GetName().Name;

        private readonly Assembly _entryAssembly;
        private readonly DependencyContext _dependencyContext;

        public AssemblyLocator(IHostingEnvironment environment)
        {
            _entryAssembly = Assembly.Load(new AssemblyName(environment.ApplicationName));

            _dependencyContext = DependencyContext.Load(_entryAssembly);
        }

        public virtual IEnumerable<Assembly> GetAssemblies()
        {
            if (_dependencyContext == null)
            {
                return new[] { _entryAssembly };
            }

            return _dependencyContext
                .RuntimeLibraries
                .Where(IsCandidateLibrary)
                .SelectMany(x => x.GetDefaultAssemblyNames(_dependencyContext))
                .Select(assembly => Assembly.Load(new AssemblyName(assembly.Name)));
        }

        private bool IsCandidateLibrary(RuntimeLibrary library)
        {
            return library.Dependencies.Any(dependency => string.Equals(AssemblyRoot, dependency.Name, StringComparison.Ordinal));
        }
    }
}
