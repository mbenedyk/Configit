using System;

namespace Configit.DependenciesResolver.Storage
{
    public class PackageNotFoundException : Exception
    {
        public PackageNotFoundException()
        {
        }

        public PackageNotFoundException(string message) : base(message)
        {
        }

        public PackageNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}