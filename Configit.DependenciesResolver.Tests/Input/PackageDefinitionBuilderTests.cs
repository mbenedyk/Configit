using System;
using System.Linq;
using Configit.DependenciesResolver.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Configit.DependenciesResolver.Tests.Input
{
    [TestClass]
    public class PackageDefinitionBuilderTests
    {
        private PackageDefinitionBuilder _unitUnderTest;

        [TestInitialize]
        public void Initialize()
        {
            _unitUnderTest = new PackageDefinitionBuilder();
        }

        [TestMethod]
        public void When_only_packages_are_added_then_Build_returns_package_definitions_without_dependencies()
        {
            _unitUnderTest.AddPackage("p1", "v1");
            _unitUnderTest.AddPackage("p2", "v2");
            _unitUnderTest.AddPackage("p3", "v3");

            var result = _unitUnderTest.Build().ToList();

            Assert.AreEqual(3, result.Count);
            Assert.IsNotNull(result.SingleOrDefault(definition =>
                definition.Identifier.Name == "p1" && definition.Identifier.Version == "v1"));
            Assert.IsNotNull(result.SingleOrDefault(definition =>
                definition.Identifier.Name == "p2" && definition.Identifier.Version == "v2"));
            Assert.IsNotNull(result.SingleOrDefault(definition =>
                definition.Identifier.Name == "p3" && definition.Identifier.Version == "v3"));
            foreach (var packageDefinition in result)
            {
                Assert.AreEqual(0, packageDefinition.DependentOn.Count());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void When_adding_dependency_before_package_then_InvalidOperationException()
        {
            _unitUnderTest.AddDependency("any", "any");
        }

        [TestMethod]
        public void When_adding_dependency_after_adding_package_then_when_build_result_contains_dependencies()
        {
            _unitUnderTest.AddPackage("p1", "v1");
            _unitUnderTest.AddDependency("d1", "v1");
            _unitUnderTest.AddDependency("d2", "v1");
            _unitUnderTest.AddPackage("p2", "v2");
            _unitUnderTest.AddDependency("d1", "v1");
            _unitUnderTest.AddDependency("d3", "v1");
            
            var result = _unitUnderTest.Build().ToList();

            Assert.AreEqual(2, result.Count);
            var p1Package = result.SingleOrDefault(definition =>
                definition.Identifier.Name == "p1" && definition.Identifier.Version == "v1");
            Assert.IsNotNull(p1Package);
            
            Assert.IsNotNull(p1Package.DependentOn.SingleOrDefault(definition => definition.Identifier.Name == "d1" && definition.Identifier.Version == "v1"));
            Assert.IsNotNull(p1Package.DependentOn.SingleOrDefault(definition => definition.Identifier.Name == "d2" && definition.Identifier.Version == "v1"));

            var p2Package = result.SingleOrDefault(definition =>
                definition.Identifier.Name == "p2" && definition.Identifier.Version == "v2");
            Assert.IsNotNull(p2Package);
            Assert.IsNotNull(p2Package.DependentOn.SingleOrDefault(definition => definition.Identifier.Name == "d1" && definition.Identifier.Version == "v1"));
            Assert.IsNotNull(p2Package.DependentOn.SingleOrDefault(definition => definition.Identifier.Name == "d3" && definition.Identifier.Version == "v1"));
        }
    }
}