using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void InstallGlobally(this NpmPackage package, bool forceReinstall = false)
        {
            InstallNpmPackageGlobally(package.Id, forceReinstall);
        }

        public static void Install(this NpmPackage package, string workingDirectory, bool forceReinstall = false)
        {
            InstallNpmPackage(package.Id, workingDirectory);
        }

        public static void Install(this BowerPackage package, string workingDirectory, bool forceReinstall = false)
        {
            InstallBowerPackage(package.Id,workingDirectory);
        }

        public static void InstallNpmPackageGlobally(string packageId, bool forceReinstall = false)
        {
            if (!HasNpmPackageInstalledGlobally(packageId) || forceReinstall)
            {
                CommandUtils.StartCommand("npm install -g " + packageId);
            }
        }

        public static void InstallNpmPackage(string packageId, string workingDirectory, bool forceReinstall = false)
        {
            if (!HasNpmPackageInstalled(packageId, workingDirectory) || forceReinstall)
            {
                CommandUtils.StartCommand("npm install " + packageId,workingDirectory);
            }
        }

        public static void InstallBowerPackage(string packageId, string workingDirectory, bool forceReinstall = false)
        {
            if (!HasBowerPackageInstalled(packageId,workingDirectory) || forceReinstall)
            {
                CommandUtils.StartCommand("bower install " + packageId, workingDirectory);
            }
        }

        public static void RunNpmInstall(string workingDirectory = null, Action<object, DataReceivedEventArgs> output = null, Action<object, DataReceivedEventArgs> error = null)
        {
            CommandUtils.StartCommand("npm install", workingDirectory, output, error);
        }

        public static void RunBowerInstall(string workingDirectory, Action<object, DataReceivedEventArgs> output = null, Action<object, DataReceivedEventArgs> error = null)
        {
            if (string.IsNullOrEmpty(workingDirectory))
            {
                throw new Exception("Bower working directory null or empty");
            }

            CommandUtils.StartCommand("bower install", workingDirectory, output, error);
        }

        public static bool HasNodeInPath()
        {
            //If user has not restarted visual studio since node install, this will still return false even though node is accessible from command-line
            bool execFoundOnPath = CommandUtils.GetFullPathToCommand("node") != null;
            if (execFoundOnPath)
            {
                return true;
            }
            bool canRun = CommandUtils.HasExecutableOnPath("node --help", "http://nodejs.org/");
            return canRun;
        }

        public static bool HasNpmInPath()
        {
            //If user has not restarted visual studio since node install, this will still return false even though node is accessible from command-line
            bool execFoundOnPath = CommandUtils.GetFullPathToCommand("npm") != null;
            if (execFoundOnPath)
            {
                return true;
            }
            bool canRun = CommandUtils.HasExecutableOnPath("npm", "Usage: npm <command>");
            return canRun;
        }

        public static bool HasBowerInPath()
        {
            //If user has not restarted visual studio since node install, this will still return false even though node is accessible from command-line
            bool execFoundOnPath = CommandUtils.GetFullPathToCommand("bower") != null;
            if (execFoundOnPath)
            {
                return true;
            }
            bool canRun = CommandUtils.HasExecutableOnPath("bower", "bower help <command>");
            return canRun;
        }

        public static bool HasGruntInPath()
        {
            //If user has not restarted visual studio since node install, this will still return false even though node is accessible from command-line
            bool execFoundOnPath = CommandUtils.GetFullPathToCommand("grunt") != null;
            if (execFoundOnPath)
            {
                return true;
            }
            bool canRun = CommandUtils.HasExecutableOnPath("grunt", "grunt command line interface");
            return canRun;
        }

        public static bool HasGulpInPath()
        {
            return CommandUtils.GetFullPathToCommand("gulp") != null;
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
    }


    public class CommandUtils
    {
        private static bool processPathInit = false;
        public static string StartCommand(string command, string workingDirectory = null, Action<object, DataReceivedEventArgs> output = null, Action<object, DataReceivedEventArgs> error = null)
        {
            var nodeCmdProcess = new Process();
            string fullPath = GetFullPathToCommand(command);
            nodeCmdProcess.StartInfo.Arguments = "/c " + "\"" + fullPath + "\" " + command.Substring(command.IndexOf(" ", System.StringComparison.Ordinal) + 1);
            nodeCmdProcess.StartInfo.RedirectStandardOutput = true;
            nodeCmdProcess.StartInfo.RedirectStandardError = true;
            nodeCmdProcess.StartInfo.UseShellExecute = false;
#if DEBUG
            nodeCmdProcess.StartInfo.CreateNoWindow = false;
#else
            nodeCmdProcess.StartInfo.CreateNoWindow = true;
#endif
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
            nodeCmdProcess.BeginOutputReadLine();
            nodeCmdProcess.BeginErrorReadLine();
            //45 second timeout
            nodeCmdProcess.WaitForExit(45000);
            eventData = cmdOutput.ToString();
            string errorData = errorOutput.ToString();
            if (nodeCmdProcess.ExitCode != 0)
            {
                throw new ProcessException("External process '" + nodeCmdProcess.StartInfo.FileName + " " +
                                           nodeCmdProcess.StartInfo.Arguments + "' with error data: " + errorData);
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
                var enviromentPath = System.Environment.GetEnvironmentVariable("PATH");
                var paths = enviromentPath.Split(';');
                execPath = paths.Select(x => Path.Combine(x, commandFile)).FirstOrDefault(File.Exists);
                if (execPath != null)
                    break;
            }
            return execPath;
        }

        public static bool HasExecutableOnPath(string commandName, string expectedOutput)
        {
            bool hasExecutableOnPath = false;
            string eventData = StartCommand(commandName);
            if (eventData.Contains(expectedOutput))
            {
                hasExecutableOnPath = true;
            }
            return hasExecutableOnPath;
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
