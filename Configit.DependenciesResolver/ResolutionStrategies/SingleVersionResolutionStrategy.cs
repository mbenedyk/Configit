using System.Collections.Generic;
using System.Linq;
using Configit.DependenciesResolver.Common;

namespace Configit.DependenciesResolver.ResolutionStrategies
{
    public class SingleVersionResolutionStrategy : IDependencyResolutionStrategy
    {
        public bool CanResolveConflicts(IEnumerable<Package> inputPackages)
        {
            var hashSet = new HashSet<PackageIdentifier>();

            foreach (var inputPackage in inputPackages)
            {
                FillDistinctPackageIdentifiers(inputPackage, hashSet);
            }

            var lookup = hashSet.ToLookup(identifier => identifier.Name);

            return !lookup.Any(grouping => grouping.Count() > 1);
        }

        private static void FillDistinctPackageIdentifiers(Package package, HashSet<PackageIdentifier> cache)
        {
            if (cache.Contains(package.Identifier))
            {
                return;
            }

            cache.Add(package.Identifier);

            foreach (var packageDependency in package.Dependencies)
            {
                FillDistinctPackageIdentifiers(packageDependency, cache);
            }
        }
    }
}