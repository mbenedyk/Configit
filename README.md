# Configit

Please look at `Configit.DependenciesResolver.Tests` project. In test class `IntegrationTests.PackageManagerIntegrationTest` you can see how to setup all components

# High level concept
```plantuml
actor CLient
Client -> PackageManager : Resolve(PackageIdentifier[])
PackageManager -> PackageStorage : GetPackages(PackageIdentifier[])
PackageStorage -> PackageRegistry : Get(PackageIdentifier)
PackageRegistry --> PackageStorage : package
alt package == null (not found)
PackageStorage -> CreateNewPackageStrategy
create Package
CreateNewPackageStrategy -> Package : new
Package --> CreateNewPackageStrategy
CreateNewPackageStrategy --> PackageStorage
end
PackageStorage --> PackageManager
PackageManager -> DependencyRoslutionStrategy : CanResolveConflicts
DependencyRoslutionStrategy --> PackageManager : result
PackageManager --> Client : result
```