using System;

namespace Configit.DependenciesResolver.Common
{
    public class PackageIdentifier
    {
        public PackageIdentifier(string name, string version)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Version = version ?? throw new ArgumentNullException(nameof(version));
        }

        public string Name { get; }

        // version type can be changed, what is important is to have and IComparable implementations for that type
        public string Version { get; }

        protected bool Equals(PackageIdentifier other)
        {
            return Name == other.Name && Version == other.Version;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PackageIdentifier) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name.GetHashCode() * 397) ^ Version.GetHashCode();
            }
        }

        public static bool operator ==(PackageIdentifier left, PackageIdentifier right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PackageIdentifier left, PackageIdentifier right)
        {
            return !Equals(left, right);
        }
    }
}
