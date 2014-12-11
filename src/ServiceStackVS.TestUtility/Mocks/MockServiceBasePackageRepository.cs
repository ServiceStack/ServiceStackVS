using System;
using System.Collections.Generic;
using System.Linq;
using NuGet;

namespace ServiceStackVS.TestUtility.Mocks
{
    public class MockServiceBasePackageRepository : MockPackageRepository, IServiceBasedRepository
    {
        public IQueryable<IPackage> Search(string searchTerm, IEnumerable<string> targetFrameworks, bool allowPrereleaseVersions)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPackage> GetUpdates(IEnumerable<IPackageName> packages, bool includePrerelease, bool includeAllVersions, IEnumerable<System.Runtime.Versioning.FrameworkName> targetFrameworks, IEnumerable<IVersionSpec> versionConstraints)
        {
            // only keep the latest version of each package Id to mimic the behavior of nuget.org GetUpdates() service method
            packages = packages.OrderByDescending(p => p.Version).Distinct(PackageEqualityComparer.Id);

            return this.GetUpdatesCore(packages, includePrerelease, includeAllVersions, targetFrameworks, versionConstraints);
        }
    }
}