using Configit.DependenciesResolver.Common;
using Configit.DependenciesResolver.ResolutionStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Configit.DependenciesResolver.Tests.ResolutionStrategy
{
    [TestClass]
    public class SingleVersionResolutionStrategyTests
    {
        private SingleVersionResolutionStrategy _unitUnderTest;

        [TestInitialize]
        public void Initialize()
        {
            _unitUnderTest = new SingleVersionResolutionStrategy();
        }

        [TestMethod]
        public void When_package_collection_contains_packages_with_same_name_and_different_version_then_cannot_resolve_conflict()
        {
            var p1 = CreatePackage("x", "v1");
            var p2 = CreatePackage("x", "v2");

            var result = _unitUnderTest.CanResolveConflicts(new[] {p1, p2});

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_package_collection_contains_packages_with_unique_names_no_dependencies_then_can_resolve_conflict()
        {
            var p1 = CreatePackage("x", "v1");
            var p2 = CreatePackage("y", "v1");

            var result = _unitUnderTest.CanResolveConflicts(new[] { p1, p2 });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void When_package_collection_contains_packages_with_unique_names_but_dependencies_refers_packages_of_same_name_with_different_version_then_cannot_resolve_conflict()
        {
            var p1 = CreatePackage("x", "v1");
            p1.AddDependency(CreatePackage("a", "v1"));
            var p2 = CreatePackage("y", "v1");
            p2.AddDependency(CreatePackage("a", "v2"));

            var result = _unitUnderTest.CanResolveConflicts(new[] { p1, p2 });

            Assert.IsFalse(result);
        }

        // other tests:
        // empty package collection as input - true
        // cyclomatic dependency - true
        // ...

        private Package CreatePackage(string name, string version)
        {
            return new Package(new PackageIdentifier(name, version));
        }
    }
}