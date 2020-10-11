using System.Collections.Generic;

namespace Configit.DependenciesResolver.Input
{
    internal class ParseResult
    {
        public int NumberOrPackages { get; set; }
        public List<string> Packages { get; set; } = new List<string>();
        public int NumberOfDependencies { get; set; }
        public List<string> Dependencies { get; set; } = new List<string>();
    }
}