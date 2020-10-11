using System;
using System.Collections.Generic;
using System.Linq;

namespace Configit.DependenciesResolver.Common
{
    public class PackageDefinitionBuilder
    {
        private readonly Stack<Data> _cache = new Stack<Data>();
        
        public PackageDefinitionBuilder AddPackage(string name, string version)
        {
            _cache.Push(new Data { Name = name, Version = version });
            return this;
        }

        public PackageDefinitionBuilder AddDependency(string name, string version)
        {
            var currentPackage = _cache.Peek();
            if (currentPackage == null)
            {
                throw new InvalidOperationException("You need to add package before adding dependency");
            }

            currentPackage.Dependencies.Add(new Data { Name = name, Version = version });
            return this;
        }

        public IEnumerable<PackageDefinition> Build()
        {
            return _cache.Select(data => data.ToPackageDefinition()).ToList();
        }

        private class Data
        {
            public string Name { get; set; }
            public string Version { get; set; }

            public List<Data> Dependencies { get; } = new List<Data>();

            public PackageDefinition ToPackageDefinition()
            {
                return new PackageDefinition(new PackageIdentifier(Name, Version), Dependencies.Select(data => data.ToPackageDefinition()));
            }
        }
    }
}