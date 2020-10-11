using System.Linq;
using Configit.DependenciesResolver.Common;
using Configit.DependenciesResolver.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Configit.DependenciesResolver.Tests.Storage
{
    [TestClass]
    public class CreateNewPackageStrategyTests
    {
        private CreateNewPackageStrategy _unitUnderTest;
        private IPackageRegistry _packageRegistry;

        [TestInitialize]
        public void Initialize()
        {
            _unitUnderTest = new CreateNewPackageStrategy();
            _packageRegistry = Substitute.For<IPackageRegistry>();
        }

        [TestMethod]
        public void When_HandleNotFoundPackage_called_then_package_without_dependencies_added_to_registry()
        {
            var newPackageIdentifier = new PackageIdentifier("any", "any");

            _packageRegistry.RegisterPackage(Arg.Any<PackageDefinition>()).Returns(_ => null);
            var result = _unitUnderTest.HandleNotFoundPackage(newPackageIdentifier, _packageRegistry);

            Assert.IsNull(result);
            _packageRegistry.Received().RegisterPackage(Arg.Is<PackageDefinition>(definition =>
                !definition.DependentOn.Any() && definition.Identifier == newPackageIdentifier));
        }
    }
}