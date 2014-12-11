using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NuGet;

namespace ServiceStackVS.TestUtility.Mocks
{
    public class MockFileSystem : IFileSystem
    {
        private ILogger _logger;
        private Dictionary<string, DateTime> _createdTime;

        public MockFileSystem()
            : this(@"C:\MockFileSystem\")
        {

        }

        public MockFileSystem(string root)
        {
            Root = root;
            Paths = new Dictionary<string, Func<Stream>>(StringComparer.OrdinalIgnoreCase);
            Deleted = new HashSet<string>();
            _createdTime = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
        }

        public virtual ILogger Logger
        {
            get
            {
                return _logger ?? NullLogger.Instance;
            }
            set
            {
                _logger = value;
            }
        }

        public virtual string Root
        {
            get;
            private set;
        }

        public virtual IDictionary<string, Func<Stream>> Paths
        {
            get;
            private set;
        }

        public virtual HashSet<string> Deleted
        {
            get;
            private set;
        }

        public virtual void CreateDirectory(string path)
        {
            Paths.Add(path, null);
        }

        public virtual void DeleteDirectory(string path, bool recursive = false)
        {
            foreach (var file in Paths.Keys.ToList())
            {
                if (file.StartsWith(path))
                {
                    Paths.Remove(file);
                }
            }
            Deleted.Add(path);
        }

        public virtual string GetFullPath(string path)
        {
            return Path.Combine(Root, path);
        }

        public virtual IEnumerable<string> GetFiles(string path, bool recursive)
        {
            var files = Paths.Select(f => f.Key);
            if (recursive)
            {
                path = PathUtility.EnsureTrailingSlash(path);
                files = files.Where(f => f.StartsWith(path, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                files = files.Where(f => Path.GetDirectoryName(f).Equals(path, StringComparison.OrdinalIgnoreCase));
            }

            return files;
        }

        public virtual IEnumerable<string> GetFiles(string path, string filter, bool recursive)
        {
            if (String.IsNullOrEmpty(filter) || filter == "*.*")
            {
                filter = "*";
            }

            // TODO: This is just flaky. We need to make it closer to the implementation that Directory.Enumerate supports perhaps by using PathResolver.
            var files = GetFiles(path, recursive);
            if (!filter.Contains("*"))
            {
                return files.Where(f => f.Equals(Path.Combine(path, filter), StringComparison.OrdinalIgnoreCase));
            }

            Regex matcher = GetFilterRegex(filter);
            return files.Where(f => matcher.IsMatch(f));
        }

        private static Regex GetFilterRegex(string wildcard)
        {
            string pattern = '^' + String.Join(@"\.", wildcard.Split('.').Select(GetPattern)) + '$';
            return new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
        }

        private static string GetPattern(string token)
        {
            return token.Replace("*", "(.*)");
        }

        public virtual void DeleteFile(string path)
        {
            Paths.Remove(path);
            Deleted.Add(path);
        }

        public virtual void DeleteFiles(IEnumerable<IPackageFile> files, string rootDir)
        {
            throw new NotImplementedException();
        }

        public virtual bool FileExists(string path)
        {
            return Paths.ContainsKey(path);
        }

        public virtual Stream OpenFile(string path)
        {
            Func<Stream> factory;
            if (!Paths.TryGetValue(path, out factory))
            {
                throw new FileNotFoundException(path + " not found.");
            }
            return factory();
        }

        public virtual Stream CreateFile(string path)
        {
            Paths[path] = () => Stream.Null;
            
            Action<Stream> streamClose = (stream) => {
                stream.Seek(0, SeekOrigin.Begin);
                AddFile(path, stream);
            };
            var memoryStream = new EventMemoryStream(streamClose);
            return memoryStream;
        }

        public string ReadAllText(string path)
        {
            return OpenFile(path).ReadToEnd();
        }

        public virtual bool DirectoryExists(string path)
        {
            string pathPrefix = PathUtility.EnsureTrailingSlash(path);
            return Paths.Keys
                        .Any(file => file.Equals(path, StringComparison.OrdinalIgnoreCase) ||
                                     file.StartsWith(pathPrefix, StringComparison.OrdinalIgnoreCase));
        }

        public virtual IEnumerable<string> GetDirectories(string path)
        {
            return Paths.GroupBy(f => Path.GetDirectoryName(f.Key))
                        .SelectMany(g => this.GetDirectories(g.Key))
                        .Where(f => !String.IsNullOrEmpty(f) &&
                               path.Equals(Path.GetDirectoryName(f), StringComparison.OrdinalIgnoreCase))
                        .Distinct();
        }

        public virtual void AddFile(string path)
        {
            AddFile(path, Stream.Null);
        }

        public void AddFile(string path, string content)
        {
            AddFile(path, content.AsStream());
        }

        public virtual void AddFile(string path, Stream stream, bool overrideIfExists)
        {
            var ms = new MemoryStream((int)stream.Length);
            stream.CopyTo(ms);
            byte[] buffer = ms.ToArray();
            Paths[path] = () => new MemoryStream(buffer);
            _createdTime[path] = DateTime.UtcNow;
        }

        public virtual void AddFile(string path, Stream stream)
        {
            AddFile(path, stream, overrideIfExists: true);
        }

        public virtual void AddFile(string path, Action<Stream> writeToStream)
        {
            var ms = new MemoryStream();
            writeToStream(ms);
            byte[] buffer = ms.ToArray();
            Paths[path] = () => new MemoryStream(buffer);
            _createdTime[path] = DateTime.UtcNow;
        }

        public virtual void AddFiles(IEnumerable<IPackageFile> files, string rootDir)
        {
            FileSystemExtensions.AddFiles(this, files, rootDir);
        }

        public virtual void AddFile(string path, Func<Stream> getStream)
        {
            Paths[path] = getStream;
        }

        public virtual DateTimeOffset GetLastModified(string path)
        {
            DateTime time;
            if (_createdTime.TryGetValue(path, out time))
            {
                return time;
            }
            else
            {
                return DateTime.UtcNow;
            }
        }

        public virtual DateTimeOffset GetCreated(string path)
        {
            DateTime time;
            if (_createdTime.TryGetValue(path, out time))
            {
                return time;
            }
            else
            {
                return DateTime.UtcNow;
            }
        }

        public virtual DateTimeOffset GetLastAccessed(string path)
        {
            DateTime time;
            if (_createdTime.TryGetValue(path, out time))
            {
                return time;
            }
            else
            {
                return DateTime.UtcNow;
            }
        }


        public void MakeFileWritable(string path)
        {
            // Nothing to do here.
        }

        public virtual void MoveFile(string src, string destination)
        {
            Paths.Add(destination, Paths[src]);
            Paths.Remove(src);
        }
    }
}