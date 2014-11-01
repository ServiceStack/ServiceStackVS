using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EnvDTE;
using ServiceStack;
using ServiceStack.Text;
using ServiceStackVS.Wizards;

namespace ServiceStackVS
{
    public static class VsixDocumentExtensions
    {
        private static bool _npmInstallRunning = false;
        private static bool _bowerInstallRunning = false;

        private static object npmStartingLock = new object();
        private static object npmRunningLock = new object();
        private static object bowerStartingLock = new object();
        private static object bowerRunningLock = new object();

        private static bool hasBowerInstalled = false;
        private static bool hasNpmInstalled;

        public static void HandleNpmPackageUpdate(this Document document, OutputWindowWriter windowWriter)
        {
            string path = document.GetProjectPath();

            //If package.json and is at the root of the project
            if (document.Name.EqualsIgnoreCase("package.json") && document.Path.EqualsIgnoreCase(path))
            {
                if (document.IsNpmUpdateDisable())
                {
                    return;
                }

                hasNpmInstalled = hasNpmInstalled ? hasNpmInstalled : NodePackageUtils.TryRegisterNpmFromDefaultLocation();

                if (!hasNpmInstalled)
                {
                    windowWriter.Show();
                    windowWriter.WriteLine("Node.js Installation not detected. Visit http://nodejs.org/ to download.");
                    return;
                }
                document.TryRunNpmInstall(windowWriter);
            }
        }

        public static void HandleBowerPackageUpdate(this Document document, OutputWindowWriter windowWriter)
        {
            string path = document.GetProjectPath();

            //If bower.json and is at the root of the project
            if (document.Name.EqualsIgnoreCase("bower.json") && document.Path.EqualsIgnoreCase(path))
            {
                if (document.IsBowerUpdateDisabled())
                {
                    return;
                }

                hasBowerInstalled = hasBowerInstalled ? hasBowerInstalled : NodePackageUtils.HasBowerOnPath();

                if (!hasBowerInstalled)
                {
                    windowWriter.Show();
                    windowWriter.WriteLine("Bower Installation not detected. Run npm install bower -g to install if Node.js/NPM already installed.");
                    return;
                }
                document.TryBowerInstall(windowWriter);
            }
        }

        public static void HandleCSharpDtoUpdate(this Document document, OutputWindowWriter outputWindowWriter)
        {
            INativeTypesHandler typesHandler = new CSharpNativeTypesHandler();
            if (document.Name.EndsWithIgnoreCase(typesHandler.CodeFileExtension))
            {
                HandleDtoUpdate(document, typesHandler, outputWindowWriter);
            }
        }

        public static void HandleFSharpDtoUpdate(this Document document, OutputWindowWriter outputWindowWriter)
        {
            INativeTypesHandler typesHandler = new FSharpNativeTypesHandler();
            if (document.Name.EndsWithIgnoreCase(typesHandler.CodeFileExtension) || document.Name.EndsWithIgnoreCase(".dto.fs"))
            {
                HandleDtoUpdate(document, typesHandler, outputWindowWriter);
            }
        }

        public static void HandleVbNetDtoUpdate(this Document document, OutputWindowWriter outputWindowWriter)
        {
            INativeTypesHandler typesHandler = new VbNetNativeTypesHandler();
            if (document.Name.EndsWithIgnoreCase(typesHandler.CodeFileExtension))
            {
                HandleDtoUpdate(document, typesHandler, outputWindowWriter);
            }
        }

        private static void HandleDtoUpdate(Document document,INativeTypesHandler typesHandler, OutputWindowWriter outputWindowWriter)
        {
            string fullPath = document.ProjectItem.Properties.Item("FullPath").Value.ToString();
            outputWindowWriter.ShowOutputPane(document.DTE);
            outputWindowWriter.Show();
            outputWindowWriter.WriteLine(
                    "--- Updating ServiceStack Reference '" +
                    fullPath.Substring(fullPath.LastIndexOf("\\", System.StringComparison.Ordinal) + 1) +
                    "' ---");
            var existingGeneratedCode = File.ReadAllLines(fullPath).Join(Environment.NewLine);
            string baseUrl = "";
            if (!typesHandler.TryExtractBaseUrl(existingGeneratedCode, out baseUrl))
            {
                outputWindowWriter.WriteLine("Failed to update ServiceStack Reference: Unabled to find BaseUrl");
                return;
            }
            try
            {
                var options = typesHandler.ParseComments(existingGeneratedCode);
                string updatedCode = typesHandler.GetUpdatedCode(baseUrl, options);

                //Can't work out another way that ensures UI is updated.
                //Overwriting the file inconsistently prompts the user that file has changed.
                //Sometimes old code persists even though file has changed.
                document.Close();
                using (var streamWriter = File.CreateText(fullPath))
                {
                    streamWriter.Write(updatedCode);
                    streamWriter.Flush();
                }
                //HACK to ensure new file is loaded
                Task.Run(() =>
                {
                    document.DTE.ItemOperations.OpenFile(fullPath);
                });
            }
            catch (Exception e)
            {
                outputWindowWriter.WriteLine("Failed to update ServiceStack Reference: Unhandled error - " + e.Message);
            }
            
            outputWindowWriter.WriteLine("--- Update ServiceStack Reference Complete ---");
        }

