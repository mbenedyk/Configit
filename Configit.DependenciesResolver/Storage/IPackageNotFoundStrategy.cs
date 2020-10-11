using Configit.DependenciesResolver.Common;

namespace Configit.DependenciesResolver.Storage
{
    /// <summary>
    /// Defines how non registered package will be resolved.
    /// </summary>
    public interface IPackageNotFoundStrategy
    {
        /// <summary>
        /// Handles non registered package scenario.
        /// If return null, then <see cref="PackageNotFoundException"/> will be thrown, otherwise package will be resolved.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="registry"></param>
        /// <returns></returns>
        Package HandleNotFoundPackage(PackageIdentifier identifier, IPackageRegistry registry);
    }
}