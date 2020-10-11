using System.Collections.Generic;
using Configit.DependenciesResolver.Common;

namespace Configit.DependenciesResolver.Storage
{
    /// <summary>
    /// Represents a package container.
    /// </summary>
    public interface IPackageRegistry
    {
        IReadOnlyDictionary<PackageIdentifier, Package> Packages { get; }

        Package RegisterPackage(PackageDefinition package);
    }
}