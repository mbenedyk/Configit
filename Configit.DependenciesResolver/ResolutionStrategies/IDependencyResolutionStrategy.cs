using System.Collections.Generic;
using Configit.DependenciesResolver.Common;

namespace Configit.DependenciesResolver.ResolutionStrategies
{
    public interface IDependencyResolutionStrategy
    {
        public bool CanResolveConflicts(IEnumerable<Package> inputPackages);
    }
}