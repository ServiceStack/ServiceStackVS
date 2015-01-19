using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServiceStackVS.Wizards
{
    public class NpmPackage
    {
        public string Id { get; set; }
        public string Version { get; set; }
    }

    public class BowerPackage
    {
        public string Id { get; set; }
        public string Version { get; set; }
    }

    public static class NodePackageUtils
    {
        private static List<string> npmPackageCache;

        public static void InstallGlobally(this NpmPackage package, Action<object, DataReceivedEventArgs> output = null, Action<object, DataReceivedEventArgs> error = null, bool forceReinstall = false, int timeoutSeconds = 60)
        {
            InstallNpmPackageGlobally(package.Id, output, error, forceReinstall, timeoutSeconds);
        }

        public static void Install(this NpmPackage package, string workingDirectory, Action<object, DataReceivedEventArgs> output = null, Action<object, DataReceivedEventArgs> error = null, bool forceReinstall = false, int timeoutSeconds = 60)
        {
            InstallNpmPackage(package.Id, workingDirectory,output,error, forceReinstall, timeoutSeconds);
        }

        public static void Install(this BowerPackage package, string workingDirectory, Action<object, DataReceivedEventArgs> output = null, Action<object, DataReceivedEventArgs> error = null, bool forceReinstall = false, int timeoutSeconds = 60)
        {
            InstallBowerPackage(package.Id, workingDirectory,output,error, forceReinstall, timeoutSeconds);
        }

        public static void InstallNpmPackageGlobally(string packageId,Action<object, DataReceivedEventArgs> output = null, Action<object, DataReceivedEventArgs> error = null, bool forceReinstall = false, int timeoutSeconds = 60)
        {
            if (!HasNpmPackageInstalledGlobally(packageId) || forceReinstall)
            {
                CommandUtils.StartCommand("npm install -g " + packageId, null, output, error, timeoutSeconds);
            }
        }

        public static void InstallNpmPackage(string packageId, string workingDirectory, Action<object, DataReceivedEventArgs> output = null, Action<object, DataReceivedEventArgs> error = null, bool forceReinstall = false, int timeoutSeconds = 60)
        {
            if (!HasNpmPackageInstalled(packageId, workingDirectory) || forceReinstall)
            {
                CommandUtils.StartCommand("npm install " + packageId, workingDirectory, output, error, timeoutSeconds);
            }
        }

        public static void InstallBowerPackage(string packageId, string workingDirectory, Action<object, DataReceivedEventArgs> output = null, Action<object, DataReceivedEventArgs> error = null, bool forceReinstall = false, int timeoutSeconds = 60)
        {
            if (!HasBowerPackageInstalled(packageId,workingDirectory) || forceReinstall)
            {
                CommandUtils.StartCommand("bower install " + packageId, workingDirectory, output, error, timeoutSeconds);
            }
        }

        public static void NpmClearCache(string workingDirectory = null, int timeoutSeconds = 60)
        {
            CommandUtils.StartCommand("npm cache clear", workingDirectory);
        }

        public static void RunNpmInstall(string workingDirectory = null, Action<object, DataReceivedEventArgs> output = null, Action<object, DataReceivedEventArgs> error = null, int timeoutSeconds = 60)
        {
            CommandUtils.StartCommand("npm install", workingDirectory, output, error, timeoutSeconds);
        }

        public static void RunBowerInstall(string workingDirectory, Action<object, DataReceivedEventArgs> output = null, Action<object, DataReceivedEventArgs> error = null, int timeoutSeconds = 60)
        {
            if (string.IsNullOrEmpty(workingDirectory))
            {
                throw new Exception("Bower working directory null or empty");
            }

            CommandUtils.StartCommand("bower install", workingDirectory, output, error, timeoutSeconds);
        }

        public static bool HasNodeInPath()
        {
            //If user has not restarted visual studio since node install, this will still return false even though node is accessible from command-line
            bool execFoundOnPath = CommandUtils.HasExecutableOnPath("node");
            if (execFoundOnPath)
            {
                return true;
            }
            bool canRun = CommandUtils.HasExecutableOnPath("node --help", "http://nodejs.org/");
            return canRun;
        }


        public static bool HasGitOnPath()
        {
            return CommandUtils.GetFullPathToCommand("git") != null;
        }

        public static bool HasNpmOnPath()
        {
            //If user has not restarted visual studio since node install, this will still return false even though npm is accessible from command-line
            bool execFoundOnPath = CommandUtils.HasExecutableOnPath("npm");
            if (execFoundOnPath)
            {
                return true;
            }
            bool canRun = CommandUtils.HasExecutableOnPath("npm", "Usage: npm <command>");
            return canRun;
        }

        public static bool HasBowerOnPath()
        {
            //If user has not restarted visual studio since node install, this will still return false even though bower is accessible from command-line
            bool execFoundOnPath = CommandUtils.HasExecutableOnPath("bower");
            if (execFoundOnPath)
            {
                return true;
            }
            bool canRun = CommandUtils.HasExecutableOnPath("bower", "bower help <command>");
            return canRun;
        }

        public static bool HasGruntOnPath()
        {
            //If user has not restarted visual studio since node install, this will still return false even though grunt is accessible from command-line
            bool execFoundOnPath = CommandUtils.HasExecutableOnPath("grunt");
            if (execFoundOnPath)
            {
                return true;
            }
            bool canRun = CommandUtils.HasExecutableOnPath("grunt", "grunt command line interface");
            return canRun;
        }

        public static bool HasGulpOnPath()
        {
            return CommandUtils.HasExecutableOnPath("gulp");
        }

        public static List<string> GetGloballyInstalledNpmPackages(bool ignoreCache = false)
        {
            if (npmPackageCache == null || ignoreCache)
            {
                string cmdOutput = CommandUtils.StartCommand("npm ls -g");
                List<string> allLines = new List<string>(cmdOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
                allLines.RemoveAt(0); //First line, contains command
                //If fresh new installation, no packages installed globally
                if (allLines[0].Contains("(empty)"))
                {
                    return new List<string>();
                }
                //Parsing tree structure due to problems with --depth not working and json format being 
                //just key/value pairing, instead of array, doesn't map to properties very well.
                var packagesWithVersion =
                    allLines
                        .Where(x => !string.IsNullOrEmpty(x) &&
                                    x.IndexOf(" ", System.StringComparison.Ordinal) != -1 &&
                                    x.IndexOf(" ", System.StringComparison.Ordinal) == x.LastIndexOf(" ", System.StringComparison.Ordinal))
                        .Select(x => x.Substring(x.LastIndexOf(" ", System.StringComparison.Ordinal) + 1)).ToList();

                npmPackageCache = packagesWithVersion.Select(x => x.Substring(0, x.IndexOf('@'))).ToList();
            }
            return npmPackageCache;
        }

        public static List<string> GetInstalledNpmPackages(string workingDirectory)
        {
            string cmdOutput = CommandUtils.StartCommand("npm ls");
            List<string> allLines = new List<string>(cmdOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
            allLines.RemoveAt(0); //First line, contains command
            if (allLines[0].Contains("(empty)"))
            {
                return new List<string>();
            }
            //Parsing tree structure due to problems with --depth not working and json format being 
            //just key/value pairing, instead of array, doesn't map to properties very well.
            var packagesWithVersion =
                allLines
                    .Where(x => !string.IsNullOrEmpty(x) &&
                                x.IndexOf(" ", System.StringComparison.Ordinal) != -1 &&
                                x.IndexOf(" ", System.StringComparison.Ordinal) == x.LastIndexOf(" ", System.StringComparison.Ordinal))
                    .Select(x => x.Substring(x.LastIndexOf(" ", System.StringComparison.Ordinal) + 1)).ToList();

            return packagesWithVersion.Select(x => x.Substring(0, x.IndexOf('@'))).ToList();
        }

        public static List<string> GetInstalledBowerPackages(string workingDirectory)
        {
            List<string> bowerPackageCache;
                string cmdOutput = CommandUtils.StartCommand("bower list --offline", workingDirectory);
                List<string> allLines = new List<string>(cmdOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
                allLines.RemoveAt(0); //First line, contains command
                var packagesWithVersion =
                    allLines
                        .Where(x => !string.IsNullOrEmpty(x) &&
                                    x.IndexOf(" ", System.StringComparison.Ordinal) != -1 &&
                                    x.IndexOf(" ", System.StringComparison.Ordinal) == x.LastIndexOf(" ", System.StringComparison.Ordinal))
                        .Select(x => x.Substring(x.LastIndexOf(" ", System.StringComparison.Ordinal) + 1)).ToList();

            
            bowerPackageCache = packagesWithVersion.Select(x => x.Substring(0, x.IndexOf('#'))).ToList();
            return bowerPackageCache;
        }

        public static bool HasNpmPackageInstalledGlobally(string packageId)
        {
            bool result = false;
            var packages = GetGloballyInstalledNpmPackages();
            if (packages.Contains(packageId.ToLowerInvariant()))
            {
                result = true;
            }
            return result;
        }

        public static bool HasNpmPackageInstalled(string packageId, string workingDirectory)
        {
            bool result = false;
            var packages = GetInstalledNpmPackages(workingDirectory);
            if (packages.Contains(packageId.ToLowerInvariant()))
            {
                result = true;
            }
            return result;
        }

        public static bool HasBowerPackageInstalled(string packageId, string workingDirectory)
        {
            bool result = false;
            var packages = GetInstalledBowerPackages(workingDirectory);
            if (packages.Contains(packageId.ToLowerInvariant()))
            {
                result = true;
            }
            return result;
        }

        public static bool TryRegisterNpmFromDefaultLocation()
        {
            bool hasNpmOnPath = HasNpmOnPath();
            if (hasNpmOnPath)
            {
                return true;
            }
                
            string systemPath = System.Environment.GetEnvironmentVariable("SystemRoot");
            bool x86Path;
            bool x64Path;
            string path = "";
            if (systemPath != null)
            {
                string systemDrive = systemPath.Substring(0, systemPath.IndexOf("\\", System.StringComparison.Ordinal) + 1);
                x86Path = Directory.Exists(Path.Combine(systemDrive, "Program Files (x86)\\nodejs"));
                x64Path = Directory.Exists(Path.Combine(systemDrive, "Program Files\\nodejs"));
                if (x86Path)
                {
                    path = Path.Combine(systemDrive, "Program Files (x86)\\nodejs");
                }

                if (x64Path)
                {
                    path = Path.Combine(systemDrive, "Program Files\\nodejs");
                }
            }

            if (!string.IsNullOrEmpty(path) && File.Exists(Path.Combine(path,"node.exe")))
            {
                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string npmFolder = Path.Combine(appDataFolder, "npm");
                path.AddToPathEnvironmentVariable();
                Path.Combine(path, "node_modules\\npm\\bin").AddToPathEnvironmentVariable();
                npmFolder.AddToPathEnvironmentVariable();
                return true;
            }
            return false;
        }

        public static void AddToPathEnvironmentVariable(this string pathValue)
        {
            var processPathValue = Environment.GetEnvironmentVariable("PATH");
            var userValue = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            if (userValue != null && !userValue.Contains(pathValue))
            {
                Environment.SetEnvironmentVariable("PATH",userValue + ";" + pathValue,EnvironmentVariableTarget.User);
            }
            if (processPathValue != null && !processPathValue.Contains(pathValue))
            {
                Environment.SetEnvironmentVariable("PATH", processPathValue + ";" + pathValue);
            }
        }
    }


    public class CommandUtils
    {
        public static string StartCommand(
            string command, 
            string workingDirectory = null, 
            Action<object, DataReceivedEventArgs> output = null, 
            Action<object, DataReceivedEventArgs> error = null,
            int timeoutSeconds = 60)
        {
            var nodeCmdProcess = new Process();
            string fullPath = GetFullPathToCommand(command);
            nodeCmdProcess.StartInfo.Arguments = "/c " + "\"" + fullPath + "\" " + command.Substring(command.IndexOf(" ", System.StringComparison.Ordinal) + 1);
            nodeCmdProcess.StartInfo.RedirectStandardOutput = true;
            nodeCmdProcess.StartInfo.RedirectStandardError = true;
            nodeCmdProcess.StartInfo.UseShellExecute = false;
            nodeCmdProcess.StartInfo.CreateNoWindow = true;
            nodeCmdProcess.StartInfo.FileName = "cmd.exe";
            nodeCmdProcess.StartInfo.WorkingDirectory = workingDirectory ?? "";
            string eventData = "";
            StringBuilder cmdOutput = new StringBuilder();
            StringBuilder errorOutput = new StringBuilder();
            nodeCmdProcess.Start();

            //npm install reports feedback to the command line for HTTP requests as errors and installation result as output..
            nodeCmdProcess.OutputDataReceived += (sender, args) =>
            {
                if (output != null)
                    output(sender, args);
                cmdOutput.AppendLine(args.Data);
            };

            nodeCmdProcess.ErrorDataReceived += (sender, args) =>
            {
                if (error != null)
                    error(sender, args);
                errorOutput.AppendLine(args.Data);
            };
            bool timedOut = true;
            nodeCmdProcess.Exited += (sender, args) =>
            {
                timedOut = false;
            };
            nodeCmdProcess.BeginOutputReadLine();
            nodeCmdProcess.BeginErrorReadLine();
            nodeCmdProcess.WaitForExit(timeoutSeconds * 1000);
            string errorData;
            lock (cmdOutput)
            {
                eventData = cmdOutput.ToString();
            }
            lock (errorOutput)
            {
                errorData = errorOutput.ToString();
            }

            try
            {
                if (nodeCmdProcess.ExitCode != 0)
                {
                    throw new ProcessException("External process '" + nodeCmdProcess.StartInfo.FileName + " " +
                                               nodeCmdProcess.StartInfo.Arguments + "' with error data: " + errorData);
                }
            }
            catch (ProcessException)
            {
                throw;
            }
            catch (Exception)
            {
                if (timedOut)
                {
                    nodeCmdProcess.Kill();
                    throw new TimeoutException("A command has timed out. " + command +
                                    "Please check your internet connection and/or proxy settings before trying again.");
                }
                throw;
            }
            
            nodeCmdProcess.Dispose();
            return eventData;
        }

        private static List<string> supportedCommandExtensions = new List<string>(
            new[] 
                {".cmd",".exe"}
            );

        public static string GetFullPathToCommand(string command)
        {
            string execPath = null;
            foreach (string currentExtension in supportedCommandExtensions)
            {
                int firstSpaceIndex = command.IndexOf(" ", System.StringComparison.Ordinal);
                bool containsArgs = firstSpaceIndex > 0;
                string commandFile = command.Substring(0, containsArgs ? firstSpaceIndex : command.Length) + currentExtension;
                var processEnvironmentPath = System.Environment.GetEnvironmentVariable("PATH");
                
                var paths = processEnvironmentPath.Split(';');
                execPath = paths.Where(x => 
                    x.IndexOfAny(Path.GetInvalidPathChars()) == -1)
                    .Select(x => Path.Combine(x, commandFile))
                    .FirstOrDefault(File.Exists);
                if (execPath != null)
                    break;
            }
            return execPath;
        }

        public static bool HasExecutableOnPath(string commandName, string expectedOutput, bool supressExceptions = true)
        {
            bool hasExecutableOnPath = false;
            try
            {
                string eventData = StartCommand(commandName);
                if (eventData.Contains(expectedOutput))
                {
                    hasExecutableOnPath = true;
                }
            }
            catch (Exception)
            {
                if (supressExceptions) return false;
                throw;
            }
            
            return hasExecutableOnPath;
        }

        public static bool HasExecutableOnPath(string commandName)
        {
            return GetFullPathToCommand(commandName) != null;
        }

        public static bool TryRegisterAppFromProgramFiles(string pfRelativePath, string commandName, string commandExtension = ".exe")
        {
            bool hasExecutableOnPath = HasExecutableOnPath(commandName);
            if (hasExecutableOnPath)
            {
                return true;
            }

            string systemPath = System.Environment.GetEnvironmentVariable("SystemRoot");
            bool x86Path;
            bool x64Path;
            string path = "";
            if (systemPath != null)
            {
                string systemDrive = systemPath.Substring(0, systemPath.IndexOf("\\", System.StringComparison.Ordinal) + 1);
                x86Path = Directory.Exists(Path.Combine(systemDrive, "Program Files (x86)\\" + pfRelativePath));
                x64Path = Directory.Exists(Path.Combine(systemDrive, "Program Files\\" + pfRelativePath));
                if (x86Path)
                {
                    path = Path.Combine(systemDrive, "Program Files (x86)\\" + pfRelativePath);
                }

                if (x64Path)
                {
                    path = Path.Combine(systemDrive, "Program Files\\" + pfRelativePath);
                }
            }

            if (!string.IsNullOrEmpty(path) && File.Exists(Path.Combine(path, commandName + commandExtension)))
            {
                path.AddToPathEnvironmentVariable();
                return true;
            }
            return false;
        }
    }

    public class ProcessException : Exception
    {
        public ProcessException()
            : base()
        {

        }

        public ProcessException(string message)
            : base(message)
        {

        }
    }
}
