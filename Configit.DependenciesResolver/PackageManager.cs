using System;
using Configit.DependenciesResolver.ResolutionStrategies;
using Configit.DependenciesResolver.Storage;

namespace Configit.DependenciesResolver
{
    public class PackageManager
    {
        private readonly IPackageStorage _storage;
        private readonly IDependencyResolutionStrategy _dependencyResolutionStrategy;

        public PackageManager(IPackageStorage storage, IDependencyResolutionStrategy dependencyResolutionStrategy)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _dependencyResolutionStrategy = dependencyResolutionStrategy ?? throw new ArgumentNullException(nameof(dependencyResolutionStrategy));
        }

        public CanResolvePackagesResponse CanResolvePackages(CanResolvePackagesRequest canResolvePackagesRequest)
        {
            _ = canResolvePackagesRequest ?? throw new ArgumentNullException(nameof(canResolvePackagesRequest));

            try
            {
                var result = _storage.GetPackages(canResolvePackagesRequest.Packages);
                var canResolveConflicts = _dependencyResolutionStrategy.CanResolveConflicts(result);

                return new CanResolvePackagesResponse(canResolveConflicts);
            }
            catch (PackageNotFoundException pnfe)
            {
                // + log
                return new CanResolvePackagesResponse(false);
            }
        }
    }
}