using System;
using System.Collections.Generic;
using System.Linq;

namespace Configit.DependenciesResolver.Common
{
    public class PackageDefinition
    {
        public PackageDefinition(PackageIdentifier identifier, IEnumerable<PackageDefinition> dependentOn)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            DependentOn = dependentOn ?? throw new ArgumentNullException(nameof(dependentOn));
        }

        public PackageDefinition(PackageIdentifier identifier)
            : this(identifier, Enumerable.Empty<PackageDefinition>())
        {
        }

        public PackageIdentifier Identifier { get; }

        public IEnumerable<PackageDefinition> DependentOn { get; }
    }
}