using System;
using System.Collections.Generic;
using System.Linq;
using Configit.DependenciesResolver.Common;

namespace Configit.DependenciesResolver.Storage
{
    public class PackageStorage : IPackageStorage, IPackageRegistry
    {
        private readonly Dictionary<PackageIdentifier, Package> _definitions = new Dictionary<PackageIdentifier, Package>();
        private readonly IPackageNotFoundStrategy _notFoundStrategy;

        public PackageStorage(IEnumerable<PackageDefinition> definitions, IPackageNotFoundStrategy notFoundStrategy)
        {
            _notFoundStrategy = notFoundStrategy ?? throw new ArgumentNullException(nameof(notFoundStrategy));
            definitions = definitions?.ToList() ?? throw new ArgumentNullException(nameof(definitions));

            BuildKeys(definitions);
            BuildDependencies(definitions);
        }

        private void BuildDependencies(IEnumerable<PackageDefinition> definitions)
        {
            foreach (var packageDefinition in definitions)
            {
                foreach (var dependency in packageDefinition.DependentOn)
                {
                    _definitions[packageDefinition.Identifier].AddDependency(_definitions[dependency.Identifier]);
                }
            }
        }

        private void BuildKeys(IEnumerable<PackageDefinition> definitions)
        {
            foreach (var packageDefinition in definitions)
            {
                if (!_definitions.ContainsKey(packageDefinition.Identifier))
                {
                    _definitions.Add(packageDefinition.Identifier, new Package(packageDefinition.Identifier));
                }
                
                if (!packageDefinition.DependentOn.Any())
                {
                    continue;
                }

                // definitions are build in a way that I dont need to check sub dependencies
                foreach (var dependency in packageDefinition.DependentOn)
                {
                    if (_definitions.ContainsKey(dependency.Identifier))
                    {
                        continue;
                    }

                    _definitions.Add(dependency.Identifier, new Package(dependency.Identifier));
                }
            }
        }

        public Package GetPackage(PackageIdentifier identifier)
        {
            return GetPackageInternal(identifier);
        }

        public IEnumerable<Package> GetPackages(IEnumerable<PackageIdentifier> packageIdentifiers)
        {
            foreach (var identifier in packageIdentifiers)
            {
                yield return GetPackageInternal(identifier);
            }
        }

        private Package GetPackageInternal(PackageIdentifier identifier)
        {
            if (_definitions.TryGetValue(identifier, out var package))
            {
                return package;
            }

            var newPackage = _notFoundStrategy.HandleNotFoundPackage(identifier, this);
            if (newPackage == null)
            {
                throw new PackageNotFoundException();
            }

            return newPackage;
        }

        IReadOnlyDictionary<PackageIdentifier, Package> IPackageRegistry.Packages => _definitions;

        Package IPackageRegistry.RegisterPackage(PackageDefinition package)
        {
            BuildKeys(new[] {package});
            BuildDependencies(new[] {package});
            return _definitions[package.Identifier];
        }
    }
}