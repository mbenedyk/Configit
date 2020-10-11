namespace Configit.DependenciesResolver
{
    public class CanResolvePackagesResponse
    {
        public CanResolvePackagesResponse(bool canResolve)
        {
            CanResolve = canResolve;
        }

        public bool CanResolve { get; }
    }
}