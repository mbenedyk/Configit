using System;
using System.Collections.Generic;
using Configit.DependenciesResolver.Common;

namespace Configit.DependenciesResolver
{
    public class CanResolvePackagesRequest
    {
        public CanResolvePackagesRequest(IEnumerable<PackageIdentifier> packages)
        {
            Packages = packages ?? throw new ArgumentNullException(nameof(packages));
        }

        public IEnumerable<PackageIdentifier> Packages { get; }
    }
}