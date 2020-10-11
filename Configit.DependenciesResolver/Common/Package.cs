using System;
using System.Collections.Generic;

namespace Configit.DependenciesResolver.Common
{
    public class Package
    {
        private List<Package> _dependencies = new List<Package>();

        internal Package(PackageIdentifier identifier)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        }

        public PackageIdentifier Identifier { get; }

        public IEnumerable<Package> Dependencies => _dependencies;

        internal void AddDependency(Package newDependency)
        {
            _dependencies.Add(newDependency);
        }
    }
}