        public static bool IsNpmUpdateDisable(this Document document)
        {
            string path = document.GetProjectPath();
            string settingsFilePath = Path.Combine(path, "servicestack.vsconfig");
            bool npmInstallDisabled = false;
            if (settingsFilePath.FileExists())
            {
                var settings = File.ReadAllText(settingsFilePath).ParseKeyValueText(" ");
                string disableNpmInstallOnSave = "";
                if (settings.TryGetValue("DisableNpmInstallOnSave", out disableNpmInstallOnSave))
                {
                    npmInstallDisabled = disableNpmInstallOnSave.EqualsIgnoreCase("true");
                }

            }
            return npmInstallDisabled;
        }

        public static bool IsBowerUpdateDisabled(this Document document)
        {
            string path = document.GetProjectPath();
            string settingsFilePath = Path.Combine(path, "servicestack.vsconfig");
            bool bowerInstallDisabled = false;
            if (settingsFilePath.FileExists())
            {
                var settings = File.ReadAllText(settingsFilePath).ParseKeyValueText(" ");
                string disableBowerInstallOnSave = "";
                if (settings.TryGetValue("DisableBowerInstallOnSave", out disableBowerInstallOnSave))
                {
                    bowerInstallDisabled = disableBowerInstallOnSave.EqualsIgnoreCase("true");
                }

            }
            return bowerInstallDisabled;
        }

        private static string GetProjectPath(this Document document)
        {
            string projectFile = document.ProjectItem.ContainingProject.FullName;
            string path = projectFile.Substring(0, projectFile.LastIndexOf("\\", System.StringComparison.Ordinal) + 1);
            return path;
        }

        private static void TryRunNpmInstall(this Document document, OutputWindowWriter windowWriter)
        {
            lock (npmStartingLock)
            {
                if (!_npmInstallRunning)
                {
                    windowWriter.Show();
                    windowWriter.WriteLine("--- NPM install started ---");
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        try
                        {
                            NodePackageUtils.RunNpmInstall(document.Path,
                                (sender, args) =>
                                {
                                    if (!string.IsNullOrEmpty(args.Data))
                                    {
                                        string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                                        windowWriter.WriteLine(s);
                                    }
                                },
                                (sender, args) =>
                                {
                                    if (!string.IsNullOrEmpty(args.Data))
                                    {
                                        string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                                        windowWriter.WriteLine(s);
                                    }
                                });
                        }
                        catch (Exception e)
                        {
                            windowWriter.WriteLine(e.Message);
                        }

                        lock (npmRunningLock)
                        {
                            _npmInstallRunning = false;
                        }
                        windowWriter.WriteLine("--- NPM install complete ---");
                    });
                    lock (npmRunningLock)
                    {
                        _npmInstallRunning = true;
                    }
                }
            }
        }

        private static void TryBowerInstall(this Document document, OutputWindowWriter windowWriter)
        {
            lock (bowerStartingLock)
            {
                if (!_bowerInstallRunning)
                {
                    windowWriter.Show();
                    windowWriter.WriteLine("--- Bower install started ---");
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        try
                        {
                            NodePackageUtils.RunBowerInstall(document.Path,
                                (sender, args) =>
                                {
                                    string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                                    windowWriter.WriteLine(s);
                                },
                                (sender, args) =>
                                {
                                    string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                                    windowWriter.WriteLine(s);
                                });
                        }
                        catch (Exception e)
                        {
                            windowWriter.WriteLine(e.Message);
                        }

                        lock (bowerRunningLock)
                        {
                            _bowerInstallRunning = false;
                        }
                        windowWriter.WriteLine("--- Bower install complete ---");
                    });
                    lock (bowerRunningLock)
                    {
                        _bowerInstallRunning = true;
                    }
                }
            }
        }
    }
}
