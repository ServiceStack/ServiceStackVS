using System;
using System.Collections.Generic;
using System.Linq;
using NuGet;

namespace ServiceStackVS.TestUtility.Mocks
{
    // !!! Should be deleted. Use MockSharedPackageRepository2 instead    
    public class MockSharedPackageRepository : MockPackageRepository, ISharedPackageRepository
    {
        private Dictionary<string, SemanticVersion> _references = 
            new Dictionary<string, SemanticVersion>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, SemanticVersion> _solutionReferences = 
            new Dictionary<string, SemanticVersion>(StringComparer.OrdinalIgnoreCase);

        public MockSharedPackageRepository()
            : this("")
        {
        }

        public MockSharedPackageRepository(string source) : base(source)
        {
        }

        public override void AddPackage(IPackage package)
        {
            base.AddPackage(package);

            if (package.HasProjectContent())
            {
                _references[package.Id] = package.Version;
            }
            else
            {
                _solutionReferences[package.Id] = package.Version;
            }
        }

        public override void RemovePackage(IPackage package)
        {
            base.RemovePackage(package);

            if (package.HasProjectContent())
            {
                _references.Remove(package.Id);
            }
            else
            {
                _solutionReferences.Remove(package.Id);
            }
        }
        
        public bool IsReferenced(string packageId, SemanticVersion version)
        {
            SemanticVersion storedVersion;
            return _references.TryGetValue(packageId, out storedVersion) && storedVersion == version;
        }

        public bool IsSolutionReferenced(string packageId, SemanticVersion version)
        {
            SemanticVersion storedVersion;
            return _solutionReferences.TryGetValue(packageId, out storedVersion) && storedVersion == version;
        }

        public void RegisterRepository(string path)
        {
            throw new NotImplementedException();
        }

        public void UnregisterRepository(string path)
        {
            throw new NotImplementedException();
        }

        public void RegisterRepository(PackageReferenceFile packageReferenceFile)
        {
            throw new NotImplementedException();
        }

        public void UnregisterRepository(PackageReferenceFile packageReferenceFile)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPackageRepository> LoadProjectRepositories()
        {
            return Enumerable.Empty<IPackageRepository>();
        }
    }

    public class MockSharedPackageRepository2 : ISharedPackageRepository
    {
        // Contains the id & version of the solution level packages.
        List<Tuple<string, SemanticVersion>> _solutionReferences;

        // Contains the list of the packages.config files.
        HashSet<PackageReferenceFile> _projectRepositories;

        // List of packages installed in the packages folder.
        List<IPackage> _packages;

        public MockSharedPackageRepository2()
        {
            _solutionReferences = new List<Tuple<string, SemanticVersion>>();
            _projectRepositories = new HashSet<PackageReferenceFile>();
            _packages = new List<IPackage>();
            PackageSaveMode = PackageSaveModes.Nupkg;
        }

        public bool IsReferenced(string packageId, SemanticVersion version)
        {
            return LoadProjectRepositories().Any(r => r.Exists(packageId, version));
        }

        public bool IsSolutionReferenced(string packageId, SemanticVersion version)
        {
            return _solutionReferences.Exists(
                p =>
                {
                    return String.Equals(p.Item1, packageId, StringComparison.OrdinalIgnoreCase) &&
                        p.Item2 == version;
                });
        }

        public void RegisterRepository(string path)
        {
            throw new NotImplementedException();
        }

        public void UnregisterRepository(string path)
        {
            throw new NotImplementedException();
        }

        public void RegisterRepository(PackageReferenceFile packageReferenceFile)
        {
            _projectRepositories.Add(packageReferenceFile);
        }

        public void UnregisterRepository(PackageReferenceFile packageReferenceFile)
        {
            _projectRepositories.Remove(packageReferenceFile);
        }

        public IEnumerable<IPackageRepository> LoadProjectRepositories()
        {
            return _projectRepositories.Select(
                f =>
                {
                    var projectRepository = new PackageReferenceRepository(
                        f.FullPath,
                        sourceRepository: this);
                    return projectRepository;
                });
        }

        public string Source
        {
            get
            {
                return "MockSharedPackageRepository";
            }
        }

        public PackageSaveModes PackageSaveMode
        {
            get;
            set;
        }

        public bool SupportsPrereleasePackages
        {
            get { return true; }
        }

        // The returned packages are sorted by Id then by Version
        public IQueryable<IPackage> GetPackages()
        {
            _packages.Sort(PackageComparer.Version);
            return _packages.AsQueryable();
        }

        public void AddPackage(IPackage package)
        {
            _packages.Add(package);
            if (!package.HasProjectContent())
            {
                // this is a solution level package. Install it to the solution.
                _solutionReferences.Add(Tuple.Create(package.Id, package.Version));
            }
        }

        public void RemovePackage(IPackage package)
        {
            _packages.RemoveAll(
                p =>
                {
                    return String.Equals(p.Id, package.Id, StringComparison.OrdinalIgnoreCase) &&
                        p.Version == package.Version;
                });

            if (!package.HasProjectContent())
            {
                // this is a solution level package. Remove it from the solution.
                _solutionReferences.RemoveAll(
                    p =>
                    {
                        return String.Equals(p.Item1, package.Id, StringComparison.OrdinalIgnoreCase) &&
                            p.Item2 == package.Version;
                    });
            }
        }
    }
}
