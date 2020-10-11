using Configit.DependenciesResolver.Common;

namespace Configit.DependenciesResolver.Storage
{
    /// <summary>
    /// Strategy responsible for creating new package without dependencies and registering it in registry, when requested package wont be found.
    /// </summary>
    public class CreateNewPackageStrategy : IPackageNotFoundStrategy
    {
        public Package HandleNotFoundPackage(PackageIdentifier identifier, IPackageRegistry registry)
        {
            // + log
            var packageDefinition = new PackageDefinition(identifier);
            return registry.RegisterPackage(packageDefinition);
        }
    }
}