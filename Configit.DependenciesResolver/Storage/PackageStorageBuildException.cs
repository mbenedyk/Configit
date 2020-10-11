using System;

namespace Configit.DependenciesResolver.Storage
{
    // throw when could packages registry cannot be build, caused by in proper dependencies - not used in example.
    public class PackageStorageBuildException : Exception
    {
        public PackageStorageBuildException()
        {
        }

        public PackageStorageBuildException(string message) : base(message)
        {
        }

        public PackageStorageBuildException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}