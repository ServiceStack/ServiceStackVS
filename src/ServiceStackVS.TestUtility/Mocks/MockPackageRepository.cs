using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NuGet;

namespace ServiceStackVS.TestUtility.Mocks
{
    public class MockPackageRepository : PackageRepositoryBase, ICollection<IPackage>, ILatestPackageLookup, IOperationAwareRepository, IPackageLookup
    {
        private readonly string _source;
        
        public string LastOperation { get; private set; }
        public string LastMainPackageId { get; private set; }
        public string LastMainPackageVersion { get; private set; }

        public MockPackageRepository()
            : this("")
        {
        }

        public MockPackageRepository(string source)
        {
            Packages = new Dictionary<string, List<IPackage>>();
            _source = source;
        }

        public override string Source
        {
            get
            {
                return _source;
            }
        }

        public override bool SupportsPrereleasePackages
        {
            get
            {
                return true;
            }
        }

        internal Dictionary<string, List<IPackage>> Packages
        {
            get;
            set;
        }

        public override void AddPackage(IPackage package)
        {
            AddPackage(package.Id, package);
        }
        
        public override IQueryable<IPackage> GetPackages()
        {
            return Packages.Values.SelectMany(p => p).AsQueryable();
        }

        public override void RemovePackage(IPackage package)
        {
            List<IPackage> packages;
            if (Packages.TryGetValue(package.Id, out packages))
            {
                packages.Remove(package);
            }

            if (packages.Count == 0)
            {
                Packages.Remove(package.Id);
            }
        }

        private void AddPackage(string id, IPackage package)
        {
            List<IPackage> packages;
            if (!Packages.TryGetValue(id, out packages))
            {
                packages = new List<IPackage>();
                Packages.Add(id, packages);
            }
            packages.Add(package);
        }

        public void Add(IPackage item)
        {
            AddPackage(item);
        }

        public void Clear()
        {
            Packages.Clear();
        }

        public bool Contains(IPackage item)
        {
            return this.Exists(item);
        }

        public void CopyTo(IPackage[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public int Count
        {
            get
            {
                return GetPackages().Count();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(IPackage item)
        {
            if (this.Exists(item))
            {
                RemovePackage(item);
                return true;
            }
            return false;
        }

        public IEnumerator<IPackage> GetEnumerator()
        {
            return GetPackages().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryFindLatestPackageById(string id, out SemanticVersion latestVersion)
        {
            List<IPackage> packages;
            bool result = Packages.TryGetValue(id, out packages);
            if (result && packages.Count > 0)
            {
                packages.Sort((a, b) => b.Version.CompareTo(a.Version));
                latestVersion = packages[0].Version;
                return true;
            }
            else
            {
                latestVersion = null;
                return false;
            }
        }

        public bool TryFindLatestPackageById(string id, bool includePrerelease, out IPackage package)
        {
            List<IPackage> packages;
            bool result = Packages.TryGetValue(id, out packages);
            if (result && packages.Count > 0)
            {
                // remove unlisted packages
                packages.RemoveAll(p => !p.IsListed());

                if (!includePrerelease)
                {
                    packages.RemoveAll(p => !p.IsReleaseVersion());
                }

                if (packages.Count > 0)
                {
                    packages.Sort((a, b) => b.Version.CompareTo(a.Version));
                    package = packages[0];
                    return true;
                }
            }

            package = null;
            return false;
        }

        public IDisposable StartOperation(string operation, string mainPackageId, string mainPackageVersion)
        {
            LastOperation = null;
            LastMainPackageId = null;
            LastMainPackageVersion = null;
            return new DisposableAction(() => 
            { 
                LastOperation = operation;
                LastMainPackageId = mainPackageId;
                LastMainPackageVersion = mainPackageVersion;
            });
        }

        public bool Exists(string packageId, SemanticVersion version)
        {
            return FindPackage(packageId, version) != null;
        }

        public IPackage FindPackage(string packageId, SemanticVersion version)
        {
            // version is only string based
            return GetPackages().Where(p => StringComparer.OrdinalIgnoreCase.Equals(p.Id, packageId) && p.Version == version).OrderByDescending(p => p.Version).FirstOrDefault();
        }

        public IEnumerable<IPackage> FindPackagesById(string packageId)
        {
            List<IPackage> packages;
            if (Packages.TryGetValue(packageId, out packages))
            {
                return packages;
            }
            return Enumerable.Empty<IPackage>();
        }
    }
}
