using System.Collections.Generic;
using Configit.DependenciesResolver.Common;

namespace Configit.DependenciesResolver.Storage
{
    /// <summary>
    /// Represents package storage.
    /// </summary>
    public interface IPackageStorage
    {
        Package GetPackage(PackageIdentifier identifier);

        IEnumerable<Package> GetPackages(IEnumerable<PackageIdentifier> packageIdentifiers);
    }
}