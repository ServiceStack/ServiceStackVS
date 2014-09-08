using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStackVS.Wizards
{
    public class NpmUtils
    {
        private static List<string> npmPackageCache;

        private readonly Dictionary<string, string> packageOnPathCheckDictionary = new Dictionary<string, string>
        {
            {"npm","Usage: npm <command>"},
            {"bower","bower help <command>"},
            {"grunt","grunt command line interface"},
        };

        public static bool HasKnownPackageOnPath(string package)
        {
            bool result = false;

            return result;
        }

        public static void InstallPackage(string packageId, bool forceReinstall = false)
        {
            if (!HasNpmPackageInstalledGlobally(packageId) || forceReinstall)
            {
                CommandUtils.StartCommand("npm install -g " + packageId);
            }
        }

        public static void RunInstall(string workingDirectory = null, Action<object, DataReceivedEventArgs> output = null, Action<object, DataReceivedEventArgs> error = null)
        {
            CommandUtils.StartCommand("npm install",workingDirectory,output,error);
        }

        public static bool HasNodeInPath()
        {
            return CommandUtils.HasExecutableOnPath("node --help", "http://nodejs.org");
        }

        public static bool HasNpmInPath()
        {
            return CommandUtils.HasExecutableOnPath("npm", "Usage: npm <command>");
        }

        public static bool HasBowerInPath()
        {
            return CommandUtils.HasExecutableOnPath("bower", "bower help <command>");
        }

        public static bool HasGruntInPath()
        {
            return CommandUtils.HasExecutableOnPath("grunt", "grunt command line interface");
        }

        public static List<string> GetAllInstalledNpmPackages(bool ignoreCache = false)
        {
            if (npmPackageCache == null || ignoreCache)
            {
                string cmdOutput = CommandUtils.StartCommand("npm ls -g");
                List<string> allLines = new List<string>(cmdOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
                allLines.RemoveAt(0); //First line, contains command
                var allLinesFromLastSpace =
                    allLines.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Substring(x.LastIndexOf(" ", System.StringComparison.Ordinal) + 1)).ToList();
                npmPackageCache = allLinesFromLastSpace.Select(x => x.Substring(0, x.IndexOf('@'))).ToList();
            }
            return npmPackageCache;
        }

        public static bool HasNpmPackageInstalledGlobally(string packageName)
        {
            bool result = false;
            var packages = GetAllInstalledNpmPackages();
            if (packages.Contains(packageName.ToLowerInvariant()))
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
            nodeCmdProcess.WaitForExit(30000);
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

        public static string GetFullPathToCommand(string command)
        {
            string commandFile = command.Substring(0, command.IndexOf(" ", System.StringComparison.Ordinal)) + ".cmd";
            var enviromentPath = System.Environment.GetEnvironmentVariable("PATH");
            var paths = enviromentPath.Split(';');
            var exePath = paths.Select(x => Path.Combine(x, commandFile)).FirstOrDefault(File.Exists);
            if (exePath == null)
            {
                commandFile = command.Substring(0, command.IndexOf(" ", System.StringComparison.Ordinal)) + ".exe";
                exePath = paths.Select(x => Path.Combine(x, commandFile)).FirstOrDefault(File.Exists);
            }

            return exePath;
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
