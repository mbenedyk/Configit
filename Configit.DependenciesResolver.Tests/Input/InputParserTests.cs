using Configit.DependenciesResolver.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Configit.DependenciesResolver.Tests.Input
{
    [TestClass]
    public class InputParserTests
    {
        private InputParser _unitUnderTest;

        [TestInitialize]
        public void Initialize()
        {
            _unitUnderTest = new InputParser();
        }

        [TestMethod]
        public void When_parsing_input_having_one_packages_and_two_dependencies_then_same_values_in_result()
        {
            // Arrange
            var input = "1\r\nA,1\r\n2\r\nA,1,B,2\r\nA,1,B,1";

            // Act
            var result = _unitUnderTest.Parse(input);

            // Assert
            Assert.AreEqual(1, result.NumberOrPackages);
            Assert.AreEqual(result.NumberOrPackages, result.Packages.Count);
            Assert.AreEqual(2, result.NumberOfDependencies);
            Assert.AreEqual(result.NumberOfDependencies, result.Dependencies.Count);
        }

        [TestMethod]
        public void When_parsing_input_having_one_packages_and_no_dependencies_then_same_values_in_result()
        {
            // Arrange
            var input = "1\r\nA,1";

            // Act
            var result = _unitUnderTest.Parse(input);

            // Assert
            Assert.AreEqual(1, result.NumberOrPackages);
            Assert.AreEqual(result.NumberOrPackages, result.Packages.Count);
            Assert.AreEqual(0, result.NumberOfDependencies);
            Assert.AreEqual(result.NumberOfDependencies, result.Dependencies.Count);
        }

        [TestMethod]
        public void When_parsing_input_having_zero_packages_then_empty_result()
        {
            // Arrange
            var input = "0";

            // Act
            var result = _unitUnderTest.Parse(input);

            // Assert
            Assert.AreEqual(0, result.NumberOrPackages);
            Assert.AreEqual(result.NumberOrPackages, result.Packages.Count);
            Assert.AreEqual(0, result.NumberOfDependencies);
            Assert.AreEqual(result.NumberOfDependencies, result.Dependencies.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void When_parsing_invalid_input_then_InvalidInputException()
        {
            // Arrange
            var input = "a";

            // Act
            _unitUnderTest.Parse(input);
        }
    }
}