using System;
using System.Collections.Generic;
using Configit.DependenciesResolver.Common;
using Configit.DependenciesResolver.Input;
using Configit.DependenciesResolver.ResolutionStrategies;
using Configit.DependenciesResolver.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Configit.DependenciesResolver.Tests.Integration
{
    [TestClass]
    public class IntegrationTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestInput), DynamicDataSourceType.Method)]
        public void PackageManagerIntegrationTest(string input, bool expectedResult)
        {
            var inputParser = new InputParser();
            var parseResult = inputParser.Parse(input);

            var packageManager = BootstrapPackageManager(parseResult);

            var packageList = new List<PackageIdentifier>();
            foreach (var resultPackage in parseResult.Packages)
            {
                var packageIdentifierParts = resultPackage.Split(',', StringSplitOptions.RemoveEmptyEntries);
                packageList.Add(new PackageIdentifier(packageIdentifierParts[0], packageIdentifierParts[1]));
            }

            var request = new CanResolvePackagesRequest(packageList);

            var canResolve = packageManager.CanResolvePackages(request);

            Assert.AreEqual(expectedResult, canResolve.CanResolve);
        }

        private static PackageManager BootstrapPackageManager(ParseResult parseResult)
        {
            var packageDefinitionBuilder = new PackageDefinitionBuilder();

            for (var i = 0; i < parseResult.NumberOfDependencies; i++)
            {
                var split = parseResult.Dependencies[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
                packageDefinitionBuilder.AddPackage(split[0], split[1]);
                for (var j = 2; j < split.Length; j += 2)
                {
                    packageDefinitionBuilder.AddDependency(split[j], split[j + 1]);
                }
            }

            var packageDefinitions = packageDefinitionBuilder.Build();

            var storage = new PackageStorage(packageDefinitions, new CreateNewPackageStrategy());
            var packageManager = new PackageManager(storage, new SingleVersionResolutionStrategy());
            return packageManager;
        }

        public static IEnumerable<object[]> GetTestInput()
        {
            yield return new object[] {"1\r\nP1,42\r\n1\r\nP1,42,P2,Beta-1", true};
            yield return new object[] {"1\r\nA,1\r\n2\r\nA,1,B,2\r\nA,1,B,1", false};
            yield return new object[] {"1\r\nB,2\r\n2\r\nB,2,A,1,C,1\r\nC,1,A,2", false};
            yield return new object[] {"1\r\nB,1\r\n1\r\nB,1,B,1", true};
            yield return new object[] {"2\r\nA,2\r\nB,2\r\n3\r\nA,1,B,1\r\nA,1,B,2\r\nA,2,C,3", true};
            yield return new object[]
                {"2\r\nA,2\r\nB,2\r\n5\r\nA,1,B,1\r\nA,1,B,2\r\nA,2,C,3\r\nC,3,D,4\r\nD,4,B,1", false};
            yield return new object[] {"1\r\nB,2\r\n2\r\nA,1,B,2\r\nB,2,A,1\r\n", true};
            yield return new object[] {"2\r\nA,1\r\nC,1\r\n2\r\nA,1,B,1\r\nC,1,B,2", false};
            yield return new object[] {"1\r\nA,1", true};
            yield return new object[] {"3\r\nA,1\r\nB,1\r\nC,1", true};
            yield return new object[] {"3\r\nA,1\r\nB,1\r\nC,1\r\n3\r\nA,1,D,1,E,1\r\nB,1,A,1\r\nC,1,B,1", true};
        }
    }
}
