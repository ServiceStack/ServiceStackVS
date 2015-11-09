using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet;

namespace ServiceStackVS.Common
{
    public static class NuGetCoreExtensions
    {
        public static string GetLatestVersionOfPackage(this IPackageRepository packageRepository, string packageId)
        {
            var package = packageRepository.FindPackagesById(packageId).First(x => x.IsLatestVersion);
            return package.Version.ToString();
        }
    }
}
